using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class Sdl
{
	private static partial class Native
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetError(string str);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_OutOfMemory();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		public static partial nint SDL_GetError();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_ClearError();
	}

	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetError(string str) => Native.SDL_SetError(str);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool OutOfMemory() => Native.SDL_OutOfMemory();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetError() => Marshal.PtrToStringUTF8(Native.SDL_GetError())!;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ClearError() => Native.SDL_ClearError();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Unsupported() => Native.SDL_SetError("That operation is not supported");

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool InvalidParamError(string param) => Native.SDL_SetError($"Parameter '{param}' is invalid");

}
