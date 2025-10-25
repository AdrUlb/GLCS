using System.Text;
using System.Xml;

namespace GLCS.Generator;

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

		foreach (XmlNode enumsElement in enumsElements)
			GenerateEnums(enumsElement, constantsBuilder);

		foreach (XmlNode commandsElement in commandsElements)
			GenerateCommands(commandsElement, functionsBuilder);

		constantsBuilder.AppendLine("}");
		functionsBuilder.AppendLine("}");

		File.WriteAllText("GL.Constants.cs", constantsBuilder.ToString());
		File.WriteAllText("GL.Functions.cs", functionsBuilder.ToString());
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

	private static void GenerateEnums(XmlNode enumsElement, StringBuilder builder)
	{
		var isBitmask = enumsElement.Attributes?["type"]?.Value == "bitmask";

		var enumType = isBitmask ? "GLbitfield" : "GLenum";

		foreach (XmlNode node in enumsElement.ChildNodes)
		{
			if (node.NodeType != XmlNodeType.Element)
				continue;

			var enumMemberName = node.Attributes?["name"]?.Value;
			var enumMemberValue = node.Attributes?["value"]?.Value;
			var enumMemberApi = node.Attributes?["api"]?.Value;
			var enumMemberType = node.Attributes?["type"]?.Value switch
			{
				"u" => "GLuint",
				"ull" => "GLuint64",
				_ => enumType
			};

			if (enumMemberApi != null || enumMemberType == null || enumMemberName == null || enumMemberValue == null)
				continue;

			enumMemberName = enumMemberName.Trim();

			builder
				.Append('\t')
				.Append("public const ")
				.Append(enumMemberType)
				.Append(' ')
				.Append(enumMemberName)
				.Append(" = unchecked((")
				.Append(enumMemberType)
				.Append(")(")
				.Append(enumMemberValue).AppendLine("));");
		}
	}

	class GlParam(string text)
	{
		public string Text = text;
		public string? Name = null;
		public string? Type = null;
	}

	class GlFunc(string name, string returnType, List<GlParam> param)
	{
		public readonly string Name = name;
		public readonly string ReturnType = returnType;
		public readonly List<GlParam> Params = param;
	}

	private static void GenerateCommands(XmlNode commandsElement, StringBuilder builder)
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

				param.Add(new(n.InnerText));
			}

			if (proto == null)
				continue;

			proto = proto.Replace("const ", "").Replace("const*", "*");
			/*
			if (proto.StartsWith("const "))
				proto = proto[6..];
			*/

			var splitIndex = proto.IndexOf('*');
			if (splitIndex < 0)
				splitIndex = proto.IndexOf(' ');
			splitIndex++;
			var returnType = proto[..splitIndex].Replace(" *", "*").Trim();
			for (var i = 0; i < param.Count; i++)
			{
				var changed = true;
				while (changed)
				{
					changed = false;
					if (param[i].Text.StartsWith("struct "))
					{
						param[i].Text = param[i].Text[7..];
						changed = true;
					}

					if (param[i].Text.StartsWith("const "))
					{
						param[i].Text = param[i].Text[6..];
						changed = true;
					}
				}

				param[i].Text = param[i].Text.Replace("* ", "*").Replace(" *", "*").Replace("const*", "*");

				var index = param[i].Text.LastIndexOf('*');
				if (index < 0)
					index = param[i].Text.LastIndexOf(' ');
				index++;
				param[i].Type = param[i].Text[..index].Trim();

				var pname = param[i].Text[index..].Trim();
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
			var type = $"delegate* unmanaged[Stdcall]<{string.Join(", ", [.. func.Params.Select(static o => o.Type), func.ReturnType])}>";
			builder.Append('\t').Append("private readonly ").Append(type).Append(" _").Append(func.Name).Append(" = (").Append(type).Append(")getProcAddress(\"")
				.Append(func.Name).Append("\");").AppendLine();
		}

		builder.AppendLine();

		foreach (var func in funcs)
		{
			var memberName = func.Name[2..];
			builder
				.Append('\t').Append("public ").Append(func.ReturnType).Append(' ').Append(memberName).Append("(").Append(string.Join(", ", func.Params.Select(p => $"{p.Type} {p.Name}")))
				.Append(") => _").Append(func.Name).Append('(').Append(string.Join(", ", func.Params.Select(p => p.Name))).AppendLine(");");
		}
	}
}
