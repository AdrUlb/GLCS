namespace SDL3CS;

// TODO
public static partial class Sdl
{

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

	public readonly struct JoystickID
	{
		private readonly uint value_;
		public static implicit operator uint(JoystickID obj) => obj.value_;
	}

	public readonly struct AudioDeviceID
	{
		private readonly uint value_;
		public static implicit operator uint(AudioDeviceID obj) => obj.value_;
	}

	public readonly struct CameraID
	{
		private readonly uint value_;
		public static implicit operator uint(CameraID obj) => obj.value_;
	}

	public readonly struct TouchID
	{
		private readonly ulong value_;
		public static implicit operator ulong(TouchID obj) => obj.value_;
	}

	public readonly struct FingerID
	{
		private readonly ulong value_;
		public static implicit operator ulong(FingerID obj) => obj.value_;
	}

	public enum PenInputFlags : uint { }
	public enum PenAxis { }
	public enum PowerState { }
}
