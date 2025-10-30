using System.Text;
using System.Xml;

namespace GLCS.Generator;

internal sealed class GLEnum(string name, string type, bool isBitmask)
{
	public readonly string Name = name;
	public readonly string Type = type;
	public readonly bool IsBitmask = isBitmask;
	public readonly List<string> Members = [];
}

internal class Program
{
	private static void Main(string[] args)
	{
		var xmlString = File.ReadAllText(args[0]);
		GenerateBindings(xmlString);
	}

	private static void GenerateBindings(string xmlString)
	{
		var constantsBuilder = new StringBuilder();
		GenerateUsingsAndNamespace(constantsBuilder);
		constantsBuilder
			.AppendLine("public unsafe partial class GL")
			.AppendLine("{");

		var enumsBuilder = new StringBuilder();
		GenerateUsingsAndNamespace(enumsBuilder);

		var procsBuilder = new StringBuilder();
		GenerateUsingsAndNamespace(procsBuilder);
		procsBuilder
			.AppendLine("public unsafe partial class GL")
			.AppendLine("{");

		var functionsBuilder = new StringBuilder();
		GenerateUsingsAndNamespace(functionsBuilder);
		functionsBuilder
			.AppendLine("public unsafe partial class GL")
			.AppendLine("{");

		var doc = new XmlDocument()
		{
			PreserveWhitespace = true,
		};
		doc.LoadXml(xmlString);

		var rootElement = doc.DocumentElement ?? throw new("FIXME");

		var enumsElements = rootElement.GetElementsByTagName("enums");
		var commandsElements = rootElement.GetElementsByTagName("commands");

		var groups = new Dictionary<string, GLEnum>();

		foreach (XmlNode enumsElement in enumsElements)
			GenerateEnums(enumsElement, groups, constantsBuilder);

		foreach (XmlNode commandsElement in commandsElements)
			GenerateCommands(commandsElement, procsBuilder, functionsBuilder);

		foreach (var (groupName, group) in groups)
		{
			if (group.IsBitmask)
				enumsBuilder.AppendLine("[System.Flags]");
			var groupType = groupName == "SpecialNumbers" ? "GLuint64" : group.Type;
			enumsBuilder.AppendLine($"public enum {groupName} : {groupType}");
			enumsBuilder.AppendLine("{");
			foreach (var member in group.Members)
				enumsBuilder.Append('\t').Append(EnumMemberName(member)).Append(" = GLCS.GL.").Append(ConstantName(member)).AppendLine(",");
			enumsBuilder.AppendLine("}");
		}

		constantsBuilder.AppendLine("}");
		procsBuilder.AppendLine("}");
		functionsBuilder.AppendLine("}");

		File.WriteAllText("GL.Constants.g.cs", constantsBuilder.ToString());
		File.WriteAllText("GL.Enums.g.cs", enumsBuilder.ToString());
		File.WriteAllText("GL.Procs.g.cs", procsBuilder.ToString());
		File.WriteAllText("GL.Functions.g.cs", functionsBuilder.ToString());
	}

	private static string ConstantName(string value)
	{
		if (!value.StartsWith("GL_"))
			return value;

		value = value[3..];
		if (char.IsAsciiDigit(value[0]))
			value = "N" + value;
		return value;
	}

	private static string EnumMemberName(string member)
	{
		if (!member.StartsWith("GL_"))
			return member;

		member = member[3..];
		var parts = member.Split('_', StringSplitOptions.RemoveEmptyEntries);
		var sb = new StringBuilder();
		foreach (var part in parts)
		{
			var firstChar = part[0];
			if (char.IsAsciiDigit(firstChar))
			{
				sb.Append('N').Append(firstChar);
			}
			else
				sb.Append(char.ToUpper(firstChar));
			if (part.Length > 1)
				sb.Append(part[1..].ToLower());
		}
		return sb.ToString();
	}

