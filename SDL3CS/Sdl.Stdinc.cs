using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class Sdl
{
	public readonly struct Environment { }

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate nint SDL_malloc_func(nuint size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate nint SDL_calloc_func(nuint nmemb, nuint size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate nint SDL_realloc_func(nint mem, nuint size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SDL_free_func(nint mem);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate int SDL_CompareCallback(void* a, void* b);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate int SDL_CompareCallback_r(nint userdata, void* a, void* b);

	public static uint DefineFourCc(byte a, byte b, byte c, byte d) =>
		((uint)a << 0) |
		((uint)b << 8) |
		((uint)c << 16) |
		((uint)d << 24);

	public static uint DefineFourCc(char a, char b, char c, char d) => DefineFourCc((byte)a, (byte)b, (byte)c, (byte)d);

	private static partial class Native
	{

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_malloc(nuint size);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_calloc(nuint nmemb, nuint size);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_realloc(nint mem, nuint size);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_free(nint mem);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_GetOriginalMemoryFunctions(out SDL_malloc_func malloc_func, out SDL_calloc_func calloc_func, out SDL_realloc_func realloc_func, out SDL_free_func free_func);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_GetMemoryFunctions(out SDL_malloc_func malloc_func, out SDL_calloc_func calloc_func, out SDL_realloc_func realloc_func, out SDL_free_func free_func);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetMemoryFunctions(SDL_malloc_func malloc_func, SDL_calloc_func calloc_func, SDL_realloc_func realloc_func, SDL_free_func free_func);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_aligned_alloc(nuint alignment, nuint size);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_aligned_free(nint mem);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial int SDL_GetNumAllocations();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Environment> SDL_GetEnvironment();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Environment> SDL_CreateEnvironment([MarshalAs(UnmanagedType.I1)] bool populated);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_GetEnvironmentVariable(in Environment env, string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial Ptr<byte>* SDL_GetEnvironmentVariables(in Environment env);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetEnvironmentVariable(in Environment env, string name, string value, [MarshalAs(UnmanagedType.I1)] bool overwrite);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_UnsetEnvironmentVariable(in Environment env, string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_DestroyEnvironment(in Environment env);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_getenv(string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_getenv_unsafe(string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial int SDL_setenv_unsafe(string name, string value, int overwrite);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial int SDL_unsetenv_unsafe(string name);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial void SDL_qsort(void* @base, nuint nmemb, nuint size, SDL_CompareCallback compare);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial void* SDL_bsearch(void* key, void* @base, nuint nmemb, nuint size, SDL_CompareCallback compare);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial void SDL_qsort_r(void* @base, nuint nmemb, nuint size, SDL_CompareCallback_r compare, nint userdata);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial void* SDL_bsearch_r(void* key, void* @base, nuint nmemb, nuint size, SDL_CompareCallback_r compare, nint userdata);
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static nint malloc(nuint size) => Native.SDL_malloc(size);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static nint calloc(nuint nmemb, nuint size) => Native.SDL_calloc(nmemb, size);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static nint realloc(nint mem, nuint size) => Native.SDL_realloc(mem, size);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void free(nint mem) => Native.SDL_free(mem);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GetOriginalMemoryFunctions(out SDL_malloc_func malloc_func, out SDL_calloc_func calloc_func, out SDL_realloc_func realloc_func, out SDL_free_func free_func) => Native.SDL_GetOriginalMemoryFunctions(out malloc_func, out calloc_func, out realloc_func, out free_func);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GetMemoryFunctions(out SDL_malloc_func malloc_func, out SDL_calloc_func calloc_func, out SDL_realloc_func realloc_func, out SDL_free_func free_func) => Native.SDL_GetMemoryFunctions(out malloc_func, out calloc_func, out realloc_func, out free_func);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetMemoryFunctions(SDL_malloc_func malloc_func, SDL_calloc_func calloc_func, SDL_realloc_func realloc_func, SDL_free_func free_func) => Native.SDL_SetMemoryFunctions(malloc_func, calloc_func, realloc_func, free_func);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static nint aligned_alloc(nuint alignment, nuint size) => Native.SDL_aligned_alloc(alignment, size);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void aligned_free(nint mem) => Native.SDL_aligned_free(mem);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetNumAllocations() => Native.SDL_GetNumAllocations();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Environment> GetEnvironment() => Native.SDL_GetEnvironment();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Environment> CreateEnvironment(bool populated) => Native.SDL_CreateEnvironment(populated);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetEnvironmentVariable(in Environment env, string name) => Native.SDL_GetEnvironmentVariable(env, name);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static OwnedArrayPtr<Ptr<byte>> GetEnvironmentVariables(in Environment env)
	{
		var ptr = Native.SDL_GetEnvironmentVariables(env);
		var count = 0;
		if (ptr != null)
		{
			while (ptr[count].IsNull == false)
				count++;
		}
		return new(ptr, count);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetEnvironmentVariable(in Environment env, string name, string value, bool overwrite) => Native.SDL_SetEnvironmentVariable(env, name, value, overwrite);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool UnsetEnvironmentVariable(in Environment env, string name) => Native.SDL_UnsetEnvironmentVariable(env, name);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DestroyEnvironment(in Environment env) => Native.SDL_DestroyEnvironment(env);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string getenv(string name) => Native.SDL_getenv(name);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string getenv_unsafe(string name) => Native.SDL_getenv_unsafe(name);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int setenv_unsafe(string name, string value, int overwrite) => Native.SDL_setenv_unsafe(name, value, overwrite);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int unsetenv_unsafe(string name) => Native.SDL_unsetenv_unsafe(name);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static void qsort(void* @base, nuint nmemb, nuint size, SDL_CompareCallback compare) => Native.SDL_qsort(@base, nmemb, size, compare);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static void* bsearch(void* key, void* @base, nuint nmemb, nuint size, SDL_CompareCallback compare) => Native.SDL_bsearch(key, @base, nmemb, size, compare);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static void qsort_r(void* @base, nuint nmemb, nuint size, SDL_CompareCallback_r compare, nint userdata) => Native.SDL_qsort_r(@base, nmemb, size, compare, userdata);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static void* bsearch_r(void* key, void* @base, nuint nmemb, nuint size, SDL_CompareCallback_r compare, nint userdata) => Native.SDL_bsearch_r(key, @base, nmemb, size, compare, userdata);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static bool size_mul_check_overflow(nuint a, nuint b, out nuint ret)
	{
		if (a != 0 && b > (nuint)sizeof(nint) / a)
		{
			ret = 0;
			return false;
		}
		ret = a * b;
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static bool size_add_check_overflow(nuint a, nuint b, out nuint ret)
	{
		if (b > (nuint)sizeof(nint) - a)
		{
			ret = 0;
			return false;
		}

		ret = a + b;
		return true;
	}
}
