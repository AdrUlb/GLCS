using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class SDL
{
	public const string LibraryName = "SDL3";

	public enum InitFlags : uint
	{
		Audio = 0x00000010,
		Video = 0x00000020,
		Joystick = 0x00000200,
		Haptic = 0x00001000,
		Gamepad = 0x00002000,
		Events = 0x00004000,
		Sensor = 0x00008000,
		Camera = 0x00010000,
	}

	public readonly struct WindowPtr
	{
		private readonly nint value_;

		public static implicit operator nint(WindowPtr obj) => obj.value_;
	}

	public static uint DefineFourCc(char a, char b, char c, char d) => ((uint)(byte)a << 0) | ((uint)(byte)b << 8) | ((uint)(byte)c << 16) | ((uint)(byte)d << 24);

	private static partial class Native
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, EntryPoint = "SDL_Init", StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_Init(InitFlags flags);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, EntryPoint = "SDL_Quit", StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_Quit();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, EntryPoint = "SDL_CreateWindowWithProperties", StringMarshalling = StringMarshalling.Utf8)]
		public static partial WindowPtr SDL_CreateWindowWithProperties(PropertiesID props);
	}

	public static bool Init(InitFlags flags) => Native.SDL_Init(flags);
	public static void Quit() => Native.SDL_Quit();
	public static WindowPtr CreateWindowWithProperties(PropertiesID props) => Native.SDL_CreateWindowWithProperties(props);
}
