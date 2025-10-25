using static GLCS.GL;

namespace GLCS.Abstractions;

public enum GLShaderType : uint
{
	Compute = GL_COMPUTE_SHADER,
	Vertex = GL_VERTEX_SHADER,
	TessControl = GL_TESS_CONTROL_SHADER,
	TessEvaluation = GL_TESS_EVALUATION_SHADER,
	Geometry = GL_GEOMETRY_SHADER,
	Fragment = GL_FRAGMENT_SHADER
}
