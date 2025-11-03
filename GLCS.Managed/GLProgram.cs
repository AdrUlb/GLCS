using System.Runtime.CompilerServices;
using System.Text;

namespace GLCS.Managed;

public unsafe sealed class GLProgram() : IDisposable
{
	public readonly uint Handle = ManagedGL.Current.Unmanaged.CreateProgram();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Get(ProgramPropertyARB pname)
	{
		var ret = 0;
		ManagedGL.Current.Unmanaged.GetProgramiv(Handle, pname, &ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Get(ProgramPropertyARB pname, Span<int> @params)
	{
		fixed (int* paramsPtr = @params)
			ManagedGL.Current.Unmanaged.GetProgramiv(Handle, pname, paramsPtr);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public string GetInfoLog()
	{
		var infoLogLength = Get(ProgramPropertyARB.InfoLogLength);

		var infoLog = stackalloc byte[infoLogLength];
		ManagedGL.Current.Unmanaged.GetProgramInfoLog(Handle, infoLogLength, null, infoLog);

		return Encoding.UTF8.GetString(infoLog, infoLogLength);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Link(params ReadOnlySpan<GLShader> shaders)
	{
		foreach (var shader in shaders)
			ManagedGL.Current.Unmanaged.AttachShader(Handle, shader.Handle);

		ManagedGL.Current.Unmanaged.LinkProgram(Handle);

		foreach (var shader in shaders)
			ManagedGL.Current.Unmanaged.DetachShader(Handle, shader.Handle);
	}

	public void Dispose() => ManagedGL.Current.Unmanaged.DeleteProgram(Handle);

}
