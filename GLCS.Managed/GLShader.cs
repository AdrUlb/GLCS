using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace GLCS.Managed;

public unsafe sealed class GLShader(ManagedGL gl, ShaderType type) : IDisposable
{
	internal readonly uint Handle = gl.Unmanaged.CreateShader(type);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Get(ShaderParameterName pname, Span<int> @params)
	{
		fixed (int* paramsPtr = @params)
			gl.Unmanaged.GetShaderiv(Handle, pname, paramsPtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Get(ShaderParameterName pname)
	{
		var ret = 0;
		gl.Unmanaged.GetShaderiv(Handle, pname, &ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Source(string source)
	{
		var length = source.Length;
		var nativeSource = (byte*)Marshal.StringToCoTaskMemUTF8(source);
		gl.Unmanaged.ShaderSource(Handle, 1, &nativeSource, &length);
		Marshal.FreeCoTaskMem((nint)nativeSource);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public string GetInfoLog()
	{
		var infoLogLength = Get(ShaderParameterName.InfoLogLength);

		var infoLog = stackalloc byte[infoLogLength];
		gl.Unmanaged.GetShaderInfoLog(Handle, infoLogLength, null, infoLog);

		return Encoding.UTF8.GetString(infoLog, infoLogLength);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Compile() => gl.Unmanaged.CompileShader(Handle);

	public void Dispose() => gl.Unmanaged.DeleteShader(Handle);
}
