using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace GLCS.Managed;

public unsafe sealed class GLTexture : IDisposable
{
	public readonly uint Handle;
	public readonly TextureTarget Target;

	public TextureWrapMode WrapS
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => SetParameter(TextureParameterName.TextureWrapS, (int)value);
	}

	public TextureWrapMode WrapT
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => SetParameter(TextureParameterName.TextureWrapT, (int)value);
	}

	public TextureMinFilter MinFilter
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => SetParameter(TextureParameterName.TextureMinFilter, (int)value);
	}

	public TextureMagFilter MagFilter
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set => SetParameter(TextureParameterName.TextureMagFilter, (int)value);
	}

	public GLTexture(TextureTarget target)
	{
		Target = target;
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.GenTextures(1, handlePtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Bind() => ManagedGL.Current.Unmanaged.BindTexture(Target, Handle);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Unbind() => ManagedGL.Current.Unmanaged.BindTexture(Target, 0);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void SetParameter(TextureParameterName param, int value)
	{
		Bind();
		ManagedGL.Current.Unmanaged.TexParameteri(Target, param, value);
		Unbind();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void SetParameter(TextureParameterName param, ReadOnlySpan<int> values)
	{
		Bind();
		fixed (int* valuesPtr = values)
			ManagedGL.Current.Unmanaged.TexParameteriv(Target, param, valuesPtr);

		Unbind();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Image2D(int level, InternalFormat internalFormat, Size size, int border, PixelFormat format, PixelType type)
	{
		Bind();
		ManagedGL.Current.Unmanaged.TexImage2D(Target, level, internalFormat, size.Width, size.Height, border, format, type, null);
		Unbind();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Image2D<T>(int level, InternalFormat internalFormat, Size size, int border, PixelFormat format, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged
	{
		Bind();
		fixed (T* pixelsPtr = pixels)
			ManagedGL.Current.Unmanaged.TexImage2D(Target, level, internalFormat, size.Width, size.Height, border, format, type, pixelsPtr);

		Unbind();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void SubImage2D<T>(int level, Rectangle rect, PixelFormat format, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged
	{
		Bind();
		fixed (T* pixelsPtr = pixels)
			ManagedGL.Current.Unmanaged.TexSubImage2D(Target, level, rect.X, rect.Y, rect.Width, rect.Height, format, type, pixelsPtr);

		Unbind();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Image2DMultisample(int samples, InternalFormat internalFormat, Size size, bool fixedSampleLocations)
	{
		Bind();
		ManagedGL.Current.Unmanaged.TexImage2DMultisample(Target, samples, internalFormat, size.Width, size.Height, fixedSampleLocations);
		Unbind();
	}

	public void GenerateMipmap()
	{
		Bind();
		ManagedGL.Current.Unmanaged.GenerateMipmap(Target);
		Unbind();
	}

	public void Dispose()
	{
		fixed (uint* handlePtr = &Handle)
			ManagedGL.Current.Unmanaged.DeleteTextures(1, handlePtr);
	}
}
