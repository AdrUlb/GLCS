using GLCS;
using GLCS.Managed;
using SDL3CS;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;

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
	const int initialWidth_ = 800;
	const int initialHeight_ = 600;

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

	private static int Main()
	{
		try
		{
#if !AOT_PUBLISH
			NativeLibrary.SetDllImportResolver(typeof(Sdl).Assembly, ImportResolver);
#endif

			Run();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Fatal Error: {ex.Message}");
			return 1;
		}

		return 0;
	}

	private static volatile bool closeRequested_ = false;

	private static void Run()
	{
		Sdl.Init(Sdl.InitFlags.Video);

		Sdl.GL_SetAttribute(Sdl.GLAttr.DoubleBuffer, 1);
		Sdl.GL_SetAttribute(Sdl.GLAttr.ContextMajorVersion, 3);
		Sdl.GL_SetAttribute(Sdl.GLAttr.ContextMinorVersion, 3);
		Sdl.GL_SetAttribute(Sdl.GLAttr.ContextFlags, Sdl.GLContextFlags.ForwardCompatible);
		Sdl.GL_SetAttribute(Sdl.GLAttr.ContextProfileMask, Sdl.GLProfile.Core);
		Sdl.GL_SetAttribute(Sdl.GLAttr.MultisampleBuffers, 1);
		Sdl.GL_SetAttribute(Sdl.GLAttr.MultisampleSamples, 8);

		var window = Sdl.CreateWindow("GLCS Test", initialWidth_, initialHeight_, Sdl.WindowFlags.OpenGL | Sdl.WindowFlags.Hidden | Sdl.WindowFlags.Resizable);

		if (window.IsNull)
			throw new($"Failed to create SDL window: {Sdl.GetError()}");

		var context = Sdl.GL_CreateContext(window);
		if (context.IsNull)
			throw new($"Failed to create GL context: {Sdl.GetError()}");

		Sdl.GL_MakeCurrent(window, context);
		var gl = new ManagedGL(static proc => Sdl.GL_GetProcAddress(proc));

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

		Sdl.GL_SetSwapInterval(1);

		gl.Viewport(0, 0, initialWidth_, initialHeight_);

		Sdl.ShowWindow(window);

		while (!closeRequested_)
		{
			while (Sdl.PollEvent(out var ev))
			{
				switch (ev.Type)
				{
					case Sdl.EventType.WindowPixelSizeChanged:
						gl.Viewport(0, 0, ev.Window.Data1, ev.Window.Data2);
						break;
					case Sdl.EventType.WindowCloseRequested:
						closeRequested_ = true;
						break;
				}
			}

			Sdl.GetWindowSizeInPixels(window, out var w, out var h);
			gl.Clear(Color.CornflowerBlue, ClearBufferMask.ColorBufferBit);
			vao.Draw(PrimitiveType.Triangles, 0, 3, program);
			Sdl.GL_SwapWindow(window);
		}

		vao.Dispose();
		vbo.Dispose();
		program.Dispose();

		Sdl.GL_MakeCurrent(window, Sdl.GLContext.Null);
		Sdl.GL_DestroyContext(context);
		Sdl.DestroyWindow(window);

		Sdl.Quit();
	}

	private static readonly Dictionary<string, nint> loadedLibraries_ = [];

#if !AOT_PUBLISH
	public static nint ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
	{
		if (loadedLibraries_.TryGetValue(libraryName, out var handle))
			return handle;

		var rid = RuntimeInformation.RuntimeIdentifier;
		var prefix =
			OperatingSystem.IsWindows() ? "" :
			OperatingSystem.IsLinux() ? "lib" :
			OperatingSystem.IsMacOS() ? "lib" :
			throw new PlatformNotSupportedException();

		var suffix =
			OperatingSystem.IsWindows() ? ".dll" :
			OperatingSystem.IsLinux() ? ".so" :
			OperatingSystem.IsMacOS() ? ".dylib" :
			throw new PlatformNotSupportedException();

		var fullName = $"{prefix}{libraryName}{suffix}";
		var path = Path.Combine("runtimes", rid, "native", fullName);
		var appPath = Path.Combine(AppContext.BaseDirectory, path);

		nint libraryHandle;

		if ((File.Exists(path) && (libraryHandle = NativeLibrary.Load(path)) != 0) ||
			(File.Exists(appPath) && (libraryHandle = NativeLibrary.Load(path)) != 0))
		{
			Console.WriteLine($"Resolved native library: {libraryName} -> {path}");
			loadedLibraries_.Add(libraryName, libraryHandle);
			return libraryHandle;
		}

		return 0;
	}
#endif
}
