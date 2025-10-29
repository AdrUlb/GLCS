using System.Runtime.InteropServices;
using System.Text;

namespace GLCS;

public unsafe partial class GL(GL.GetProcAddress getProcAddress)
{
	public delegate nint GetProcAddress(string proc);
}
