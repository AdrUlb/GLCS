using System.Runtime.InteropServices;
using System.Text;

namespace GLCS;

public unsafe partial class GL(GL.GetProcAddress getProcAddress)
{
	public delegate nint GetProcAddress(string proc);

	public void GenBuffers(ReadOnlySpan<uint> buffers)
	{
		fixed (uint* buffersPtr = buffers)
			_glGenBuffers(buffers.Length, buffersPtr);
	}

	public uint GenBuffer()
	{
		var buffer = 0u;
		_glGenBuffers(1, &buffer);
		return buffer;
	}

	public void DeleteBuffer(uint buffer)
	{
		_glDeleteBuffers(1, &buffer);
	}

	public void BufferData<T>(uint target, ReadOnlySpan<T> data, uint usage) where T : unmanaged
	{
		fixed (T* dataPtr = data)
			_glBufferData(target, data.Length * sizeof(T), dataPtr, usage);
	}

	public void GenVertexArrays(ReadOnlySpan<uint> arrays)
	{
		fixed (uint* arraysPtr = arrays)
			GenVertexArrays(arrays.Length, arraysPtr);
	}

	public uint GenVertexArray()
	{
		var vao = 0u;
		_glGenVertexArrays(1, &vao);
		return vao;
	}

	public void DeleteVertexArray(uint vao)
	{
		_glDeleteVertexArrays(1, &vao);
	}

	public void GetShaderiv(uint shader, uint pname, Span<int> @params)
	{
		fixed (int* paramsPtr = @params)
			_glGetShaderiv(shader, pname, paramsPtr);
	}

	public int GetShader(uint shader, uint pname)
	{
		var ret = 0;
		_glGetShaderiv(shader, pname, &ret);
		return ret;
	}

	public void ShaderSource(uint shader, string source)
	{
		var length = source.Length;
		var nativeSource = (byte*)Marshal.StringToCoTaskMemUTF8(source);
		_glShaderSource(shader, 1, &nativeSource, &length);
		Marshal.FreeCoTaskMem((nint)nativeSource);
	}

	public string GetShaderInfoLog(uint shader)
	{
		var infoLogLength = GetShader(shader, GL_INFO_LOG_LENGTH);

		var infoLog = stackalloc byte[infoLogLength];
		_glGetShaderInfoLog(shader, infoLogLength, null, infoLog);

		return Encoding.UTF8.GetString(infoLog, infoLogLength);
	}

	public void GetProgramiv(uint program, uint pname, Span<int> @params)
	{
		fixed (int* paramsPtr = @params)
			_glGetProgramiv(program, pname, paramsPtr);
	}

	public int GetProgram(uint program, uint pname)
	{
		var ret = 0;
		_glGetProgramiv(program, pname, &ret);
		return ret;
	}

	public string GetProgramInfoLog(uint program)
	{
		var infoLogLength = GetProgram(program, GL_INFO_LOG_LENGTH);

		var infoLog = stackalloc byte[infoLogLength];
		_glGetProgramInfoLog(program, infoLogLength, null, infoLog);

		return Encoding.UTF8.GetString(infoLog, infoLogLength);
	}

	public void VertexAttribPointer(uint @index, int @size, uint @type, bool @normalized, int @stride, nint offset)
	{
		_glVertexAttribPointer(index, size, type, normalized, stride, (void*)offset);
	}
}
