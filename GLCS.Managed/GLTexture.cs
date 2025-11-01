using System.Drawing;
using System.Runtime.CompilerServices;

namespace GLCS.Managed;

public unsafe sealed class GLTexture : IDisposable
{
	public readonly uint Handle;
	private readonly ManagedGL gl_;

	public GLTexture(ManagedGL gl)
	{
		gl_ = gl;
		fixed (uint* handlePtr = &Handle)
			gl.Unmanaged.GenTextures(1, handlePtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Image2DMultisample(int samples, InternalFormat internalFormat, Size size, bool fixedSampleLocations)
	{
		gl_.Unmanaged.BindTexture(TextureTarget.Texture2dMultisample, Handle);
		gl_.Unmanaged.TexImage2DMultisample(TextureTarget.Texture2dMultisample, samples, internalFormat, size.Width, size.Height, fixedSampleLocations);
		gl_.Unmanaged.BindTexture(TextureTarget.Texture2dMultisample, 0);
	}

	public void Dispose()
    {
		fixed (uint* handlePtr = &Handle)
			gl_.Unmanaged.DeleteTextures(1, handlePtr);
	}
}
