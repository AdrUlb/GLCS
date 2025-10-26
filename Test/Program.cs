using GLCS;
using GLCS.Abstractions;
using SDL;
using System.Drawing;
using System.Numerics;
using static SDL.SDL3;

namespace Test;

internal readonly struct VertexAttribs(Vector3 position, Color color)
{
	[GLVertexAttrib(0, 3, VertexAttribPointerType.GL_FLOAT, false)]
	public readonly Vector3 Position = position;

	[GLVertexAttrib(1, 4, VertexAttribPointerType.GL_FLOAT, false)]
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

	private unsafe static void Main()
	{
		SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_DOUBLEBUFFER, 1);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_MINOR_VERSION, 3);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_FLAGS, SDL_GL_CONTEXT_FORWARD_COMPATIBLE_FLAG);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_PROFILE_MASK, SDL_GL_CONTEXT_PROFILE_CORE);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_MULTISAMPLEBUFFERS, 1);
		SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_MULTISAMPLESAMPLES, 8);

		var props = SDL_CreateProperties();
		SDL_SetStringProperty(props, SDL_PROP_WINDOW_CREATE_TITLE_STRING, "GLCS Test");
		SDL_SetNumberProperty(props, SDL_PROP_WINDOW_CREATE_WIDTH_NUMBER, 800);
		SDL_SetNumberProperty(props, SDL_PROP_WINDOW_CREATE_HEIGHT_NUMBER, 600);
		SDL_SetBooleanProperty(props, SDL_PROP_WINDOW_CREATE_HIDDEN_BOOLEAN, true);
		SDL_SetBooleanProperty(props, SDL_PROP_WINDOW_CREATE_OPENGL_BOOLEAN, true);
		SDL_SetBooleanProperty(props, SDL_PROP_WINDOW_CREATE_RESIZABLE_BOOLEAN, true);
		var window = SDL_CreateWindowWithProperties(props);
		SDL_DestroyProperties(props);

		if (window == null)
			throw new($"Failed to create SDL window: {SDL_GetError()}");

		var context = SDL_GL_CreateContext(window);
		if (context == null)
			throw new($"Failed to create GL context: {SDL_GetError()}");

		var closeRequested = false;

		SDL_GL_MakeCurrent(window, context);
		var gl = new GL(static proc => SDL_GL_GetProcAddress(proc));

		Console.WriteLine($"OpenGL Renderer: {gl.GetStringManaged(StringName.GL_RENDERER)}");
		Console.WriteLine($"OpenGL Version: {gl.GetStringManaged(StringName.GL_VERSION)}");

		var program = new GLProgram(gl);
		using (var vertexShader = new GLShader(gl, ShaderType.GL_VERTEX_SHADER))
		using (var fragmentShader = new GLShader(gl, ShaderType.GL_FRAGMENT_SHADER))
		{
			vertexShader.Compile(vertexShaderSource_);
			fragmentShader.Compile(fragmentShaderSource_);
			program.Link(vertexShader, fragmentShader);
		}

		var vbo = new GLBuffer(gl);
		vbo.Data(vertices_, BufferUsageARB.GL_STATIC_DRAW);

		var vao = new GLVertexArray(gl);
		vao.VertexAttribPointers<VertexAttribs>(vbo);
		
		SDL_ShowWindow(window);

		SDL_GL_SetSwapInterval(0);
		
		while (!closeRequested)
		{
			SDL_Event ev;
			while (SDL_PollEvent(&ev))
			{
				switch ((SDL_EventType)ev.type)
				{
					case SDL_EventType.SDL_EVENT_WINDOW_CLOSE_REQUESTED:

						closeRequested = true;
						break;
				}
			}

			int width = 0, height = 0;
			if (SDL_GetWindowSize(window, &width, &height))
				gl.Viewport(0, 0, width, height);

			gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			gl.Clear(ClearBufferMask.GL_COLOR_BUFFER_BIT);
			vao.Draw(PrimitiveType.GL_TRIANGLES, 0, 3, program);
			SDL_GL_SwapWindow(window);
		}

		vao.Dispose();
		vbo.Dispose();
		program.Dispose();

		SDL_GL_DestroyContext(context);
		SDL_DestroyWindow(window);
	}
}
