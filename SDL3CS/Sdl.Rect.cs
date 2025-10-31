using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class Sdl
{
	public struct Point
	{
		public int X;
		public int Y;
	}

	public struct FPoint
	{
		public float X;
		public float Y;
	}

	public struct Rect
	{
		public int X, Y;
		public int Width, Height;
	}

	public struct FRect
	{
		public float X;
		public float Y;
		public float Width;
		public float Height;
	}

	private static partial class Native
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_HasRectIntersection(in Rect a, in Rect b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetRectIntersection(in Rect a, in Rect b, out Rect result);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetRectUnion(in Rect a, in Rect b, out Rect result);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetRectEnclosingPoints(in Point points, int count, in Rect clip, out Rect result);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetRectAndLineIntersection(in Rect rect, ref int x1, ref int y1, ref int x2, ref int y2);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_HasRectIntersectionFloat(in FRect a, in FRect b);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetRectIntersectionFloat(in FRect a, in FRect b, out FRect result);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetRectUnionFloat(in FRect a, in FRect b, out FRect result);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetRectEnclosingPointsFloat(in FPoint points, int count, in FRect clip, out FRect result);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetRectAndLineIntersectionFloat(in FRect rect, ref float x1, ref float y1, ref float x2, ref float y2);
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool HasRectIntersection(in Rect a, in Rect b) => Native.SDL_HasRectIntersection(a, b);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetRectIntersection(in Rect a, in Rect b, out Rect result) => Native.SDL_GetRectIntersection(a, b, out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetRectUnion(in Rect a, in Rect b, out Rect result) => Native.SDL_GetRectUnion(a, b, out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetRectEnclosingPoints(in Point points, int count, in Rect clip, out Rect result) => Native.SDL_GetRectEnclosingPoints(points, count, clip, out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetRectAndLineIntersection(in Rect rect, ref int x1, ref int y1, ref int x2, ref int y2) => Native.SDL_GetRectAndLineIntersection(rect, ref x1, ref y1, ref x2, ref y2);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool HasRectIntersectionFloat(in FRect a, in FRect b) => Native.SDL_HasRectIntersectionFloat(a, b);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetRectIntersectionFloat(in FRect a, in FRect b, out FRect result) => Native.SDL_GetRectIntersectionFloat(a, b, out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetRectUnionFloat(in FRect a, in FRect b, out FRect result) => Native.SDL_GetRectUnionFloat(a, b, out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetRectEnclosingPointsFloat(in FPoint points, int count, in FRect clip, out FRect result) => Native.SDL_GetRectEnclosingPointsFloat(points, count, clip, out result);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetRectAndLineIntersectionFloat(in FRect rect, ref float x1, ref float y1, ref float x2, ref float y2) => Native.SDL_GetRectAndLineIntersectionFloat(rect, ref x1, ref y1, ref x2, ref y2);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SDL_RectToFRect(in Rect rect, out FRect frect)
	{
		frect = new()
		{
			X = rect.X,
			Y = rect.Y,
			Width = rect.Width,
			Height = rect.Height
		};
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SDL_PointInRect(in Point p, in Rect r) => (p.X >= r.X) && (p.X < (r.X + r.Width)) && (p.Y >= r.Y) && (p.Y < (r.Y + r.Height));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SDL_RectEmpty(in Rect r) => r.Width <= 0 || r.Height <= 0;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SDL_RectsEqual(in Rect a, in Rect b) => (a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SDL_PointInRectFloat(in FPoint p, in FRect r) => (p.X >= r.X) && (p.X <= (r.X + r.Width)) && (p.Y >= r.Y) && (p.Y <= (r.Y + r.Height));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SDL_RectEmptyFloat(in FRect r) => (r.Width < 0.0f) || (r.Height < 0.0f);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SDL_RectsEqualEpsilon(in FRect a, in FRect b, float epsilon) =>
		(float.Abs(a.X - b.X) <= epsilon) &&
		(float.Abs(a.Y - b.Y) <= epsilon) &&
		(float.Abs(a.Width - b.Width) <= epsilon) &&
		(float.Abs(a.Height - b.Height) <= epsilon);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SDL_RectsEqualFloat(in FRect a, in FRect b) => SDL_RectsEqualEpsilon(a, b, float.Epsilon);
}
