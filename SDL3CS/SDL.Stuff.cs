using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class Sdl
{
	public const string LibraryName = "SDL3";

	private sealed class SdlFreeHandle : SafeHandle
	{
		public SdlFreeHandle(nint handle) : base(0, true)
		{
			SetHandle(handle);
		}

		public override bool IsInvalid => handle == 0;

		protected override bool ReleaseHandle()
		{
			if (IsInvalid)
				return false;

			Native.SDL_free(handle);
			Console.WriteLine("freed!");
			SetHandle(0);
			return true;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public unsafe readonly struct Ptr<T> where T : unmanaged
	{
		private readonly T* ptr_;
		public readonly bool IsNull
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => ptr_ == null;
		}

		public readonly ref T Value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (IsNull)
					throw new NullReferenceException();
				return ref *ptr_;
			}
		}
	}

	public unsafe readonly struct OwnedArray<T>(T* ptr, int count) : IDisposable where T : unmanaged
	{
		private readonly SdlFreeHandle handle_ = new((nint)ptr);

		public readonly bool IsNull
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => ptr == null;
		}

		public readonly Span<T> Value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => IsNull ? throw new NullReferenceException() : new(ptr, count);
		}

		public void Dispose() => handle_.Dispose();
	}

	public const float FltEpsilon = float.Epsilon;

	public readonly struct Surface { }

	public readonly struct SensorID
	{
		private readonly uint value_;
		public static implicit operator uint(SensorID obj) => obj.value_;
	}

	public readonly struct PenID
	{
		private readonly uint value_;
		public static implicit operator uint(PenID obj) => obj.value_;
	}

	public enum TouchID : ulong { }
	public enum FingerID : ulong { }
	public enum PenInputFlags : uint { }
	public enum KeyboardID : uint { }
	public enum Keycode : uint { }
	public enum MouseID : uint { }
	public enum MouseButtonFlags : uint { }
	public enum JoystickID : uint { }
	public enum AudioDeviceID : uint { }
	public enum CameraID : uint { }
	public enum Keymod : ushort { }
	public enum PenAxis { }
	public enum Scancode { }
	public enum MouseWheelDirection { }
	public enum PowerState { }

	public static uint DefineFourCc(char a, char b, char c, char d) => ((uint)(byte)a << 0) | ((uint)(byte)b << 8) | ((uint)(byte)c << 16) | ((uint)(byte)d << 24);

	private static partial class Native
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_free(nint mem);
	}
}
