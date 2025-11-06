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

[AttributeUsage(AttributeTargets.Field)]
public sealed class GLVertexAttribDivisorAttribute(uint divisor) : Attribute
{
	public readonly uint Divisor = divisor;
}

public readonly struct GLVertexAttribsInfo<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] T> where T : struct
{
	private readonly struct AttribInfo(uint index, int size, VertexAttribPointerType type, bool normalized, nint offset, uint divisor)
	{
		public readonly uint Index = index;
		public readonly int Size = size;
		public readonly VertexAttribPointerType Type = type;
		public readonly bool Normalized = normalized;
		public readonly nint Offset = offset;
		public readonly uint Divisor = divisor;
	}

	private readonly List<AttribInfo> attribs_ = [];

	public readonly int Stride;

	public GLVertexAttribsInfo()
	{
		Stride = Marshal.SizeOf<T>();

		var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
		foreach (var field in fields)
		{
			if (field.GetCustomAttributes(typeof(GLVertexAttribAttribute), false).FirstOrDefault() is not GLVertexAttribAttribute attrib)
				continue;

			var divisorAttr = field.GetCustomAttributes(typeof(GLVertexAttribDivisorAttribute), false).FirstOrDefault() as GLVertexAttribDivisorAttribute;

			var offset = Marshal.OffsetOf<T>(field.Name);

			// Handle attributes that span multiple attribute slots
			var count = attrib.Size / 4;
			if (attrib.Size % 4 != 0)
				count++;

			var size = Math.Min(attrib.Size, 4);
			var index = attrib.Index;

			var typeSize = attrib.Type switch
			{
				VertexAttribPointerType.Byte => sizeof(sbyte),
				VertexAttribPointerType.UnsignedByte => sizeof(byte),
				VertexAttribPointerType.Short => sizeof(short),
				VertexAttribPointerType.UnsignedShort => sizeof(ushort),
				VertexAttribPointerType.Int => sizeof(int),
				VertexAttribPointerType.UnsignedInt => sizeof(uint),
				VertexAttribPointerType.Float => sizeof(float),
				VertexAttribPointerType.Double => sizeof(double),
				VertexAttribPointerType.HalfFloat => throw new NotImplementedException(),
				VertexAttribPointerType.Fixed => throw new NotImplementedException(),
				VertexAttribPointerType.Int64Arb => throw new NotImplementedException(),
				VertexAttribPointerType.UnsignedInt64Arb => throw new NotImplementedException(),
				VertexAttribPointerType.UnsignedInt2101010Rev => throw new NotImplementedException(),
				VertexAttribPointerType.UnsignedInt10f11f11fRev => throw new NotImplementedException(),
				VertexAttribPointerType.Int2101010Rev => throw new NotImplementedException(),
				_ => throw new NotImplementedException(),
			};

			var divisor = divisorAttr?.Divisor ?? 0;

			for (var i = 0; i < count; i++)
			{
				attribs_.Add(new AttribInfo(index, size, attrib.Type, attrib.Normalized, offset, divisor));
				index++;
				offset += typeSize * 4;
			}
		}
	}

	public unsafe void VertexAttribPointers(ManagedGL gl)
	{
		foreach (var attrib in attribs_)
		{
			gl.Unmanaged.EnableVertexAttribArray(attrib.Index);
			gl.Unmanaged.VertexAttribPointer(attrib.Index, attrib.Size, attrib.Type, attrib.Normalized, Stride, (void*)attrib.Offset);
			gl.Unmanaged.VertexAttribDivisor(attrib.Index, attrib.Divisor);
		}
	}
}
