using System.Diagnostics.CodeAnalysis;

namespace GLCS.Abstractions;

public sealed class GLVertexArray(GL gl) : IDisposable
{
	public readonly uint Handle = gl.GenVertexArray();

	public void VertexAttribPointer(uint index, int size, VertexAttribPointerType type, bool normalized, int stride, nint offset, GLBuffer vertexBuffer)
	{
		gl.BindVertexArray(Handle);
		gl.BindBuffer(BufferTargetARB.GL_ARRAY_BUFFER, vertexBuffer);
		gl.VertexAttribPointer(index, size, type, normalized, stride, offset);
		gl.BindBuffer(BufferTargetARB.GL_ARRAY_BUFFER, 0);
		gl.EnableVertexAttribArray(0);
		gl.BindVertexArray(0);
		
	}

	public void VertexAttribPointers<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] T>(GLBuffer vertexBuffer) where T : struct
	{
		gl.BindVertexArray(Handle);
		gl.BindBuffer(BufferTargetARB.GL_ARRAY_BUFFER, vertexBuffer);
		new GLVertexAttribsInfo<T>().VertexAttribPointers(gl);
		gl.BindBuffer(BufferTargetARB.GL_ARRAY_BUFFER, 0);
		gl.EnableVertexAttribArray(0);
		gl.BindVertexArray(0);
		
	}

	public void Draw(PrimitiveType mode, int first, int count, GLProgram program)
	{
		gl.UseProgram(program);
		gl.BindVertexArray(Handle);
		gl.DrawArrays(mode, first, count);
		gl.BindVertexArray(0);
		gl.UseProgram(0);
	}

    public void Dispose() => gl.DeleteVertexArray(Handle);
	public static implicit operator uint(GLVertexArray buffer) => buffer.Handle;
}
