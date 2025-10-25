using GLCS;
using System.Runtime.InteropServices;
using static GLCS.GL;

namespace GLCS.Abstractions;

public readonly struct GLVertexAttribsInfo<T> where T : struct
{
	private readonly struct AttribInfo(uint index, int size, uint type, bool normalized, nint offset)
	{
		public readonly uint Index = index;
		public readonly int Size = size;
		public readonly uint Type = type;
		public readonly bool Normalized = normalized;
		public readonly nint Offset = offset;
	}

	private readonly List<AttribInfo> attribs_ = [];

	public readonly int Stride;

	public GLVertexAttribsInfo()
	{
		Stride = Marshal.SizeOf<T>();

		var fields = typeof(T).GetFields();
		foreach (var field in fields)
		{
			var a = field.GetCustomAttributes(typeof(GLVertexAttribAttribute), false);
			if (a.Length == 0)
				continue;

			if (a[0] is not GLVertexAttribAttribute attrib)
				continue;

			var size = Marshal.SizeOf(field.FieldType);
			var offset = Marshal.OffsetOf<T>(field.Name);
			attribs_.Add(new AttribInfo(attrib.Index, size, attrib.Type, attrib.Normalized, offset));
		}
	}

	public void VertexAttribPointers(GL gl)
	{
		foreach (var attrib in attribs_)
		{
			var typeSize = attrib.Type switch
			{
				GL_BYTE or GL_UNSIGNED_BYTE => sizeof(byte),
				GL_SHORT or GL_UNSIGNED_SHORT => sizeof(short),
				GL_INT or GL_UNSIGNED_INT or GL_FLOAT => sizeof(int),
				GL_DOUBLE => sizeof(double),
				_ => throw new("FIXME"),
			};

			gl.VertexAttribPointer(attrib.Index, attrib.Size / typeSize, attrib.Type, attrib.Normalized, Stride, attrib.Offset);
			gl.EnableVertexAttribArray(attrib.Index);
		}
	}
}
