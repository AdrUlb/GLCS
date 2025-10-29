using GLCS;
using GLCS.Managed;
using SDL3CS;
using System.Drawing;
using System.Numerics;

namespace Test;

internal readonly struct VertexAttribs(Vector3 position, Color color)
{
	[GLVertexAttrib(0, 3, VertexAttribPointerType.Float, false)]
	public readonly Vector3 Position = position;

	[GLVertexAttrib(1, 4, VertexAttribPointerType.Float, false)]
	public readonly Vector4 Color = new(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
}

internal static class Program
{
	private const string vertexShaderSource_ =
		"""
		#version 330 core
		layout(location = 0) in vec3 aPos;
		layout(location = 1) in vec4 aColor;

		out vec4 vColor;

		void main()
		{
			vColor = aColor;

			gl_Position = vec4(aPos, 1.0);
		}
		""";

	private const string fragmentShaderSource_ =
		"""
		#version 330 core
		out vec4 FragColor;

		in vec4 vColor;

		void main()
		{
			FragColor = vColor;
		}
		""";

	private static readonly VertexAttribs[] vertices_ =
	[
		new(new(0.0f, 0.5f, 0.0f), Color.Red), // top
		new(new(-0.5f, -0.5f, 0.0f), Color.Green), // bottom left
		new(new(0.5f, -0.5f, 0.0f), Color.Blue), // bottom right
	];

	private static bool closeRequested_ = false;

	private static int Main()
	{
		try
		{
			Run();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Fatal Error: {ex.Message}");
			return 1;
		}

		return 0;
	}

	private static ManagedGL? gl = null;

	private unsafe static void Run()
	{
		SDL.Init(SDL.InitFlags.Video);

		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_DOUBLEBUFFER, 1);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_MINOR_VERSION, 3);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_FLAGS, SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_MULTISAMPLEBUFFERS, 1);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_MULTISAMPLESAMPLES, 8);

		var initialWidth = 800;
		var initialHeight = 600;

		var props = SDL.CreateProperties();
		SDL.SetStringProperty(props, SDL_PROP_WINDOW_CREATE_TITLE_STRING, "GLCS Test");
		SDL.SetNumberProperty(props, SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER, initialWidth);
		SDL.SetNumberProperty(props, SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER, initialHeight);
		SDL.SetBooleanProperty(props, SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN, true);
		SDL.SetBooleanProperty(props, SDL_PROP_WINDOW_CREATE_OPENGL_BOOLEAN, true);
		SDL.SetBooleanProperty(props, SDL_PROP_WINDOW_CREATE_RESIZABLE_BOOLEAN, true);
		var window = SDL.CreateWindowWithProperties(props);
		SDL.DestroyProperties(props);

		if (window == 0)
			throw new($"Failed to create SDL window: {SDL_GetError()}");

		var context = SDL_GL_CreateContext(window);
		if (context == null)
			throw new($"Failed to create GL context: {SDL_GetError()}");

		SDL_GL_MakeCurrent(window, context);
		gl = new ManagedGL(static proc => SDL_GL_GetProcAddress(proc));

		Console.WriteLine($"OpenGL Renderer: {gl.GetString(StringName.Renderer)}");
		Console.WriteLine($"OpenGL Version: {gl.GetString(StringName.Version)}");

		var program = new GLProgram(gl);

		using (var vertexShader = new GLShader(gl, ShaderType.VertexShader))
		using (var fragmentShader = new GLShader(gl, ShaderType.FragmentShader))
		{
			vertexShader.Source(vertexShaderSource_);
			vertexShader.Compile();
			if (vertexShader.Get(ShaderParameterName.CompileStatus) != GL.TRUE)
				throw new($"Vertex shader compilation failed: {vertexShader.GetInfoLog()}");

			fragmentShader.Source(fragmentShaderSource_);
			fragmentShader.Compile();
			if (fragmentShader.Get(ShaderParameterName.CompileStatus) != GL.TRUE)
				throw new($"Fragment shader compilation failed: {fragmentShader.GetInfoLog()}");

			program.AttachShader(vertexShader);
			program.AttachShader(fragmentShader);

			program.Link();

			program.DetachShader(vertexShader);
			program.DetachShader(fragmentShader);
		}
		if (program.Get(ProgramPropertyARB.LinkStatus) != GL.TRUE)
			throw new($"Shader program linking failed: {program.GetInfoLog()}");

		var vbo = new GLBuffer<VertexAttribs>(gl);
		vbo.Data(vertices_, BufferUsageARB.StaticDraw);

		var vao = new GLVertexArray(gl);
		vao.VertexAttribPointers(vbo);

		SDL_GL_SetSwapInterval(0);

		gl.Viewport(0, 0, initialWidth, initialHeight);

		SDL_GL_MakeCurrent(window, null);

		var width = initialWidth;
		var height = initialHeight;

		var renderThread = new Thread(() =>
		{
			SDL_GL_MakeCurrent(window, context);
			var lastWidth = width;
			var lastHeight = height;
			while (!closeRequested_)
			{
				if (lastWidth != width || lastHeight != height)
				{
					lastWidth = width;
					lastHeight = height;
					gl.Viewport(0, 0, lastWidth, lastHeight);
				}
				gl.Clear(Color.CornflowerBlue, ClearBufferMask.ColorBufferBit);
				vao.Draw(PrimitiveType.Triangles, 0, 3, program);
				SDL_GL_SwapWindow(window);
			}
		});

		renderThread.Start();

		SDL_ShowWindow(window);

		while (!closeRequested_)
		{
			SDL_Event ev;
			while (SDL_PollEvent(&ev))
			{
				switch ((SDL_EventType)ev.type)
				{
					case SDL_EventType.SDL_EVENT_WINDOW_PIXEL_SIZE_CHANGED:
						width = ev.window.data1;
						height = ev.window.data2;
						Console.WriteLine($"Window resized to {width}x{height}");
						break;
					case SDL_EventType.SDL_EVENT_WINDOW_CLOSE_REQUESTED:
						closeRequested_ = true;
						break;
				}
			}
		}

		SpinWait.SpinUntil(() => renderThread.Join(100));

		vao.Dispose();
		vbo.Dispose();
		program.Dispose();

		SDL_GL_DestroyContext(context);
		SDL.DestroyWindow(window);
	}
}
