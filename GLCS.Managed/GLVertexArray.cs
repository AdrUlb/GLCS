using System.Diagnostics.CodeAnalysis;

namespace GLCS.Managed;

public unsafe sealed class GLVertexArray : IDisposable
{
	public readonly uint Handle;

	public GLVertexArray()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.GenVertexArrays(1, handlePtr);
	}

	public void VertexAttribPointer<T>(uint index, int size, VertexAttribPointerType type, bool normalized, int stride, nint offset, GLBuffer<T> arrayBuffer) where T : unmanaged
	{
		ManagedGL.Current.Unmanaged.BindVertexArray(Handle);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, arrayBuffer.Handle);
		ManagedGL.Current.Unmanaged.VertexAttribPointer(index, size, type, normalized, stride, (void*)offset);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
		ManagedGL.Current.Unmanaged.EnableVertexAttribArray(0);
		ManagedGL.Current.Unmanaged.BindVertexArray(0);

	}


	public void VertexAttribPointers<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] T>(GLBuffer<T> arrayBuffer) where T : unmanaged
	{
		ManagedGL.Current.Unmanaged.BindVertexArray(Handle);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, arrayBuffer.Handle);
		new GLVertexAttribsInfo<T>().VertexAttribPointers(ManagedGL.Current);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
		ManagedGL.Current.Unmanaged.EnableVertexAttribArray(0);
		ManagedGL.Current.Unmanaged.BindVertexArray(0);

	}

	public void Draw(PrimitiveType mode, int first, int count, GLProgram program)
	{
		ManagedGL.Current.Unmanaged.UseProgram(program.Handle);
		ManagedGL.Current.Unmanaged.BindVertexArray(Handle);
		ManagedGL.Current.Unmanaged.DrawArrays(mode, first, count);
		ManagedGL.Current.Unmanaged.BindVertexArray(0);
		ManagedGL.Current.Unmanaged.UseProgram(0);
	}

	public void DrawElements<T>(PrimitiveType mode, GLBuffer<T> elementArray, int count, DrawElementsType type, GLProgram program) where T : unmanaged
	{
		ManagedGL.Current.Unmanaged.UseProgram(program.Handle);
		ManagedGL.Current.Unmanaged.BindVertexArray(Handle);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ElementArrayBuffer, elementArray.Handle);
		ManagedGL.Current.Unmanaged.DrawElements(mode, count, type, (void*)0);
		ManagedGL.Current.Unmanaged.BindVertexArray(0);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
		ManagedGL.Current.Unmanaged.UseProgram(0);
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.DeleteVertexArrays(1, handlePtr);
	}
}
