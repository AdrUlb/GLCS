using System.Drawing;
using System.Runtime.CompilerServices;

namespace GLCS.Managed;

public sealed unsafe class GLRenderbuffer : IDisposable
{
	public readonly uint Handle;

	public GLRenderbuffer()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.GenRenderbuffers(1, handlePtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Storage(InternalFormat internalFormat, Size size)
	{
		ManagedGL.Current.Unmanaged.BindRenderbuffer(RenderbufferTarget.Renderbuffer, Handle);
		ManagedGL.Current.Unmanaged.RenderbufferStorage(RenderbufferTarget.Renderbuffer, internalFormat, size.Width, size.Height);
		ManagedGL.Current.Unmanaged.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void StorageMultisample(int samples, InternalFormat internalFormat, Size size)
	{
		ManagedGL.Current.Unmanaged.BindRenderbuffer(RenderbufferTarget.Renderbuffer, Handle);
		ManagedGL.Current.Unmanaged.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, samples, internalFormat, size.Width, size.Height);
		ManagedGL.Current.Unmanaged.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.DeleteRenderbuffers(1, handlePtr);
	}
}
