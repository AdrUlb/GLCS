namespace GLCS.Managed;

public unsafe sealed class GLBuffer<T> : IDisposable where T : unmanaged
{
	public readonly uint Handle;

	public GLBuffer()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.GenBuffers(1, handlePtr);
	}

	public void Data(nint size, BufferUsageARB usage)
	{
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, Handle);
		ManagedGL.Current.Unmanaged.BufferData(BufferTargetARB.ArrayBuffer, size, (void*)0, usage);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
	}

	public void Data(ReadOnlySpan<T> data, BufferUsageARB usage)
	{
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, Handle);
		fixed (T* dataPtr = data)
			ManagedGL.Current.Unmanaged.BufferData(BufferTargetARB.ArrayBuffer, data.Length * sizeof(T), dataPtr, usage);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
	}

	public void SubData(nint offset, ReadOnlySpan<T> data)
	{
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, Handle);
		fixed (T* dataPtr = data)
			ManagedGL.Current.Unmanaged.BufferSubData(BufferTargetARB.ArrayBuffer, offset, data.Length * sizeof(T), dataPtr);
		ManagedGL.Current.Unmanaged.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.DeleteBuffers(1, handlePtr);
	}
}
