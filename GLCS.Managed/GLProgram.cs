using System.Runtime.CompilerServices;
using System.Text;

namespace GLCS.Managed;

public unsafe sealed class GLProgram(ManagedGL gl) : IDisposable
{
	public readonly uint Handle = gl.Unmanaged.CreateProgram();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Get(ProgramPropertyARB pname)
	{
		var ret = 0;
		gl.Unmanaged.GetProgramiv(Handle, pname, &ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Get(ProgramPropertyARB pname, Span<int> @params)
	{
		fixed (int* paramsPtr = @params)
			gl.Unmanaged.GetProgramiv(Handle, pname, paramsPtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public string GetInfoLog()
	{
		var infoLogLength = Get(ProgramPropertyARB.InfoLogLength);

		var infoLog = stackalloc byte[infoLogLength];
		gl.Unmanaged.GetProgramInfoLog(Handle, infoLogLength, null, infoLog);

		return Encoding.UTF8.GetString(infoLog, infoLogLength);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void AttachShader(GLShader shader) => gl.Unmanaged.AttachShader(Handle, shader.Handle);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void DetachShader(GLShader shader) => gl.Unmanaged.DetachShader(Handle, shader.Handle);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Link() => gl.Unmanaged.LinkProgram(Handle);

	public void Dispose() => gl.Unmanaged.DeleteProgram(Handle);

}