	private static void GenerateUsingsAndNamespace(StringBuilder builder)
	{
		builder
			.AppendLine("using GLenum = uint;")
			.AppendLine("using GLboolean = bool;")
			.AppendLine("using GLbitfield = uint;")
			.AppendLine("using GLbyte = sbyte;")
			.AppendLine("using GLubyte = byte;")
			.AppendLine("using GLshort = short;")
			.AppendLine("using GLushort = ushort;")
			.AppendLine("using GLint = int;")
			.AppendLine("using GLuint = uint;")
			.AppendLine("using GLclampx = int;")
			.AppendLine("using GLsizei = int;")
			.AppendLine("using GLfloat = float;")
			.AppendLine("using GLclampf = float;")
			.AppendLine("using GLdouble = double;")
			.AppendLine("using GLclampd = double;")
			.AppendLine("using GLeglClientBufferEXT = nuint;")
			.AppendLine("using GLeglImageOES = nuint;")
			.AppendLine("using GLchar = byte;")
			.AppendLine("using GLcharARB = byte;")
			.AppendLine("using GLhandleARB = nuint; // Size??")
			.AppendLine("using GLhalf = ushort;")
			.AppendLine("using GLhalfARB = ushort;")
			.AppendLine("using GLfixed = int;")
			.AppendLine("using GLintptr = nint;")
			.AppendLine("using GLintptrARB = nint;")
			.AppendLine("using GLsizeiptr = nint;")
			.AppendLine("using GLsizeiptrARB = nint;")
			.AppendLine("using GLint64 = long;")
			.AppendLine("using GLint64EXT = long;")
			.AppendLine("using GLuint64 = ulong;")
			.AppendLine("using GLuint64EXT = ulong;")
			.AppendLine("using GLsync = nuint; // typedef struct __GLsync *GLsync;")
			.AppendLine()
			.AppendLine("using _cl_context = nuint; // struct _cl_context;")
			.AppendLine("using _cl_event = nuint; // struct _cl_event;")
			.AppendLine("using GLDEBUGPROC = nuint;")
			.AppendLine("using GLDEBUGPROCARB = nuint;")
			.AppendLine("using GLDEBUGPROCKHR = nuint;")
			.AppendLine()
			.AppendLine("// Vendor extension types")
			.AppendLine("using GLDEBUGPROCAMD = nuint;")
			.AppendLine("using GLhalfNV = ushort;")
			.AppendLine("using GLvdpauSurfaceNV = nint; // typedef GLintptr GLvdpauSurfaceNV;")
			.AppendLine("using GLVULKANPROCNV = nuint;")
			.AppendLine()
			.AppendLine("namespace GLCS;")
			.AppendLine();
	}

	private static void GenerateEnums(XmlNode enumsElement, Dictionary<string, GLEnum> groups, StringBuilder constantsBuilder)
	{
		var isBitmask = enumsElement.Attributes?["type"]?.Value == "bitmask";

		var enumType = isBitmask ? "GLbitfield" : "GLenum";

		foreach (XmlNode node in enumsElement.ChildNodes)
		{
			if (node.NodeType != XmlNodeType.Element)
				continue;

			if (node.Name != "enum")
				continue;

			var groupList = node.Attributes?["group"]?.Value?.Split(",");
			var enumMemberName = node.Attributes?["name"]?.Value;
			var enumMemberValue = node.Attributes?["value"]?.Value;
			var enumMemberApi = node.Attributes?["api"]?.Value;
			enumType = node.Attributes?["type"]?.Value switch
			{
				"u" => "GLuint",
				"ull" => "GLuint64",
				_ => enumType
			};

			if (enumMemberApi != null || enumType == null || enumMemberName == null || enumMemberValue == null)
				continue;

			enumMemberName = enumMemberName.Trim();

			if (groupList != null)
			{
				foreach (var groupName in groupList)
				{
					if (!groups.TryGetValue(groupName, out var group))
						groups.Add(groupName, group = new(groupName, enumType, isBitmask));

					group.Members.Add(enumMemberName);
				}
			}

			constantsBuilder
				.Append('\t')
				.Append("public const ")
				.Append(enumType)
				.Append(' ')
				.Append(ConstantName(enumMemberName))
				.Append(" = unchecked((")
				.Append(enumType)
				.Append(")(")
				.Append(enumMemberValue).AppendLine("));");
		}
	}

