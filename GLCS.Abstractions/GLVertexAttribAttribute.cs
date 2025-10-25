namespace GLCS.Abstractions;

[AttributeUsage(AttributeTargets.Field)]
public sealed class GLVertexAttribAttribute(uint index, int size, uint type, bool normalized) : Attribute
{
	public readonly uint Index = index;
	public readonly int Size = size;
	public readonly uint Type = type;
	public readonly bool Normalized = normalized;
}
