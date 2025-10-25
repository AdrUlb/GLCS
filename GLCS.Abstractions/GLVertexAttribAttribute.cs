namespace GLCS.Abstractions;

[AttributeUsage(AttributeTargets.Field)]
public sealed class GLVertexAttribAttribute(uint index, uint type, bool normalized) : Attribute
{
	public readonly uint Index = index;
	public readonly uint Type = type;
	public readonly bool Normalized = normalized;
}
