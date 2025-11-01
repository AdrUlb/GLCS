using System.Drawing;
using System.Runtime.CompilerServices;

namespace GLCS.Managed;

public unsafe sealed class GLRenderbuffer : IDisposable
{
	public readonly uint Handle;
	private readonly ManagedGL gl_;

	public GLRenderbuffer(ManagedGL gl)
	{
		gl_ = gl;
		fixed (uint* handlePtr = &Handle)
			gl.Unmanaged.GenRenderbuffers(1, handlePtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void StorageMultisample(int samples, InternalFormat internalFormat, Size size)
	{
		gl_.Unmanaged.BindRenderbuffer(RenderbufferTarget.Renderbuffer, Handle);
		gl_.Unmanaged.RenderbufferStorageMultisample(RenderbufferTarget.Renderbuffer, samples, internalFormat, size.Width, size.Height);
		gl_.Unmanaged.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			gl_.Unmanaged.DeleteRenderbuffers(1, handlePtr);
	}
}
