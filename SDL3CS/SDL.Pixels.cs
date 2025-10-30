using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class Sdl
{
	public const byte AlphaOpaque = 255;
	public const float AlphaOpaqueFloat = 1.0f;
	public const byte AlphaTransparent = 0;
	public const float AlphaTransparentFloat = 0.0f;

	public enum PixelType
	{
		Unknown,
		Index1,
		Index4,
		Index8,
		Packed8,
		Packed16,
		Packed32,
		ArrayU8,
		ArrayU16,
		ArrayU32,
		ArrayF16,
		ArrayF32,
		Index2
	}

	public enum BitmapOrder
	{
		None,
		Order4321,
		Order1234
	}

	public enum PackedOrder
	{
		None,
		Xrgb,
		Rgbx,
		Argb,
		Rgba,
		Xbgr,
		Bgrx,
		Abgr,
		Bgra
	}

	public enum ArrayOrder
	{
		None,
		Rgb,
		Rgba,
		Argb,
		Bgr,
		Bgra,
		Abgr
	}

	public enum PackedLayout
	{
		None,
		Layout332,
		Layout4444,
		Layout1555,
		Layout5551,
		Layout565,
		Layout8888,
		Layout2101010,
		Layout1010102
	}

	public enum PixelFormat
	{
		Unknown = 0,
		Index1LSB = 0x11100100,
		Index1MSB = 0x11200100,
		Index2LSB = 0x1c100200,
		Index2MSB = 0x1c200200,
		Index4LSB = 0x12100400,
		Index4MSB = 0x12200400,
		Index8 = 0x13000801,
		Rgb332 = 0x14110801,
		Xrgb4444 = 0x15120c02,
		Xbgr4444 = 0x15520c02,
		Xrgb1555 = 0x15130f02,
		Xbgr1555 = 0x15530f02,
		Argb4444 = 0x15321002,
		Rgba4444 = 0x15421002,
		Abgr4444 = 0x15721002,
		Bgra4444 = 0x15821002,
		Argb1555 = 0x15331002,
		Rgba5551 = 0x15441002,
		Abgr1555 = 0x15731002,
		Bgra5551 = 0x15841002,
		Rgb565 = 0x15151002,
		Bgr565 = 0x15551002,
		Rgb24 = 0x17101803,
		Xrgb8888 = 0x16161804,
		Rgbx8888 = 0x16261804,
		Xbgr8888 = 0x16561804,
		Bgrx8888 = 0x16661804,
		Argb8888 = 0x16362004,
		Rgba8888 = 0x16462004,
		Abgr8888 = 0x16762004,
		Bgra8888 = 0x16862004,
		Xbgr2101010 = 0x16572004,
		Argb2101010 = 0x16372004,
		Abgr2101010 = 0x16772004,
		Rgb48 = 0x18103006,
		Bgr48 = 0x18403006,
		Rgba64 = 0x18204008,
		Argb64 = 0x18304008,
		Bgra64 = 0x18504008,
		Abgr64 = 0x18604008,
		Rgb48Float = 0x1a103006,
		Bgr48Float = 0x1a403006,
		Argb64Float = 0x1a304008,
		Bgra64Float = 0x1a504008,
		Abgr64Float = 0x1a604008,
		Rgb96Float = 0x1b10600c,
		Bgr96Float = 0x1b40600c,
		Rgba128Float = 0x1b208010,
		Argb128Float = 0x1b308010,
		Bgra128Float = 0x1b508010,
		Abgr128Float = 0x1b608010,
		Yc12 = 0x32315659,
		Iyuc = 0x56555949,
		yuy2 = 0x32595559,
		Uyvy = 0x59565955,
		Yvyu = 0x55595659,
		Nv12 = 0x3231564e,
		Nv21 = 0x3132564e,
		P010 = 0x30313050,
		ExternalOes = 0x2053454f,
		Mjpg = 0x47504a4d,
		Rgba32 = Abgr8888,
		Argb32 = Bgra8888,
		Bgra32 = Argb8888,
		Abgr32 = Rgba8888,
		Rgbx32 = Xbgr8888,
		Xrgb32 = Bgrx8888,
		Bgrx32 = Xrgb8888,
		Xbgr32 = Rgbx8888
	}

	public enum ColorType
	{
		Unknown = 0,
		Rgb = 1,
		Ycbcr = 2
	}

	public enum ColorRange
	{
		Unknown = 0,
		Limited = 1,
		Full = 2
	}

	public enum ColorPrimaries
	{
		Unknown = 0,
		Bt709 = 1,
		Unspecified = 2,
		Bt470M = 4,
		Bt470Bg = 5,
		Bt601 = 6,
		Smpte240 = 7,
		GenericFilm = 8,
		Bt2020 = 9,
		Xyz = 10,
		Smpte431 = 11,
		Smpte432 = 12,
		Ebu3213 = 22,
		Custom = 31
	}

	public enum TransferCharacteristics
	{
		Unknown = 0,
		Bt709 = 1,
		Unspecified = 2,
		Gamma22 = 4,
		Gamma28 = 5,
		Bt601 = 6,
		Smpte240 = 7,
		Linear = 8,
		Log100 = 9,
		Log100Sqrt10 = 10,
		Iec61966 = 11,
		Bt1361 = 12,
		Srgb = 13,
		Bt2020_10Bit = 14,
		Bt2020_12Bit = 15,
		Pq = 16,
		Smpte428 = 17,
		Hlg = 18,
		Custom = 31
	}

	public enum MatrixCoefficients
	{
		Identity = 0,
		Bt709 = 1,
		Unspecified = 2,
		Fcc = 4,
		Bt470Bg = 5,
		Bt601 = 6,
		Smpte240 = 7,
		YcgcO = 8,
		Bt2020Ncl = 9,
		Bt2020Cl = 10,
		Smpte2085 = 11,
		ChromaDerivedNcl = 12,
		ChromaDerivedCl = 13,
		Ictcp = 14,
		Custom = 31
	}

	public enum ChromaLocation
	{
		None = 0,
		Left = 1,
		Center = 2,
		TopLeft = 3
	}

	public enum Colorspace
	{
		Unknown = 0,
		Srgb = 0x120005a0,
		SrgbLinear = 0x12000500,
		HDR10 = 0x12002600,
		JPEG = 0x220004c6,
		Bt601Limited = 0x211018c6,
		Bt601Full = 0x221018c6,
		Bt709Limited = 0x21100421,
		Bt709Full = 0x22100421,
		Bt2020Limited = 0x21102609,
		Bt2020Full = 0x22102609,
		RgbDefault = Srgb,
		YuvDefault = Bt601Limited
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct Color
	{
		public byte R;
		public byte G;
		public byte B;
		public byte A;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct FColor
	{
		public float R;
		public float G;
		public float B;
		public float A;
	}

	[StructLayout(LayoutKind.Sequential)]
	public readonly unsafe struct Palette
	{
		private readonly int numColors_;
		private readonly Color* colors_;
		private readonly uint version_;
		private readonly int refCount_;

		public Span<Color> Colors
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new(colors_, numColors_);
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct PixelFormatDetails
	{
		public PixelFormat Format;
		public byte BitsPerPixel;
		public byte BytesPerPixel;
		private fixed byte padding_[2];
		public uint RMask;
		public uint GMask;
		public uint BMask;
		public uint AMask;
		public byte RBits;
		public byte GBits;
		public byte BBits;
		public byte ABits;
		public byte RShift;
		public byte GShift;
		public byte BShift;
		public byte AShift;
	}

	private static partial class Native
	{

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_GetPixelFormatName(PixelFormat format);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetMasksForPixelFormat(PixelFormat format, out int bpp, out uint rmask, out uint gmask, out uint bmask, out uint amask);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial PixelFormat SDL_GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask, uint amask);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<PixelFormatDetails> SDL_GetPixelFormatDetails(PixelFormat format);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Palette> SDL_CreatePalette(int ncolors);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetPaletteColors(in Palette palette, in Color colors, int firstcolor, int ncolors);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_DestroyPalette(in Palette palette);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial uint SDL_MapRGB(in PixelFormatDetails format, in Palette palette, byte r, byte g, byte b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial uint SDL_MapRGBA(in PixelFormatDetails format, in Palette palette, byte r, byte g, byte b, byte a);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_GetRGB(uint pixelvalue, in PixelFormatDetails format, in Palette palette, out byte r, out byte g, out byte b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_GetRGBA(uint pixelvalue, in PixelFormatDetails format, in Palette palette, out byte r, out byte g, out byte b, out byte a);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetPixelFormatName(PixelFormat format) => Native.SDL_GetPixelFormatName(format);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetMasksForPixelFormat(PixelFormat format, out int bpp, out uint rmask, out uint gmask, out uint bmask, out uint amask) => Native.SDL_GetMasksForPixelFormat(format, out bpp, out rmask, out gmask, out bmask, out amask);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PixelFormat GetPixelFormatForMasks(int bpp, uint rmask, uint gmask, uint bmask, uint amask) => Native.SDL_GetPixelFormatForMasks(bpp, rmask, gmask, bmask, amask);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<PixelFormatDetails> GetPixelFormatDetails(PixelFormat format) => Native.SDL_GetPixelFormatDetails(format);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Palette> CreatePalette(int ncolors) => Native.SDL_CreatePalette(ncolors);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetPaletteColors(in Palette palette, in Color colors, int firstcolor, int ncolors) => Native.SDL_SetPaletteColors(palette, colors, firstcolor, ncolors);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DestroyPalette(in Palette palette) => Native.SDL_DestroyPalette(palette);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint MapRGB(in PixelFormatDetails format, in Palette palette, byte r, byte g, byte b) => Native.SDL_MapRGB(format, palette, r, g, b);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint MapRGBA(in PixelFormatDetails format, in Palette palette, byte r, byte g, byte b, byte a) => Native.SDL_MapRGBA(format, palette, r, g, b, a);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GetRGB(uint pixelvalue, in PixelFormatDetails format, in Palette palette, out byte r, out byte g, out byte b) => Native.SDL_GetRGB(pixelvalue, format, palette, out r, out g, out b);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GetRGBA(uint pixelvalue, in PixelFormatDetails format, in Palette palette, out byte r, out byte g, out byte b, out byte a) => Native.SDL_GetRGBA(pixelvalue, format, palette, out r, out g, out b, out a);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PixelFormat DefinePixelFourCc(char a, char b, char c, char d) => (PixelFormat)DefineFourCc(a, b, c, d);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PixelFormat DefinePixelFormat(PixelType type, uint order, PackedLayout layout, byte bits, byte bytes) =>
		(PixelFormat)((1u << 28) | ((uint)type << 24) | (order << 20) | ((uint)layout << 16) | ((uint)bits << 8) | ((uint)bytes << 0));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint GetFormatPixelFlag(PixelFormat format) => ((uint)format >> 28) & 0x0F;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PixelType GetFormatPixelType(PixelFormat format) => (PixelType)((((uint)format) >> 24) & 0x0F);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint GetFormatPixelOrder(PixelFormat format) => ((uint)format >> 20) & 0x0F;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PackedLayout GetFormatPixelLayout(PixelFormat format) => (PackedLayout)((((uint)format) >> 16) & 0x0F);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte GetFormatBitsPerPixel(PixelFormat format) => (byte)(IsPixelFormatFourCc(format) ? 0 : ((((uint)format) >> 8) & 0xFF));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte GetFormatBytesPerPixel(PixelFormat format) => IsPixelFormatFourCc(format)
		? (byte)((format is PixelFormat.yuy2 or PixelFormat.Uyvy or PixelFormat.Yvyu or PixelFormat.P010) ? 2 : 1)
		: (byte)(((uint)format >> 0) & 0xFF);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPixelFormatIndexed(PixelFormat format) => !IsPixelFormatFourCc(format) &&
		GetFormatPixelType(format) is PixelType.Index1 or PixelType.Index2 or PixelType.Index4 or PixelType.Index8;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPixelFormatPacked(PixelFormat format) => !IsPixelFormatFourCc(format) &&
		GetFormatPixelType(format) is PixelType.Packed8 or PixelType.Packed16 or PixelType.Packed32;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPixelFormatArray(PixelFormat format) => !IsPixelFormatFourCc(format) &&
		GetFormatPixelType(format) is PixelType.ArrayU8 or PixelType.ArrayU16 or PixelType.ArrayU32 or PixelType.ArrayF16 or PixelType.ArrayF32;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPixelFormat10Bit(PixelFormat format) => !IsPixelFormatFourCc(format) &&
		GetFormatPixelType(format) == PixelType.Packed32 && GetFormatPixelLayout(format) == PackedLayout.Layout2101010;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPixelFormatFloat(PixelFormat format) => !IsPixelFormatFourCc(format) &&
		GetFormatPixelType(format) is PixelType.ArrayF16 or PixelType.ArrayF32;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPixelFormatAlpha(PixelFormat format) =>
		(IsPixelFormatPacked(format) && ((PackedOrder)GetFormatPixelOrder(format) is PackedOrder.Argb or PackedOrder.Rgba or PackedOrder.Abgr or PackedOrder.Bgra)) ||
		(IsPixelFormatArray(format) && ((ArrayOrder)GetFormatPixelOrder(format) is ArrayOrder.Argb or ArrayOrder.Rgba or ArrayOrder.Abgr or ArrayOrder.Bgra));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsPixelFormatFourCc(PixelFormat format) => format != 0 && (GetFormatPixelFlag(format) != 1);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Colorspace DefineColorspace(ColorType type, ColorRange range, ColorPrimaries primaries, TransferCharacteristics transfer, MatrixCoefficients matrix, ChromaLocation chroma) =>
		(Colorspace)(((uint)type << 28) | ((uint)range << 24) | ((uint)chroma << 20) | ((uint)primaries << 10) | ((uint)transfer << 5) | ((uint)matrix << 0));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ColorType GetColorspaceType(Colorspace cspace) => (ColorType)(((uint)cspace >> 28) & 0x0F);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ColorRange GetColorspaceRange(Colorspace cspace) => (ColorRange)(((uint)cspace >> 24) & 0x0F);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ChromaLocation GetColorspaceChroma(Colorspace cspace) => (ChromaLocation)(((uint)cspace >> 20) & 0x0F);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ColorPrimaries GetColorspacePrimaries(Colorspace cspace) => (ColorPrimaries)(((uint)cspace >> 10) & 0x1F);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TransferCharacteristics GetColorspaceTransfer(Colorspace cspace) => (TransferCharacteristics)(((uint)cspace >> 5) & 0x1F);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static MatrixCoefficients GetColorspaceMatrix(Colorspace cspace) => (MatrixCoefficients)((uint)cspace & 0x1F);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsColorspaceMatrixBt601(Colorspace cspace) => GetColorspaceMatrix(cspace) is MatrixCoefficients.Bt601 or MatrixCoefficients.Bt470Bg;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsColorspaceMatrixBt709(Colorspace cspace) => GetColorspaceMatrix(cspace) == MatrixCoefficients.Bt709;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsColorspaceMatrixBt2020Ncl(Colorspace cspace) => GetColorspaceMatrix(cspace) == MatrixCoefficients.Bt2020Ncl;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsColorspaceLimitedRange(Colorspace cspace) => GetColorspaceRange(cspace) != ColorRange.Full;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsColorspaceFullRange(Colorspace cspace) => GetColorspaceRange(cspace) == ColorRange.Full;
}
