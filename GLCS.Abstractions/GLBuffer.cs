using static GLCS.GL;

namespace GLCS.Abstractions;

public sealed class GLBuffer(GL gl) : IDisposable
{
	public readonly uint Handle = gl.GenBuffer();

	public void Data<T>(ReadOnlySpan<T> data, GLBufferUsage usage) where T : unmanaged
	{
		gl.BindBuffer(GL_ARRAY_BUFFER, Handle);
		gl.BufferData(GL_ARRAY_BUFFER, data, (uint)usage);
		gl.BindBuffer(GL_ARRAY_BUFFER, 0);
	}

	public void Dispose() => gl.DeleteBuffer(Handle);
	public static implicit operator uint(GLBuffer buffer) => buffer.Handle;
}
