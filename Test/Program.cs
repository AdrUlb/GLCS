using GLCS;
using GLCS.Abstractions;
using SDL;
using System.Drawing;
using System.Numerics;
using static GLCS.GL;

namespace Test;

internal unsafe readonly struct VertexAttribs(Vector3 position, Color color)
{
	[GLVertexAttrib(0, 3, GL_FLOAT, false)]
	public readonly Vector3 Position = position;

	[GLVertexAttrib(1, 4, GL_FLOAT, false)]
	public readonly Vector4 Color = new(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
}

internal static class Program
{
	private const string vertexShaderSource_ = """
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

	private const string fragmentShaderSource_ = """
			#version 330 core
			out vec4 FragColor;
			
			in vec4 vColor;

			void main()
			{
				FragColor = vColor;
			}
		""";

	private static readonly VertexAttribs[] vertices_ = [
		new(new (0.0f,  0.5f, 0.0f), Color.Red),  // top
		new(new (-0.5f, -0.5f, 0.0f), Color.Green),  // bottom left
		new(new (0.5f, -0.5f, 0.0f), Color.Blue),  // bottom right
	];

	private static unsafe void Main()
	{
		SDL3.SDL_Init(SDL_InitFlags.SDL_INIT_VIDEO);
		SDL3.SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_DOUBLEBUFFER, 1);
		SDL3.SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
		SDL3.SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_MINOR_VERSION, 3);
		SDL3.SDL_GL_SetAttribute(SDL_GLAttr.SDL_GL_CONTEXT_PROFILE_MASK, SDL3.SDL_GL_CONTEXT_PROFILE_CORE);

		var window = SDL3.SDL_CreateWindow("GLCS Test", 800, 600, SDL_WindowFlags.SDL_WINDOW_HIDDEN | SDL_WindowFlags.SDL_WINDOW_OPENGL);
		var context = SDL3.SDL_GL_CreateContext(window);

		var closeRequested = false;

		SDL3.SDL_GL_MakeCurrent(window, context);
		var gl = new GL(static proc => SDL3.SDL_GL_GetProcAddress(proc));

		var vbo = new GLBuffer(gl);
		var vao = new GLVertexArray(gl);
		var program = new GLProgram(gl);

		using (var vertexShader = new GLShader(gl, GLShaderType.Vertex))
		using (var fragmentShader = new GLShader(gl, GLShaderType.Fragment))
		{
			vertexShader.Compile(vertexShaderSource_);
			fragmentShader.Compile(fragmentShaderSource_);
			program.Link(vertexShader, fragmentShader);
		}

		vbo.Data(vertices_, GLBufferUsage.StaticDraw);
		vao.VertexAttribPointers<VertexAttribs>(vbo);

		SDL3.SDL_ShowWindow(window);

		while (!closeRequested)
		{
			SDL_Event ev;
			while (SDL3.SDL_PollEvent(&ev))
			{
				switch ((SDL_EventType)ev.type)
				{
					case SDL_EventType.SDL_EVENT_WINDOW_CLOSE_REQUESTED:
						closeRequested = true;
						break;
				}
			}

			gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
			gl.Clear(GL_COLOR_BUFFER_BIT);
			vao.Draw(GLDrawMode.Triangles, 0, 3, program);
			SDL3.SDL_GL_SwapWindow(window);
		}

		program.Dispose();
		vao.Dispose();
		vbo.Dispose();

		SDL3.SDL_GL_DestroyContext(context);
		SDL3.SDL_DestroyWindow(window);
	}
}
