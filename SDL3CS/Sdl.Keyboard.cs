using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class Sdl
{
	public enum Capitalization
	{
		None,
		Sentences,
		Words,
		Letters
	}

	public enum SDL_TextInputType
	{
		Text,
		TextName,
		TextEmail,
		TextUsername,
		TextPasswordHidden,
		TextPasswordVisible,
		Number,
		NumberPasswordHidden,
		NumberPasswordVisible
	}

	public readonly struct KeyboardID
	{
		private readonly uint value_;
		public static implicit operator uint(KeyboardID obj) => obj.value_;
	}

	public static partial class Prop
	{
		public static partial class TextInput
		{
			public const string TypeNumber = "SDL.textinput.type";
			public const string CapitalizationNumber = "SDL.textinput.capitalization";
			public const string AutocorrectBoolean = "SDL.textinput.autocorrect";
			public const string MultilineBoolean = "SDL.textinput.multiline";
			public const string AndroidInputtypeNumber = "SDL.textinput.android.inputtype";
		}
	}

	private static partial class Native
	{

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_HasKeyboard();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static unsafe partial KeyboardID* SDL_GetKeyboards(out int count);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_GetKeyboardNameForID(KeyboardID instanceId);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Window> SDL_GetKeyboardFocus();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static unsafe partial byte* SDL_GetKeyboardState(out int numkeys);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_ResetKeyboard();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Keymod SDL_GetModState();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_SetModState(Keymod modstate);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Keycode SDL_GetKeyFromScancode(Scancode scancode, Keymod modstate, [MarshalAs(UnmanagedType.I1)] bool keyEvent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Scancode SDL_GetScancodeFromKey(Keycode key, out Keymod modstate);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetScancodeName(Scancode scancode, string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_GetScancodeName(Scancode scancode);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Scancode SDL_GetScancodeFromName(string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_GetKeyName(Keycode key);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Keycode SDL_GetKeyFromName(string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_StartTextInput(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_StartTextInputWithProperties(in Window window, PropertiesID props);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_TextInputActive(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_StopTextInput(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_ClearComposition(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetTextInputArea(in Window window, in Rect rect, int cursor);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetTextInputArea(in Window window, out Rect rect, out int cursor);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_HasScreenKeyboardSupport();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_ScreenKeyboardShown(in Window window);
	}

	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool HasKeyboard() => Native.SDL_HasKeyboard();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static OwnedArrayPtr<KeyboardID> GetKeyboards() => new(Native.SDL_GetKeyboards(out var count), count);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? GetKeyboardNameForID(KeyboardID instanceId) => Marshal.PtrToStringUTF8(Native.SDL_GetKeyboardNameForID(instanceId));
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> GetKeyboardFocus() => Native.SDL_GetKeyboardFocus();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static ArrayPtr<bool> GetKeyboardState() => new((bool*)Native.SDL_GetKeyboardState(out var numkeys), numkeys);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void ResetKeyboard() => Native.SDL_ResetKeyboard();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Keymod GetModState() => Native.SDL_GetModState();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetModState(Keymod modstate) => Native.SDL_SetModState(modstate);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Keycode GetKeyFromScancode(Scancode scancode, Keymod modstate, bool keyEvent) => Native.SDL_GetKeyFromScancode(scancode, modstate, keyEvent);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Scancode GetScancodeFromKey(Keycode key, out Keymod modstate) => Native.SDL_GetScancodeFromKey(key, out modstate);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetScancodeName(Scancode scancode, string name) => Native.SDL_SetScancodeName(scancode, name);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? GetScancodeName(Scancode scancode) => Marshal.PtrToStringUTF8(Native.SDL_GetScancodeName(scancode));
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Scancode GetScancodeFromName(string name) => Native.SDL_GetScancodeFromName(name);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? GetKeyName(Keycode key) => Marshal.PtrToStringUTF8(Native.SDL_GetKeyName(key));
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Keycode GetKeyFromName(string name) => Native.SDL_GetKeyFromName(name);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool StartTextInput(in Window window) => Native.SDL_StartTextInput(window);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool StartTextInput(Ptr<Window> window) => Native.SDL_StartTextInput(window.Value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool StartTextInputWithProperties(in Window window, PropertiesID props) => Native.SDL_StartTextInputWithProperties(window, props);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool StartTextInputWithProperties(Ptr<Window> window, PropertiesID props) => Native.SDL_StartTextInputWithProperties(window.Value, props);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TextInputActive(in Window window) => Native.SDL_TextInputActive(window);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool TextInputActive(Ptr<Window> window) => Native.SDL_TextInputActive(window.Value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool StopTextInput(in Window window) => Native.SDL_StopTextInput(window);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool StopTextInput(Ptr<Window> window) => Native.SDL_StopTextInput(window.Value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ClearComposition(in Window window) => Native.SDL_ClearComposition(window);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ClearComposition(Ptr<Window> window) => Native.SDL_ClearComposition(window.Value);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetTextInputArea(in Window window, in Rect rect, int cursor) => Native.SDL_SetTextInputArea(window, rect, cursor);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetTextInputArea(Ptr<Window> window, in Rect rect, int cursor) => Native.SDL_SetTextInputArea(window.Value, rect, cursor);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetTextInputArea(in Window window, out Rect rect, out int cursor) => Native.SDL_GetTextInputArea(window, out rect, out cursor);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetTextInputArea(Ptr<Window> window, out Rect rect, out int cursor) => Native.SDL_GetTextInputArea(window.Value, out rect, out cursor);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool HasScreenKeyboardSupport() => Native.SDL_HasScreenKeyboardSupport();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ScreenKeyboardShown(in Window window) => Native.SDL_ScreenKeyboardShown(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ScreenKeyboardShown(Ptr<Window> window) => Native.SDL_ScreenKeyboardShown(window.Value);
}
