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

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Enable(EnableCap cap) => Unmanaged.Enable(cap);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Disable(EnableCap cap) => Unmanaged.Disable(cap);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void GetInteger(GetPName pname, Span<int> data)
	{
		fixed (int* dataPtr = data)
			Unmanaged.GetIntegerv(pname, dataPtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int GetInteger(GetPName pname)
	{
		int data;
		Unmanaged.GetIntegerv(pname, &data);
		return data;
	}
}
