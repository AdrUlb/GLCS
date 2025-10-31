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

	public unsafe readonly struct ArrayPtr<T>(T* ptr, int count) where T : unmanaged
	{
		public readonly bool IsNull
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => ptr == null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan() => IsNull ? throw new NullReferenceException() : new(ptr, count);
	}

	public unsafe readonly struct OwnedArrayPtr<T>(T* ptr, int count) : IDisposable where T : unmanaged
	{
		private readonly ArrayPtr<T> array_ = new(ptr, count);
		private readonly SdlFreeHandle handle_ = new((nint)ptr);
		public readonly int Count { get; } = count;

		public readonly bool IsNull
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => array_.IsNull;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T* AsPointer() => ptr;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan() => array_.AsSpan();

		public readonly void Dispose() => handle_.Dispose();
	}

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
	public enum MouseID : uint { }
	public enum MouseButtonFlags : uint { }
	public enum JoystickID : uint { }
	public enum AudioDeviceID : uint { }
	public enum CameraID : uint { }
	public enum PenAxis { }
	public enum MouseWheelDirection { }
	public enum PowerState { }
}
