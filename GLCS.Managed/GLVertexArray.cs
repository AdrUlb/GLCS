using System.Diagnostics.CodeAnalysis;

namespace GLCS.Managed;

public unsafe sealed class GLVertexArray : IDisposable
{
	public readonly uint Handle;
	private readonly ManagedGL gl_;

	public GLVertexArray(ManagedGL gl)
	{
		gl_ = gl;
		fixed (uint* handlePtr = &Handle)
			gl.Unmanaged.GenVertexArrays(1, handlePtr);
	}

	public void VertexAttribPointer<T>(uint index, int size, VertexAttribPointerType type, bool normalized, int stride, nint offset, GLBuffer<T> vertexBuffer) where T : unmanaged
	{
		gl_.Unmanaged.BindVertexArray(Handle);
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, vertexBuffer.Handle);
		gl_.Unmanaged.VertexAttribPointer(index, size, type, normalized, stride, (void*)offset);
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
		gl_.Unmanaged.EnableVertexAttribArray(0);
		gl_.Unmanaged.BindVertexArray(0);

	}

	public void VertexAttribPointers<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] T>(GLBuffer<T> vertexBuffer) where T : unmanaged
	{
		gl_.Unmanaged.BindVertexArray(Handle);
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, vertexBuffer.Handle);
		new GLVertexAttribsInfo<T>().VertexAttribPointers(gl_);
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
		gl_.Unmanaged.EnableVertexAttribArray(0);
		gl_.Unmanaged.BindVertexArray(0);

	}

	public void Draw(PrimitiveType mode, int first, int count, GLProgram program)
	{
		gl_.Unmanaged.UseProgram(program.Handle);
		gl_.Unmanaged.BindVertexArray(Handle);
		gl_.Unmanaged.DrawArrays(mode, first, count);
		gl_.Unmanaged.BindVertexArray(0);
		gl_.Unmanaged.UseProgram(0);
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			gl_.Unmanaged.DeleteVertexArrays(1, handlePtr);
	}
}
