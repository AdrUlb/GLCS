using static GLCS.GL;

namespace GLCS.Abstractions;

public sealed class GLShader(GL gl, ShaderType type) : IDisposable
{
    public readonly uint Handle = gl.CreateShader(type);

    public void Compile(string source)
    {
		gl.ShaderSource(Handle, source);
		gl.CompileShader(Handle);
		if (gl.GetShader(Handle, ShaderParameterName.GL_COMPILE_STATUS) == GL_FALSE)
			throw new($"Shader compilation failed: {gl.GetShaderInfoLog(Handle)}.");
	}

    public void Dispose() => gl.DeleteShader(Handle);
	public static implicit operator uint(GLShader buffer) => buffer.Handle;
}
