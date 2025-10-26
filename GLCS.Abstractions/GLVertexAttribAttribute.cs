namespace GLCS.Abstractions;

[AttributeUsage(AttributeTargets.Field)]
public sealed class GLVertexAttribAttribute(uint index, int size, VertexAttribPointerType type, bool normalized) : Attribute
{
	public readonly uint Index = index;
	public readonly int Size = size;
	public readonly VertexAttribPointerType Type = type;
	public readonly bool Normalized = normalized;
}
