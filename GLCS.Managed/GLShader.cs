using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace GLCS.Managed;

public unsafe sealed class GLShader(ShaderType type) : IDisposable
{
	public readonly uint Handle = ManagedGL.Current.Unmanaged.CreateShader(type);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Get(ShaderParameterName pname, Span<int> @params)
	{
		fixed (int* paramsPtr = @params)
			ManagedGL.Current.Unmanaged.GetShaderiv(Handle, pname, paramsPtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Get(ShaderParameterName pname)
	{
		var ret = 0;
		ManagedGL.Current.Unmanaged.GetShaderiv(Handle, pname, &ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public string GetInfoLog()
	{
		var infoLogLength = Get(ShaderParameterName.InfoLogLength);

		var infoLog = stackalloc byte[infoLogLength];
		ManagedGL.Current.Unmanaged.GetShaderInfoLog(Handle, infoLogLength, null, infoLog);

		return Encoding.UTF8.GetString(infoLog, infoLogLength);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Compile(string source)
	{
		var length = source.Length;
		var nativeSource = (byte*)Marshal.StringToCoTaskMemUTF8(source);
		ManagedGL.Current.Unmanaged.ShaderSource(Handle, 1, &nativeSource, &length);
		Marshal.FreeCoTaskMem((nint)nativeSource);
		ManagedGL.Current.Unmanaged.CompileShader(Handle);
	}

	public void Dispose() => ManagedGL.Current.Unmanaged.DeleteShader(Handle);
}
