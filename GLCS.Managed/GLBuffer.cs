namespace GLCS.Managed;

public unsafe sealed class GLBuffer<T> : IDisposable where T : unmanaged
{
	public readonly uint Handle;
	private readonly ManagedGL gl_;

	public GLBuffer(ManagedGL gl)
	{
		gl_ = gl;
		fixed (uint* handlePtr = &Handle)
			gl.Unmanaged.GenBuffers(1, handlePtr);
	}

	public void Data(nint size, BufferUsageARB usage)
	{
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, Handle);
		gl_.Unmanaged.BufferData(BufferTargetARB.ArrayBuffer, size, (void*)0, usage);
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
	}

	public void Data(ReadOnlySpan<T> data, BufferUsageARB usage)
	{
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, Handle);
		fixed (T* dataPtr = data)
			gl_.Unmanaged.BufferData(BufferTargetARB.ArrayBuffer, data.Length * sizeof(T), dataPtr, usage);
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
	}

	public void SubData(nint offset, ReadOnlySpan<T> data)
	{
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, Handle);
		fixed (T* dataPtr = data)
			gl_.Unmanaged.BufferSubData(BufferTargetARB.ArrayBuffer, offset, data.Length * sizeof(T), dataPtr);
		gl_.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			gl_.Unmanaged.DeleteBuffers(1, handlePtr);
	}
}
