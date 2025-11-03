using System.Drawing;
using System.Runtime.CompilerServices;

namespace GLCS.Managed;

public unsafe sealed class GLFramebuffer : IDisposable
{
	public readonly uint Handle;

	public GLFramebuffer()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.GenFramebuffers(1, handlePtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void BindDraw() => ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.DrawFramebuffer, Handle);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void UnbindDraw() => ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Texture2D(FramebufferAttachment attachment, TextureTarget textureTarget, GLTexture texture, int level)
	{
		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
		ManagedGL.Current.Unmanaged.FramebufferTexture2D(FramebufferTarget.Framebuffer, attachment, textureTarget, texture.Handle, level);
		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Renderbuffer(FramebufferAttachment attachment, RenderbufferTarget renderbufferTarget, GLRenderbuffer renderbuffer)
	{
		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
		ManagedGL.Current.Unmanaged.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, attachment, renderbufferTarget, renderbuffer.Handle);
		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public FramebufferStatus CheckStatus()
	{
		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, Handle);
		var result = ManagedGL.Current.Unmanaged.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

		return (FramebufferStatus)result;
	}

	public void Blit(GLFramebuffer? drawBuffer, Rectangle sourceRect, Rectangle destRect, ClearBufferMask mask, BlitFramebufferFilter filter)
	{
		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
		drawBuffer?.BindDraw();

		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.ReadFramebuffer, Handle);
		ManagedGL.Current.Unmanaged.BlitFramebuffer(sourceRect.X, sourceRect.Y, sourceRect.Width, sourceRect.Height, destRect.X, destRect.Y, destRect.Width, destRect.Height, mask, filter);
		ManagedGL.Current.Unmanaged.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);

		drawBuffer?.UnbindDraw();
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.DeleteFramebuffers(1, handlePtr);
	}
}
