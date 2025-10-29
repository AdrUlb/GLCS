using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GLCS.Managed;

[AttributeUsage(AttributeTargets.Field)]
public sealed class GLVertexAttribAttribute(uint index, int size, VertexAttribPointerType type, bool normalized) : Attribute
{
	public readonly uint Index = index;
	public readonly int Size = size;
	public readonly VertexAttribPointerType Type = type;
	public readonly bool Normalized = normalized;
}

public readonly struct GLVertexAttribsInfo<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] T> where T : struct
{
	private readonly struct AttribInfo(uint index, int size, VertexAttribPointerType type, bool normalized, nint offset)
	{
		public readonly uint Index = index;
		public readonly int Size = size;
		public readonly VertexAttribPointerType Type = type;
		public readonly bool Normalized = normalized;
		public readonly nint Offset = offset;
	}

	private readonly List<AttribInfo> attribs_ = [];

	public readonly int Stride;

	public GLVertexAttribsInfo()
	{
		Stride = Marshal.SizeOf<T>();

		var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
		foreach (var field in fields)
		{
			var a = field.GetCustomAttributes(typeof(GLVertexAttribAttribute), false);
			if (a.Length == 0)
				continue;

			if (a[0] is not GLVertexAttribAttribute attrib)
				continue;

			var offset = Marshal.OffsetOf<T>(field.Name);
			attribs_.Add(new AttribInfo(attrib.Index, attrib.Size, attrib.Type, attrib.Normalized, offset));
		}
	}

	public unsafe void VertexAttribPointers(ManagedGL gl)
	{
		foreach (var attrib in attribs_)
		{
			gl.Unmanaged.VertexAttribPointer(attrib.Index, attrib.Size, attrib.Type, attrib.Normalized, Stride, (void*)attrib.Offset);
			gl.Unmanaged.EnableVertexAttribArray(attrib.Index);
		}
	}
}
