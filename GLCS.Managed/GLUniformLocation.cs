using System.Numerics;

namespace GLCS.Managed;

public sealed class GLUniformLocation(GLProgram program, int location)
{
	public unsafe void Matrix4(ref Matrix4x4 value)
	{
		ManagedGL.Current.Unmanaged.UseProgram(program.Handle);
		fixed (Matrix4x4* valuePtr = &value)
			ManagedGL.Current.Unmanaged.UniformMatrix4fv(location, 1, false, (float*)valuePtr);
		ManagedGL.Current.Unmanaged.UseProgram(0);
	}
	
	public unsafe void Vec4(params ReadOnlySpan<float> value)
	{
		ManagedGL.Current.Unmanaged.UseProgram(program.Handle);
		fixed (float* valuePtr = value)
			ManagedGL.Current.Unmanaged.Uniform4fv(location, 1, valuePtr);
		ManagedGL.Current.Unmanaged.UseProgram(0);
	}
	
	public void Int(int value)
	{
		ManagedGL.Current.Unmanaged.UseProgram(program.Handle);
		ManagedGL.Current.Unmanaged.Uniform1i(location, value);
		ManagedGL.Current.Unmanaged.UseProgram(0);
	}
	
	public void UInt(uint value)
	{
		ManagedGL.Current.Unmanaged.UseProgram(program.Handle);
		ManagedGL.Current.Unmanaged.Uniform1ui(location, value);
		ManagedGL.Current.Unmanaged.UseProgram(0);
	}
}