	class GlParam(string text, string? group)
	{
		public string Text = text;
		public string? Group = group;
		public string? Name = null;
		public string? Type = null;
	}

	class GlFunc(string name, string returnType, List<GlParam> param)
	{
		public readonly string Name = name;
		public readonly string ReturnType = returnType;
		public readonly List<GlParam> Params = param;
	}

	private static string GetEnumType(string group, string type)
	{
		if (group != null)
		{
			var index = type.IndexOf('*');
			if (index >= 0)
				group += type[index..];

			return group;
		}

		return type;
	}

	private static void GenerateCommands(XmlNode commandsElement, StringBuilder procsBuilder, StringBuilder functionsBuilder)
	{
		var funcs = new List<GlFunc>();

		foreach (XmlNode node in commandsElement.ChildNodes)
		{
			if (node.NodeType != XmlNodeType.Element)
				continue;

			if (node.Name != "command")
				continue;

			var element = (XmlElement)node;

			foreach (XmlAttribute attr in node.Attributes ?? throw new NotSupportedException())
			{
				switch (attr.Name)
				{
					case "comment":
						break;
					default:
						throw new NotSupportedException($"Unknown command attribute value '{attr.Name}' = '{attr.Value}'");
				}
			}

			var proto = node["proto"]?.InnerText;
			var param = new List<GlParam>();

			foreach (XmlNode n in node.ChildNodes)
			{
				if (n.NodeType != XmlNodeType.Element || n.Name != "param")
					continue;

				var group = n.Attributes?["group"]?.Value;

				param.Add(new(n.InnerText, group));
			}

			if (proto == null)
				continue;

			proto = proto.Replace("const ", "").Replace("const*", "*");

			var splitIndex = proto.IndexOf('*');
			if (splitIndex < 0)
				splitIndex = proto.IndexOf(' ');
			splitIndex++;
			var returnType = proto[..splitIndex].Replace(" *", "*").Trim();
			for (var i = 0; i < param.Count; i++)
			{
				var paramText = param[i].Text
					.Replace("const*", "*")
					.Replace("struct ", "")
					.Replace("const ", "")
					.Replace("* ", "*")
					.Replace(" *", "*")
					;

				var index = paramText.LastIndexOf('*');
				if (index < 0)
					index = paramText.LastIndexOf(' ');
				index++;
				param[i].Type = paramText[..index].Trim();

				var pname = paramText[index..].Trim();
				//if (pname is "params" or "event")
				pname = "@" + pname;
				param[i].Name = pname;
			}

			var name = proto[splitIndex..].Trim();
			if (!name.StartsWith("gl"))
				continue;

			funcs.Add(new GlFunc(name, returnType, param));
		}

		foreach (var func in funcs)
		{
			var delegateName = char.ToLower(func.Name[0]) + func.Name[1..] + "_";
			var type = $"delegate* unmanaged[Stdcall]<{string.Join(", ", [.. func.Params.Select(static o => o.Type), func.ReturnType])}>";
			procsBuilder.Append('\t').Append("private readonly ").Append(type).Append(' ').Append(delegateName).Append(" = (").Append(type).Append(")getProcAddress(\"")
				.Append(func.Name).Append("\");").AppendLine();
		}

		foreach (var func in funcs)
		{
			var delegateName = char.ToLower(func.Name[0]) + func.Name[1..] + "_";
			var memberName = func.Name[2..];
			functionsBuilder
				.Append('\t').AppendLine("[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]")
				.Append('\t').Append("public ").Append(func.ReturnType).Append(' ').Append(memberName).Append('(').Append(string.Join(", ", func.Params.Select(p => $"{GetEnumType(p.Group!, p.Type!)} {p.Name}")))
				.Append(") => ").Append(delegateName).Append('(').Append(string.Join(", ", func.Params.Select(p => $"({p.Type}){p.Name}"))).AppendLine(");");
		}
	}
}
