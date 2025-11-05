using System.Numerics;

namespace GLCS.Managed;

public sealed class GLUniformLocation(GLProgram program, int location)
{
	public unsafe void Matrix4(bool transpose, ref Matrix4x4 value)
	{
		ManagedGL.Current.Unmanaged.UseProgram(program.Handle);
		fixed (Matrix4x4* valuePtr = &value)
			ManagedGL.Current.Unmanaged.UniformMatrix4fv(location, 1, transpose, (float*)valuePtr);
		ManagedGL.Current.Unmanaged.UseProgram(0);
	}
}
