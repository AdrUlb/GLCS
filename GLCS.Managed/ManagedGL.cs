using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GLCS.Managed;

public unsafe partial class ManagedGL(GL.GetProcAddress getProcAddress)
{
	public GL Unmanaged { get; } = new(getProcAddress);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Viewport(int x, int y, int width, int height)
	{
		Unmanaged.Viewport(x, y, width, height);
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public string? GetString(StringName name) => Marshal.PtrToStringUTF8((nint)Unmanaged.GetString(name));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Clear(Color color, ClearBufferMask mask)
	{
		Unmanaged.ClearColor(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
		Unmanaged.Clear(mask);
	}
}
