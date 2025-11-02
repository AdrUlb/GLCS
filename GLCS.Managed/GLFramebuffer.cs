using System.Drawing;
using System.Runtime.CompilerServices;

namespace GLCS.Managed;

public unsafe sealed class GLFramebuffer : IDisposable
{
	public readonly uint Handle;
	private readonly ManagedGL gl_;

	public GLFramebuffer(ManagedGL gl)
	{
		gl_ = gl;
		fixed (uint* handlePtr = &Handle)
			gl.Unmanaged.GenFramebuffers(1, handlePtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void BindDraw() => gl_.Unmanaged.BindFramebuffer(FramebufferTarget.DrawFramebuffer, Handle);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void UnbindDraw() => gl_.Unmanaged.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Texture2D(FramebufferAttachment attachment, TextureTarget textureTarget, GLTexture texture, int level)
	{
		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
		gl_.Unmanaged.FramebufferTexture2D(FramebufferTarget.Framebuffer, attachment, textureTarget, texture.Handle, level);
		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Renderbuffer(FramebufferAttachment attachment, RenderbufferTarget renderbufferTarget, GLRenderbuffer renderbuffer)
	{
		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
		gl_.Unmanaged.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, attachment, renderbufferTarget, renderbuffer.Handle);
		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public FramebufferStatus CheckStatus()
	{
		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
		var result = gl_.Unmanaged.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

		return (FramebufferStatus)result;
	}

	public void Blit(GLFramebuffer? drawBuffer, Rectangle sourceRect, Rectangle destRect, ClearBufferMask mask, BlitFramebufferFilter filter)
	{
		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
		drawBuffer?.BindDraw();

		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.ReadFramebuffer, Handle);
		gl_.Unmanaged.BlitFramebuffer(sourceRect.X, sourceRect.Y, sourceRect.Width, sourceRect.Height, destRect.X, destRect.Y, destRect.Width, destRect.Height, mask, filter);
		gl_.Unmanaged.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);

		drawBuffer?.UnbindDraw();
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			gl_.Unmanaged.DeleteFramebuffers(1, handlePtr);
	}
}
