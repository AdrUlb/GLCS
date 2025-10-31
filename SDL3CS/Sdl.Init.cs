using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class Sdl
{
	public enum InitFlags : uint
	{
		Audio = 0x00000010u,
		Video = 0x00000020u,
		Joystick = 0x00000200u,
		Haptic = 0x00001000u,
		Gamepad = 0x00002000u,
		Events = 0x00004000u,
		Sensor = 0x00008000u,
		Camera = 0x00010000u,
	}

	public enum AppResult
	{
		Continue,
		Success,
		Failure
	}

	public static partial class Prop
	{
		public static partial class App
		{
			public const string NameString = "SDL.app.metadata.name";
			public const string VersionString = "SDL.app.metadata.version";
			public const string IdentifierString = "SDL.app.metadata.identifier";
			public const string CreatorString = "SDL.app.metadata.creator";
			public const string CopyrightString = "SDL.app.metadata.copyright";
			public const string UrlString = "SDL.app.metadata.url";
			public const string TypeString = "SDL.app.metadata.type";
		}
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void MainThreadCallback(nint userdata);

	private static partial class Native
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate AppResult AppInit_func(ref nint appstate, int argc, byte** argv);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AppResult AppIterate_func(nint appstate);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate AppResult AppEvent_func(nint appstate, in Event @event);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void AppQuit_func(nint appstate, AppResult result);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_Init(InitFlags flags);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_InitSubSystem(InitFlags flags);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_QuitSubSystem(InitFlags flags);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial InitFlags SDL_WasInit(InitFlags flags);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_Quit();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_IsMainThread();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_RunOnMainThread(MainThreadCallback callback, nint userdata, [MarshalAs(UnmanagedType.I1)] bool waitComplete);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetAppMetadata(string appname, string appversion, string appidentifier);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetAppMetadataProperty(string name, string value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_GetAppMetadataProperty(string name);
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool Init(InitFlags flags) => Native.SDL_Init(flags);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool InitSubSystem(InitFlags flags) => Native.SDL_InitSubSystem(flags);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void QuitSubSystem(InitFlags flags) => Native.SDL_QuitSubSystem(flags);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static InitFlags WasInit(InitFlags flags) => Native.SDL_WasInit(flags);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void Quit() => Native.SDL_Quit();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsMainThread() => Native.SDL_IsMainThread();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool RunOnMainThread(MainThreadCallback callback, nint userdata, [MarshalAs(UnmanagedType.I1)] bool waitComplete) => Native.SDL_RunOnMainThread(callback, userdata, waitComplete);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetAppMetadata(string appName, string appVersion, string appIdentifier) => Native.SDL_SetAppMetadata(appName, appVersion, appIdentifier);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetAppMetadataProperty(string name, string value) => Native.SDL_SetAppMetadataProperty(name, value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetAppMetadataProperty(string name) => Native.SDL_GetAppMetadataProperty(name);
}
