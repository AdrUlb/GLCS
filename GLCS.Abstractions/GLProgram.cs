using static GLCS.GL;

namespace GLCS.Abstractions;

public sealed class GLProgram(GL gl) : IDisposable
{
	public readonly uint Handle = gl.CreateProgram();

	public void Link(params ReadOnlySpan<uint> shaders)
	{
		foreach (var shader in shaders)
			gl.AttachShader(Handle, shader);

		gl.LinkProgram(Handle);
		if (gl.GetProgram(Handle, ProgramPropertyARB.GL_LINK_STATUS) == GL_FALSE)
			throw new($"Program linking failed: {gl.GetProgramInfoLog(Handle)}.");

		foreach (var shader in shaders)
			gl.DetachShader(Handle, shader);
	}

	public void Dispose() => gl.DeleteProgram(Handle);
	public static implicit operator uint(GLProgram buffer) => buffer.Handle;
}
