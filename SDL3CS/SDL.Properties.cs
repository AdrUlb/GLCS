using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class SDL
{
	public enum PropertyType : uint
	{
		Invalid,
		Pointer,
		String,
		Number,
		Float,
		Boolean
	}

	public readonly struct PropertiesID
	{
		private readonly uint value_;
	}

	public static partial class Prop
	{
		public const string NameString = "SDL.name";
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void CleanupPropertyCallback(nint userdata, nint value);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void EnumeratePropertiesCallback(nint userdata, PropertiesID props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name);

	private static partial class Native
	{

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial PropertiesID SDL_GetGlobalProperties();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial PropertiesID SDL_CreateProperties();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_CopyProperties(PropertiesID src, PropertiesID dst);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_LockProperties(PropertiesID props);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_UnlockProperties(PropertiesID props);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_SetPointerPropertyWithCleanup(PropertiesID props, string name, nint value, CleanupPropertyCallback cleanup, nint userdata);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_SetPointerProperty(PropertiesID props, string name, nint value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_SetStringProperty(PropertiesID props, string name, string value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_SetNumberProperty(PropertiesID props, string name, long value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_SetFloatProperty(PropertiesID props, string name, float value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_SetBooleanProperty(PropertiesID props, string name, [MarshalAs(UnmanagedType.I1)] bool value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_HasProperty(PropertiesID props, string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial PropertyType SDL_GetPropertyType(PropertiesID props, string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_GetPointerProperty(PropertiesID props, string name, nint defaultValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_GetStringProperty(PropertiesID props, string name, string defaultValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial long SDL_GetNumberProperty(PropertiesID props, string name, long defaultValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial float SDL_GetFloatProperty(PropertiesID props, string name, float defaultValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_GetBooleanProperty(PropertiesID props, string name, [MarshalAs(UnmanagedType.I1)] bool defaultValue);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_ClearProperty(PropertiesID props, string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial void SDL_EnumerateProperties(PropertiesID props, EnumeratePropertiesCallback callback, nint userdata);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_DestroyProperties(PropertiesID props);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PropertiesID GetGlobalProperties() => Native.SDL_GetGlobalProperties();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PropertiesID CreateProperties() => Native.SDL_CreateProperties();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool CopyProperties(PropertiesID src, PropertiesID dst) => Native.SDL_CopyProperties(src, dst);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool LockProperties(PropertiesID props) => Native.SDL_LockProperties(props);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void UnlockProperties(PropertiesID props) => Native.SDL_UnlockProperties(props);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetPointerPropertyWithCleanup(PropertiesID props, string name, nint value, CleanupPropertyCallback cleanup, nint userdata) => Native.SDL_SetPointerPropertyWithCleanup(props, name, value, cleanup, userdata);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetPointerProperty(PropertiesID props, string name, nint value) => Native.SDL_SetPointerProperty(props, name, value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetStringProperty(PropertiesID props, string name, string value) => Native.SDL_SetStringProperty(props, name, value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetNumberProperty(PropertiesID props, string name, long value) => Native.SDL_SetNumberProperty(props, name, value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetFloatProperty(PropertiesID props, string name, float value) => Native.SDL_SetFloatProperty(props, name, value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetBooleanProperty(PropertiesID props, string name, [MarshalAs(UnmanagedType.I1)] bool value) => Native.SDL_SetBooleanProperty(props, name, value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void HasProperty(PropertiesID props, string name) => Native.SDL_HasProperty(props, name);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PropertyType GetPropertyType(PropertiesID props, string name) => Native.SDL_GetPropertyType(props, name);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static nint GetPointerProperty(PropertiesID props, string name, nint defaultValue) => Native.SDL_GetPointerProperty(props, name, defaultValue);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetStringProperty(PropertiesID props, string name, string defaultValue) => Native.SDL_GetStringProperty(props, name, defaultValue);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static long GetNumberProperty(PropertiesID props, string name, long defaultValue) => Native.SDL_GetNumberProperty(props, name, defaultValue);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetFloatProperty(PropertiesID props, string name, float defaultValue) => Native.SDL_GetFloatProperty(props, name, defaultValue);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GetBooleanProperty(PropertiesID props, string name, [MarshalAs(UnmanagedType.I1)] bool defaultValue) => Native.SDL_GetBooleanProperty(props, name, defaultValue);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ClearProperty(PropertiesID props, string name) => Native.SDL_ClearProperty(props, name);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void EnumerateProperties(PropertiesID props, EnumeratePropertiesCallback callback, nint userdata) => Native.SDL_EnumerateProperties(props, callback, userdata);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DestroyProperties(PropertiesID props) => Native.SDL_DestroyProperties(props);
}
