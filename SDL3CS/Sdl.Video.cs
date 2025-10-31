using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3CS;

public static partial class Sdl
{
	public enum SystemTheme : uint
	{
		Unknown,
		Light,
		Dark
	}

	public enum DisplayOrientation : uint
	{
		Unknown,
		Landscape,
		LandscapeFlipped,
		Portrait,
		PortraitFlipped
	}

	public enum WindowFlags : ulong
	{
		Fullscreen = 0x0000000000000001,
		OpenGL = 0x0000000000000002,
		Occluded = 0x0000000000000004,
		Hidden = 0x0000000000000008,
		Borderless = 0x0000000000000010,
		Resizable = 0x0000000000000020,
		Minimized = 0x0000000000000040,
		Maximized = 0x0000000000000080,
		MouseGrabbed = 0x0000000000000100,
		InputFocus = 0x0000000000000200,
		MouseFocus = 0x0000000000000400,
		External = 0x0000000000000800,
		Modal = 0x0000000000001000,
		HighPixelDensity = 0x0000000000002000,
		MouseCapture = 0x0000000000004000,
		MouseRelativeMode = 0x0000000000008000,
		AlwaysOnTop = 0x0000000000010000,
		Utility = 0x0000000000020000,
		Tooltip = 0x0000000000040000,
		PopupMenu = 0x0000000000080000,
		KeyboardGrabbed = 0x0000000000100000,
		Vulkan = 0x0000000010000000,
		Metal = 0x0000000020000000,
		Transparent = 0x0000000040000000,
		NotFocusable = 0x0000000080000000
	}

	public enum FlashOperation
	{
		Cancel,
		Briefly,
		UntilFocused
	}

	public enum ProgressState
	{
		Învalid = -1,
		None,
		Indeterminate,
		Normal,
		Paused,
		Error
	}

	public enum HitTestResult
	{
		Normal,
		Draggable,
		ResizeTopLeft,
		ResizeTop,
		ResizeTopRight,
		ResizeRight,
		ResizeBottomRight,
		ResizeBottom,
		ResizeBottomLeft,
		ResizeLeft
	}

	public enum GLAttr
	{
		RedSize,
		GreenSize,
		BlueSize,
		AlphaSize,
		BufferSize,
		DoubleBuffer,
		DepthSize,
		StencilSize,
		AccumRedSize,
		AccumGreenSize,
		AccumBlueSize,
		AccumAlphaSize,
		Stereo,
		MultisampleBuffers,
		MultisampleSamples,
		AcceleratedVisual,
		RetainedBacking,
		ContextMajorVersion,
		ContextMinorVersion,
		ContextFlags,
		ContextProfileMask,
		ShareWithCurrentContext,
		FramebufferSrgbCapable,
		ContextReleaseBehaviour,
		ContextResetNotification,
		ContextNoError,
		FloatBuffers,
		EglPlatform
	}

	public enum GLProfile : uint
	{
		Core = 0x0001,
		Compatibility = 0x0002,
		ES = 0x0004,
	}

	[Flags]
	public enum GLContextFlags : uint
	{
		Debug = 0x0001,
		ForwardCompatible = 0x0002,
		RobusAccess = 0x0004,
		ResetIsolation = 0x0008,
	}

	[Flags]
	public enum GLContextReleaseFlags : uint
	{
		BehaviourNone = 0x0000,
		BehaviourFlush = 0x0001
	}

	public enum GLContextResetNotification : uint
	{
		None = 0x0000,
		LoseContext = 0x0001
	}

	public readonly struct DisplayID
	{
		private readonly uint value_;

		public static implicit operator uint(DisplayID obj) => obj.value_;
	}

	public readonly struct WindowID
	{
		private readonly uint value_;
		public static implicit operator uint(WindowID obj) => obj.value_;
	}

	public struct DisplayMode
	{
		public DisplayID DisplayID;
		public PixelFormat Format;
		public int Width;
		public int Height;
		public float PixelDensity;
		public float RefreshRate;
		public int RefreshRateNumerator;
		public int RefreshRateDenominator;

		private readonly nint internal_;
	}

	public readonly struct Window { }

	public readonly struct GLContext
	{
		public static readonly GLContext Null = new(0);
		private readonly nint value_;
		private GLContext(nint value) => value_ = value;
		public readonly bool IsNull
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => value_ == 0;
		}
	}

	public readonly struct EGLDisplay
	{
		public static readonly EGLDisplay Null = new(0);
		private readonly nint value_;
		private EGLDisplay(nint value) => value_ = value;

		public readonly bool IsNull
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => value_ == 0;
		}
	}

	public readonly struct EGLConfig
	{
		public static readonly EGLConfig Null = new(0);
		private readonly nint value_;
		private EGLConfig(nint value) => value_ = value;
		public readonly bool IsNull
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => value_ == 0;
		}
	}

	public readonly struct EGLSurface
	{
		public static readonly EGLSurface Null = new(0);
		private readonly nint value_;
		private EGLSurface(nint value) => value_ = value;
		public readonly bool IsNull
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => value_ == 0;
		}
	}

	public readonly struct EGLAttrib
	{
		public readonly nint Value;
	}

	public readonly struct EGLint
	{
		private readonly int Value;
	}

	public static partial class Prop
	{
		public static partial class GlobalVideo
		{
			public static partial class Wayland
			{
				public const string WlDisplayPointer = "SDL.video.wayland.wl_display";
			}
		}
	}

	public const int WindowPosUndefinedMask = 0x1FFF0000;
	public static int WindowPosUndefinedDisplay(int x) => WindowPosUndefinedMask | x;
	public static readonly int WindowPosUndefined = WindowPosUndefinedDisplay(0);
	public static bool WindowPosIsUndefined(int x) => ((x) & 0xFFFF0000) == WindowPosUndefinedMask;
	public const int WindowPosCenteredMask = 0x2FFF0000;
	public static int WindowPosCenteredDisplay(int x) => WindowPosCenteredMask | x;
	public static readonly int WindowPosCentered = WindowPosCenteredDisplay(0);
	public static bool WindowPosIsCentered(int x) => (x & 0xFFFF0000) == WindowPosCenteredMask;

	public const int WindowSurfaceVsyncDisabled = 0;
	public const int WindowSurfaceVsyncAdaptive = -1;

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate Ptr<EGLAttrib> EGLAttribArrayCallback(nint userdata);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate Ptr<EGLint> EGLIntArrayCallback(nint userdata, EGLDisplay display, EGLConfig config);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate HitTestResult HitTest(in Window win, in Point area, nint data);

	public static partial class Prop
	{
		public static partial class Display
		{
			public const string HdrEnabledBoolean = "SDL.display.HDR_enabled";
			public const string KmsdrmPanelOrientationNumber = "SDL.display.KMSDRM.panel_orientation";
			public const string WaylandWlOutputPointer = "SDL.display.wayland.wl_output";
		}

		public static partial class Window
		{
			public static partial class Create
			{
				public const string AlwaysOnTopBoolean = "SDL.window.create.always_on_top";
				public const string BorderlessBoolean = "SDL.window.create.borderless";
				public const string ConstrainPopupBoolean = "SDL.window.create.constrain_popup";
				public const string FocusableBoolean = "SDL.window.create.focusable";
				public const string ExternalGraphicsContextBoolean = "SDL.window.create.external_graphics_context";
				public const string FlagsNumber = "SDL.window.create.flags";
				public const string FullscreenBoolean = "SDL.window.create.fullscreen";
				public const string HeightNumber = "SDL.window.create.height";
				public const string HiddenBoolean = "SDL.window.create.hidden";
				public const string HighPixelDensityBoolean = "SDL.window.create.high_pixel_density";
				public const string MaximizedBoolean = "SDL.window.create.maximized";
				public const string MenuBoolean = "SDL.window.create.menu";
				public const string MetalBoolean = "SDL.window.create.metal";
				public const string MinimizedBoolean = "SDL.window.create.minimized";
				public const string ModalBoolean = "SDL.window.create.modal";
				public const string MouseGrabbedBoolean = "SDL.window.create.mouse_grabbed";
				public const string OpenGLBoolean = "SDL.window.create.opengl";
				public const string ParentPointer = "SDL.window.create.parent";
				public const string ResizableBoolean = "SDL.window.create.resizable";
				public const string TitleString = "SDL.window.create.title";
				public const string TransparentBoolean = "SDL.window.create.transparent";
				public const string TooltipBoolean = "SDL.window.create.tooltip";
				public const string UtilityBoolean = "SDL.window.create.utility";
				public const string VulkanBoolean = "SDL.window.create.vulkan";
				public const string WidthNumber = "SDL.window.create.width";
				public const string XNumber = "SDL.window.create.x";
				public const string YNumber = "SDL.window.create.y";

				public static partial class Cocoa
				{
					public const string WindowPointer = "SDL.window.create.cocoa.window";
					public const string ViewPointer = "SDL.window.create.cocoa.view";
				}

				public static partial class Wayland
				{
					public const string SurfaceRoleCustomBoolean = "SDL.window.create.wayland.surface_role_custom";
					public const string CreateEglWindowBoolean = "SDL.window.create.wayland.create_egl_window";
					public const string WlSurfacePointer = "SDL.window.create.wayland.wl_surface";
				}

				public static partial class Win32
				{
					public const string HwndPointer = "SDL.window.create.win32.hwnd";
					public const string PixelFormatHwndPointer = "SDL.window.create.win32.pixel_format_hwnd";
				}

				public static partial class X11
				{
					public const string WindowNumber = "SDL.window.create.x11.window";
				}

				public static partial class Emscripten
				{
					public const string CanvasIdString = "SDL.window.create.emscripten.canvas_id";
					public const string FillDocumentBoolean = "SDL.window.create.emscripten.fill_document";
					public const string KeyboardElementString = "SDL.window.create.emscripten.keyboard_element";
				}
			}

			public const string ShapePointer = "SDL.window.shape";
			public const string HdrEnabledBoolean = "SDL.window.HDR_enabled";
			public const string SdrWhiteLevelFloat = "SDL.window.SDR_white_level";
			public const string HdrHeadroomFloat = "SDL.window.HDR_headroom";

			public static partial class Android
			{
				public const string WindowPointer = "SDL.window.android.window";
				public const string SurfacePointer = "SDL.window.android.surface";
			}

			public static partial class UIKit
			{
				public const string WindowPointer = "SDL.window.uikit.window";
				public const string MetalViewTagNumber = "SDL.window.uikit.metal_view_tag";

				public static partial class OpenGL
				{
					public const string FramebufferNumber = "SDL.window.uikit.opengl.framebuffer";
					public const string RenderbufferNumber = "SDL.window.uikit.opengl.renderbuffer";
					public const string ResolveFramebufferNumber = "SDL.window.uikit.opengl.resolve_framebuffer";
				}
			}

			public static partial class Kmsdrm
			{
				public const string DeviceIndexNumber = "SDL.window.kmsdrm.dev_index";
				public const string DrmFdNumber = "SDL.window.kmsdrm.drm_fd";
				public const string GbmDevicePointer = "SDL.window.kmsdrm.gbm_dev";
			}

			public static partial class Cocoa
			{
				public const string WindowPointer = "SDL.window.cocoa.window";
				public const string MetalViewTagNumber = "SDL.window.cocoa.metal_view_tag";
			}

			public static partial class OpenVR
			{
				public const string OverlayIdNumber = "SDL.window.openvr.overlay_id";
			}

			public static partial class Vivante
			{
				public const string DisplayPointer = "SDL.window.vivante.display";
				public const string WindowPointer = "SDL.window.vivante.window";
				public const string SurfacePointer = "SDL.window.vivante.surface";
			}

			public static partial class Win32
			{
				public const string HwndPointer = "SDL.window.win32.hwnd";
				public const string HdcPointer = "SDL.window.win32.hdc";
				public const string InstancePointer = "SDL.window.win32.instance";
			}

			public static partial class Wayland
			{
				public const string DisplayPointer = "SDL.window.wayland.display";
				public const string SurfacePointer = "SDL.window.wayland.surface";
				public const string ViewportPointer = "SDL.window.wayland.viewport";
				public const string EglWindowPointer = "SDL.window.wayland.egl_window";
				public const string XdgSurfacePointer = "SDL.window.wayland.xdg_surface";
				public const string XdgToplevelPointer = "SDL.window.wayland.xdg_toplevel";
				public const string XdgToplevelExportHandleString = "SDL.window.wayland.xdg_toplevel_export_handle";
				public const string XdgPoputPointer = "SDL.window.wayland.xdg_popup";
				public const string XdgPositionerPointer = "SDL.window.wayland.xdg_positioner";
			}

			public static partial class X11
			{
				public const string DisplayPointer = "SDL.window.x11.display";
				public const string ScreenNumber = "SDL.window.x11.screen";
				public const string WindowNumber = "SDL.window.x11.window";
			}
			public static partial class Emscripten
			{
				public const string CanvasIdString = "SDL.window.emscripten.canvas_id";
				public const string FillDocumentBoolean = "SDL.window.emscripten.fill_document";
				public const string KeyboardElementString = "SDL.window.emscripten.keyboard_element";
			}
		}
	}

	private static partial class Native
	{

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial int SDL_GetNumVideoDrivers();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_GetVideoDriver(int index);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_GetCurrentVideoDriver();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial SystemTheme SDL_GetSystemTheme();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial DisplayID* SDL_GetDisplays(out int count);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial DisplayID SDL_GetPrimaryDisplay();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial PropertiesID SDL_GetDisplayProperties(DisplayID displayID);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_GetDisplayName(DisplayID displayID);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetDisplayBounds(DisplayID displayID, out Rect rect);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetDisplayUsableBounds(DisplayID displayID, out Rect rect);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial DisplayOrientation SDL_GetNaturalDisplayOrientation(DisplayID displayID);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial DisplayOrientation SDL_GetCurrentDisplayOrientation(DisplayID displayID);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial float GetDisplayContentScale(DisplayID displayID);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial Ptr<DisplayMode>* SDL_GetFullscreenDisplayModes(DisplayID displayID, out int count);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetClosestFullscreenDisplayMode(DisplayID displayID, int w, int h, float refreshRate, [MarshalAs(UnmanagedType.I1)] bool includeHighDensityModes, out DisplayMode closest);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<DisplayMode> SDL_GetDesktopDisplayMode(DisplayID displayID);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<DisplayMode> SDL_GetCurrentDisplayMode(DisplayID displayID);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial DisplayID SDL_GetDisplayForPoint(in Point point);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial DisplayID SDL_GetDisplayForRect(in Rect rect);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial DisplayID SDL_GetDisplayForWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial float SDL_GetWindowPixelDensity(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial float SDL_GetWindowDisplayScale(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowFullscreenMode(in Window window, in DisplayMode mode);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<DisplayMode> SDL_GetWindowFullscreenMode(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial void* SDL_GetWindowICCProfile(in Window window, out nuint size);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial PixelFormat SDL_GetWindowPixelFormat(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public unsafe static partial Ptr<Window>* SDL_GetWindows(out int count);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Window> SDL_CreateWindow(string title, int width, int height, WindowFlags flags);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Window> SDL_CreatePopupWindow(in Window parent, int offsetX, int offsetY, int width, int height, WindowFlags flags);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Window> SDL_CreateWindowWithProperties(PropertiesID props);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial WindowID SDL_GetWindowID(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Window> SDL_GetWindowFromID(WindowID id);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Window> SDL_GetWindowParent(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial PropertiesID SDL_GetWindowProperties(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial WindowFlags SDL_GetWindowFlags(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowTitle(in Window window, string title);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial string SDL_GetWindowTitle(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowIcon(in Window window, in Surface icon);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowPosition(in Window window, int x, int y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowPosition(in Window window, out int x, out int y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowSize(in Window window, int width, int height);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowSize(in Window window, out int width, out int height);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowSafeArea(in Window window, out Rect rect);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowAspectRatio(in Window window, float minAspect, float maxAspect);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowAspectRatio(in Window window, out float minAspect, out float maxAspect);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowBordersSize(in Window window, out int top, out int left, out int bottom, out int right);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowSizeInPixels(in Window window, out int width, out int height);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowMinimumSize(in Window window, int minWidth, int minHeight);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowMinimumSize(in Window window, out int width, out int height);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowMaximumSize(in Window window, int maxWidth, int maxHeight);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowMaximumSize(in Window window, out int width, out int height);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowBordered(in Window window, [MarshalAs(UnmanagedType.I1)] bool bordered);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowResizable(in Window window, [MarshalAs(UnmanagedType.I1)] bool resizable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowAlwaysOnTop(in Window window, [MarshalAs(UnmanagedType.I1)] bool onTop);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_ShowWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_HideWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_RaiseWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_MaximizeWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_MinimizeWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_RestoreWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowFullscreen(in Window window, [MarshalAs(UnmanagedType.I1)] bool fullscreen);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SyncWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_WindowHasSurface(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Surface> SDL_GetWindowSurface(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowSurfaceVSync(in Window window, int vsync);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowSurfaceVSync(in Window window, out int vsync);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_UpdateWindowSurface(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public unsafe static partial bool SDL_UpdateWindowSurfaceRects(in Window window, Rect* rects, int numrects);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_DestroyWindowSurface(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowKeyboardGrab(in Window window, [MarshalAs(UnmanagedType.I1)] bool grabbed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowMouseGrab(in Window window, [MarshalAs(UnmanagedType.I1)] bool grabbed);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowKeyboardGrab(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GetWindowMouseGrab(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Window> SDL_GetGrabbedWindow();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowMouseRect(in Window window, in Rect rect);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Rect> SDL_GetWindowMouseRect(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowOpacity(in Window window, float opacity);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial float SDL_GetWindowOpacity(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowParent(in Window window, in Window parent);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowModal(in Window window, [MarshalAs(UnmanagedType.I1)] bool modal);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowFocusable(in Window window, [MarshalAs(UnmanagedType.I1)] bool focusable);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_ShowWindowSystemMenu(in Window window, int x, int y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowHitTest(in Window window, HitTest callback, nint callbackData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowShape(in Window window, in Surface shape);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_FlashWindow(in Window window, FlashOperation operation);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowProgressState(in Window window, ProgressState state);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial ProgressState SDL_GetWindowProgressState(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_SetWindowProgressValue(in Window window, float value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial float SDL_GetWindowProgressValue(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_DestroyWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_ScreenSaverEnabled();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_EnableScreenSaver();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_DisableScreenSaver();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_LoadLibrary(string path);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_GL_GetProcAddress(string proc);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial nint SDL_EGL_GetProcAddress(string proc);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_GL_UnloadLibrary();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_ExtensionSupported(string extension);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_GL_ResetAttributes();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_SetAttribute(GLAttr attr, int value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_GetAttribute(GLAttr attr, out int value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial GLContext SDL_GL_CreateContext(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_MakeCurrent(in Window window, GLContext context);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial Ptr<Window> SDL_GL_GetCurrentWindow();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial GLContext SDL_GL_GetCurrentContext();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial EGLDisplay SDL_EGL_GetCurrentDisplay();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial EGLConfig SDL_EGL_GetCurrentConfig();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial EGLSurface SDL_EGL_GetWindowSurface(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		public static partial void SDL_EGL_SetAttributeCallbacks(EGLAttribArrayCallback platformAttribCallback, EGLIntArrayCallback surfaceAttribCallback, EGLIntArrayCallback contextAttribCallback, nint userdata);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_SetSwapInterval(int interval);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_GetSwapInterval(out int interval);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_SwapWindow(in Window window);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[LibraryImport(LibraryName, StringMarshalling = StringMarshalling.Utf8)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static partial bool SDL_GL_DestroyContext(GLContext context);
	}


	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetNumVideoDrivers() => Native.SDL_GetNumVideoDrivers();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetVideoDriver(int index) => Native.SDL_GetVideoDriver(index);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetCurrentVideoDriver() => Native.SDL_GetCurrentVideoDriver();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static SystemTheme GetSystemTheme() => Native.SDL_GetSystemTheme();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static unsafe OwnedArrayPtr<DisplayID> GetDisplays() => new(Native.SDL_GetDisplays(out var count), count);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DisplayID GetPrimaryDisplay() => Native.SDL_GetPrimaryDisplay();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PropertiesID GetDisplayProperties(DisplayID displayID) => Native.SDL_GetDisplayProperties(displayID);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetDisplayName(DisplayID displayID) => Native.SDL_GetDisplayName(displayID);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetDisplayBounds(DisplayID displayID, out Rect rect) => Native.SDL_GetDisplayBounds(displayID, out rect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetDisplayUsableBounds(DisplayID displayID, out Rect rect) => Native.SDL_GetDisplayUsableBounds(displayID, out rect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DisplayOrientation GetNaturalDisplayOrientation(DisplayID displayID) => Native.SDL_GetNaturalDisplayOrientation(displayID);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DisplayOrientation GetCurrentDisplayOrientation(DisplayID displayID) => Native.SDL_GetCurrentDisplayOrientation(displayID);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float DisplayContentScale(DisplayID displayID) => Native.GetDisplayContentScale(displayID);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static OwnedArrayPtr<Ptr<DisplayMode>> GetFullscreenDisplayModes(DisplayID displayID) => new(Native.SDL_GetFullscreenDisplayModes(displayID, out var count), count);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetClosestFullscreenDisplayMode(DisplayID displayID, int width, int height, float refreshRate, bool includeHighDensityModes, out DisplayMode closest) => Native.SDL_GetClosestFullscreenDisplayMode(displayID, width, height, refreshRate, includeHighDensityModes, out closest);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<DisplayMode> GetDesktopDisplayMode(DisplayID displayID) => Native.SDL_GetDesktopDisplayMode(displayID);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<DisplayMode> GetCurrentDisplayMode(DisplayID displayID) => Native.SDL_GetCurrentDisplayMode(displayID);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DisplayID GetDisplayForPoint(in Point point) => Native.SDL_GetDisplayForPoint(point);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DisplayID GetDisplayForRect(in Rect rect) => Native.SDL_GetDisplayForRect(rect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DisplayID GetDisplayForWindow(in Window window) => Native.SDL_GetDisplayForWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DisplayID GetDisplayForWindow(Ptr<Window> window) => Native.SDL_GetDisplayForWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetWindowPixelDensity(in Window window) => Native.SDL_GetWindowPixelDensity(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetWindowPixelDensity(Ptr<Window> window) => Native.SDL_GetWindowPixelDensity(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetWindowDisplayScale(in Window window) => Native.SDL_GetWindowDisplayScale(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetWindowDisplayScale(Ptr<Window> window) => Native.SDL_GetWindowDisplayScale(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowFullscreenMode(in Window window, in DisplayMode mode) => Native.SDL_SetWindowFullscreenMode(window, mode);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowFullscreenMode(Ptr<Window> window, in DisplayMode mode) => Native.SDL_SetWindowFullscreenMode(window.Value, mode);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<DisplayMode> GetWindowFullscreenMode(in Window window) => Native.SDL_GetWindowFullscreenMode(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<DisplayMode> GetWindowFullscreenMode(Ptr<Window> window) => Native.SDL_GetWindowFullscreenMode(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static OwnedArrayPtr<byte> GetWindowICCProfile(in Window window) => new((byte*)Native.SDL_GetWindowICCProfile(window, out var size), (int)size);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static OwnedArrayPtr<byte> GetWindowICCProfile(Ptr<Window> window) => new((byte*)Native.SDL_GetWindowICCProfile(window.Value, out var size), (int)size);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PixelFormat GetWindowPixelFormat(in Window window) => Native.SDL_GetWindowPixelFormat(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PixelFormat GetWindowPixelFormat(Ptr<Window> window) => Native.SDL_GetWindowPixelFormat(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static OwnedArrayPtr<Ptr<Window>> GetWindows() => new(Native.SDL_GetWindows(out var count), count);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> CreateWindow(string title, int width, int height, WindowFlags flags) => Native.SDL_CreateWindow(title, width, height, flags);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> CreatePopupWindow(in Window parent, int offsetX, int offsetY, int width, int height, WindowFlags flags) => Native.SDL_CreatePopupWindow(parent, offsetX, offsetY, width, height, flags);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> CreatePopupWindow(Ptr<Window> parent, int offsetX, int offsetY, int width, int height, WindowFlags flags) => Native.SDL_CreatePopupWindow(parent.Value, offsetX, offsetY, width, height, flags);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> CreateWindowWithProperties(PropertiesID props) => Native.SDL_CreateWindowWithProperties(props);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static WindowID GetWindowID(in Window window) => Native.SDL_GetWindowID(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static WindowID GetWindowID(Ptr<Window> window) => Native.SDL_GetWindowID(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> GetWindowFromID(WindowID id) => Native.SDL_GetWindowFromID(id);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> GetWindowParent(in Window window) => Native.SDL_GetWindowParent(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> GetWindowParent(Ptr<Window> window) => Native.SDL_GetWindowParent(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PropertiesID GetWindowProperties(in Window window) => Native.SDL_GetWindowProperties(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static PropertiesID GetWindowProperties(Ptr<Window> window) => Native.SDL_GetWindowProperties(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static WindowFlags GetWindowFlags(in Window window) => Native.SDL_GetWindowFlags(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static WindowFlags GetWindowFlags(Ptr<Window> window) => Native.SDL_GetWindowFlags(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowTitle(in Window window, string title) => Native.SDL_SetWindowTitle(window, title);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowTitle(Ptr<Window> window, string title) => Native.SDL_SetWindowTitle(window.Value, title);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetWindowTitle(in Window window) => Native.SDL_GetWindowTitle(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetWindowTitle(Ptr<Window> window) => Native.SDL_GetWindowTitle(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowIcon(in Window window, in Surface icon) => Native.SDL_SetWindowIcon(window, icon);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowIcon(Ptr<Window> window, in Surface icon) => Native.SDL_SetWindowIcon(window.Value, icon);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowPosition(in Window window, int x, int y) => Native.SDL_SetWindowPosition(window, x, y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowPosition(Ptr<Window> window, int x, int y) => Native.SDL_SetWindowPosition(window.Value, x, y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowPosition(in Window window, out int x, out int y) => Native.SDL_GetWindowPosition(window, out x, out y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowPosition(Ptr<Window> window, out int x, out int y) => Native.SDL_GetWindowPosition(window.Value, out x, out y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowSize(in Window window, int width, int height) => Native.SDL_SetWindowSize(window, width, height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowSize(Ptr<Window> window, int width, int height) => Native.SDL_SetWindowSize(window.Value, width, height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowSize(in Window window, out int width, out int height) => Native.SDL_GetWindowSize(window, out width, out height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowSize(Ptr<Window> window, out int width, out int height) => Native.SDL_GetWindowSize(window.Value, out width, out height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowSafeArea(in Window window, out Rect rect) => Native.SDL_GetWindowSafeArea(window, out rect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowSafeArea(Ptr<Window> window, out Rect rect) => Native.SDL_GetWindowSafeArea(window.Value, out rect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowAspectRatio(in Window window, float minAspect, float maxAspect) => Native.SDL_SetWindowAspectRatio(window, minAspect, maxAspect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowAspectRatio(Ptr<Window> window, float minAspect, float maxAspect) => Native.SDL_SetWindowAspectRatio(window.Value, minAspect, maxAspect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowAspectRatio(in Window window, out float minAspect, out float maxAspect) => Native.SDL_GetWindowAspectRatio(window, out minAspect, out maxAspect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowAspectRatio(Ptr<Window> window, out float minAspect, out float maxAspect) => Native.SDL_GetWindowAspectRatio(window.Value, out minAspect, out maxAspect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowBordersSize(in Window window, out int top, out int left, out int bottom, out int right) => Native.SDL_GetWindowBordersSize(window, out top, out left, out bottom, out right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowBordersSize(Ptr<Window> window, out int top, out int left, out int bottom, out int right) => Native.SDL_GetWindowBordersSize(window.Value, out top, out left, out bottom, out right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowSizeInPixels(in Window window, out int width, out int height) => Native.SDL_GetWindowSizeInPixels(window, out width, out height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowSizeInPixels(Ptr<Window> window, out int width, out int height) => Native.SDL_GetWindowSizeInPixels(window.Value, out width, out height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowMinimumSize(in Window window, int minWidth, int minHeight) => Native.SDL_SetWindowMinimumSize(window, minWidth, minHeight);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowMinimumSize(Ptr<Window> window, int minWidth, int minHeight) => Native.SDL_SetWindowMinimumSize(window.Value, minWidth, minHeight);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowMinimumSize(in Window window, out int width, out int height) => Native.SDL_GetWindowMinimumSize(window, out width, out height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowMinimumSize(Ptr<Window> window, out int width, out int height) => Native.SDL_GetWindowMinimumSize(window.Value, out width, out height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowMaximumSize(in Window window, int maxWidth, int maxHeight) => Native.SDL_SetWindowMaximumSize(window, maxWidth, maxHeight);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowMaximumSize(Ptr<Window> window, int maxWidth, int maxHeight) => Native.SDL_SetWindowMaximumSize(window.Value, maxWidth, maxHeight);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowMaximumSize(in Window window, out int width, out int height) => Native.SDL_GetWindowMaximumSize(window, out width, out height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowMaximumSize(Ptr<Window> window, out int width, out int height) => Native.SDL_GetWindowMaximumSize(window.Value, out width, out height);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowBordered(in Window window, bool bordered) => Native.SDL_SetWindowBordered(window, bordered);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowBordered(Ptr<Window> window, bool bordered) => Native.SDL_SetWindowBordered(window.Value, bordered);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowResizable(in Window window, bool resizable) => Native.SDL_SetWindowResizable(window, resizable);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowResizable(Ptr<Window> window, bool resizable) => Native.SDL_SetWindowResizable(window.Value, resizable);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowAlwaysOnTop(in Window window, bool onTop) => Native.SDL_SetWindowAlwaysOnTop(window, onTop);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowAlwaysOnTop(Ptr<Window> window, bool onTop) => Native.SDL_SetWindowAlwaysOnTop(window.Value, onTop);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ShowWindow(in Window window) => Native.SDL_ShowWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ShowWindow(Ptr<Window> window) => Native.SDL_ShowWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool HideWindow(in Window window) => Native.SDL_HideWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool HideWindow(Ptr<Window> window) => Native.SDL_HideWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool RaiseWindow(in Window window) => Native.SDL_RaiseWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool RaiseWindow(Ptr<Window> window) => Native.SDL_RaiseWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool MaximizeWindow(in Window window) => Native.SDL_MaximizeWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool MaximizeWindow(Ptr<Window> window) => Native.SDL_MaximizeWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool MinimizeWindow(in Window window) => Native.SDL_MinimizeWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool MinimizeWindow(Ptr<Window> window) => Native.SDL_MinimizeWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool RestoreWindow(in Window window) => Native.SDL_RestoreWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool RestoreWindow(Ptr<Window> window) => Native.SDL_RestoreWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowFullscreen(in Window window, bool fullscreen) => Native.SDL_SetWindowFullscreen(window, fullscreen);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowFullscreen(Ptr<Window> window, bool fullscreen) => Native.SDL_SetWindowFullscreen(window.Value, fullscreen);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SyncWindow(in Window window) => Native.SDL_SyncWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SyncWindow(Ptr<Window> window) => Native.SDL_SyncWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool WindowHasSurface(in Window window) => Native.SDL_WindowHasSurface(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool WindowHasSurface(Ptr<Window> window) => Native.SDL_WindowHasSurface(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Surface> GetWindowSurface(in Window window) => Native.SDL_GetWindowSurface(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Surface> GetWindowSurface(Ptr<Window> window) => Native.SDL_GetWindowSurface(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowSurfaceVSync(in Window window, int vsync) => Native.SDL_SetWindowSurfaceVSync(window, vsync);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowSurfaceVSync(Ptr<Window> window, int vsync) => Native.SDL_SetWindowSurfaceVSync(window.Value, vsync);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowSurfaceVSync(in Window window, out int vsync) => Native.SDL_GetWindowSurfaceVSync(window, out vsync);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowSurfaceVSync(Ptr<Window> window, out int vsync) => Native.SDL_GetWindowSurfaceVSync(window.Value, out vsync);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool UpdateWindowSurface(in Window window) => Native.SDL_UpdateWindowSurface(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool UpdateWindowSurface(Ptr<Window> window) => Native.SDL_UpdateWindowSurface(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static bool UpdateWindowSurfaceRects(in Window window, ReadOnlySpan<Rect> rects)
	{
		fixed (Rect* rectsPtr = rects)

			return Native.SDL_UpdateWindowSurfaceRects(window, rectsPtr, rects.Length);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static bool UpdateWindowSurfaceRects(Ptr<Window> window, ReadOnlySpan<Rect> rects)
	{
		fixed (Rect* rectsPtr = rects)
			return Native.SDL_UpdateWindowSurfaceRects(window.Value, rectsPtr, rects.Length);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool DestroyWindowSurface(in Window window) => Native.SDL_DestroyWindowSurface(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool DestroyWindowSurface(Ptr<Window> window) => Native.SDL_DestroyWindowSurface(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowKeyboardGrab(in Window window, bool grabbed) => Native.SDL_SetWindowKeyboardGrab(window, grabbed);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowKeyboardGrab(Ptr<Window> window, bool grabbed) => Native.SDL_SetWindowKeyboardGrab(window.Value, grabbed);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowMouseGrab(in Window window, bool grabbed) => Native.SDL_SetWindowMouseGrab(window, grabbed);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowMouseGrab(Ptr<Window> window, bool grabbed) => Native.SDL_SetWindowMouseGrab(window.Value, grabbed);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowKeyboardGrab(in Window window) => Native.SDL_GetWindowKeyboardGrab(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowKeyboardGrab(Ptr<Window> window) => Native.SDL_GetWindowKeyboardGrab(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowMouseGrab(in Window window) => Native.SDL_GetWindowMouseGrab(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GetWindowMouseGrab(Ptr<Window> window) => Native.SDL_GetWindowMouseGrab(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> GetGrabbedWindow() => Native.SDL_GetGrabbedWindow();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowMouseRect(in Window window, in Rect rect) => Native.SDL_SetWindowMouseRect(window, rect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowMouseRect(Ptr<Window> window, in Rect rect) => Native.SDL_SetWindowMouseRect(window.Value, rect);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Rect> GetWindowMouseRect(in Window window) => Native.SDL_GetWindowMouseRect(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Rect> GetWindowMouseRect(Ptr<Window> window) => Native.SDL_GetWindowMouseRect(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowOpacity(in Window window, float opacity) => Native.SDL_SetWindowOpacity(window, opacity);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowOpacity(Ptr<Window> window, float opacity) => Native.SDL_SetWindowOpacity(window.Value, opacity);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetWindowOpacity(in Window window) => Native.SDL_GetWindowOpacity(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetWindowOpacity(Ptr<Window> window) => Native.SDL_GetWindowOpacity(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowParent(in Window window, in Window parent) => Native.SDL_SetWindowParent(window, parent);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowParent(Ptr<Window> window, Ptr<Window> parent) => Native.SDL_SetWindowParent(window.Value, parent.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowModal(in Window window, bool modal) => Native.SDL_SetWindowModal(window, modal);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowModal(Ptr<Window> window, bool modal) => Native.SDL_SetWindowModal(window.Value, modal);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowFocusable(in Window window, bool focusable) => Native.SDL_SetWindowFocusable(window, focusable);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowFocusable(Ptr<Window> window, bool focusable) => Native.SDL_SetWindowFocusable(window.Value, focusable);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ShowWindowSystemMenu(in Window window, int x, int y) => Native.SDL_ShowWindowSystemMenu(window, x, y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ShowWindowSystemMenu(Ptr<Window> window, int x, int y) => Native.SDL_ShowWindowSystemMenu(window.Value, x, y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowHitTest(in Window window, HitTest callback, nint callbackData) => Native.SDL_SetWindowHitTest(window, callback, callbackData);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowHitTest(Ptr<Window> window, HitTest callback, nint callbackData) => Native.SDL_SetWindowHitTest(window.Value, callback, callbackData);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowShape(in Window window, in Surface shape) => Native.SDL_SetWindowShape(window, shape);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowShape(Ptr<Window> window, in Surface shape) => Native.SDL_SetWindowShape(window.Value, shape);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool FlashWindow(in Window window, FlashOperation operation) => Native.SDL_FlashWindow(window, operation);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool FlashWindow(Ptr<Window> window, FlashOperation operation) => Native.SDL_FlashWindow(window.Value, operation);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowProgressState(in Window window, ProgressState state) => Native.SDL_SetWindowProgressState(window, state);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowProgressState(Ptr<Window> window, ProgressState state) => Native.SDL_SetWindowProgressState(window.Value, state);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ProgressState GetWindowProgressState(in Window window) => Native.SDL_GetWindowProgressState(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ProgressState GetWindowProgressState(Ptr<Window> window) => Native.SDL_GetWindowProgressState(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowProgressValue(in Window window, float value) => Native.SDL_SetWindowProgressValue(window, value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool SetWindowProgressValue(Ptr<Window> window, float value) => Native.SDL_SetWindowProgressValue(window.Value, value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetWindowProgressValue(in Window window) => Native.SDL_GetWindowProgressValue(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static float GetWindowProgressValue(Ptr<Window> window) => Native.SDL_GetWindowProgressValue(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DestroyWindow(in Window window) => Native.SDL_DestroyWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void DestroyWindow(Ptr<Window> window) => Native.SDL_DestroyWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool ScreenSaverEnabled() => Native.SDL_ScreenSaverEnabled();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool EnableScreenSaver() => Native.SDL_EnableScreenSaver();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool DisableScreenSaver() => Native.SDL_DisableScreenSaver();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_LoadLibrary(string path) => Native.SDL_GL_LoadLibrary(path);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static nint GL_GetProcAddress(string proc) => Native.SDL_GL_GetProcAddress(proc);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static nint EGL_GetProcAddress(string proc) => Native.SDL_EGL_GetProcAddress(proc);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GL_UnloadLibrary() => Native.SDL_GL_UnloadLibrary();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_ExtensionSupported(string extension) => Native.SDL_GL_ExtensionSupported(extension);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void GL_ResetAttributes() => Native.SDL_GL_ResetAttributes();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_SetAttribute(GLAttr attr, int value) => Native.SDL_GL_SetAttribute(attr, value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_SetAttribute(GLAttr attr, GLContextFlags value) => Native.SDL_GL_SetAttribute(attr, (int)value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_SetAttribute(GLAttr attr, GLProfile value) => Native.SDL_GL_SetAttribute(attr, (int)value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_GetAttribute(GLAttr attr, out int value) => Native.SDL_GL_GetAttribute(attr, out value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static GLContext GL_CreateContext(in Window window) => Native.SDL_GL_CreateContext(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static GLContext GL_CreateContext(Ptr<Window> window) => Native.SDL_GL_CreateContext(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_MakeCurrent(in Window window, GLContext context) => Native.SDL_GL_MakeCurrent(window, context);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_MakeCurrent(Ptr<Window> window, GLContext context) => Native.SDL_GL_MakeCurrent(window.Value, context);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Ptr<Window> GL_GetCurrentWindow() => Native.SDL_GL_GetCurrentWindow();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static GLContext GL_GetCurrentContext() => Native.SDL_GL_GetCurrentContext();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static EGLDisplay EGL_GetCurrentDisplay() => Native.SDL_EGL_GetCurrentDisplay();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static EGLConfig EGL_GetCurrentConfig() => Native.SDL_EGL_GetCurrentConfig();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static EGLSurface EGL_GetWindowSurface(in Window window) => Native.SDL_EGL_GetWindowSurface(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static EGLSurface EGL_GetWindowSurface(Ptr<Window> window) => Native.SDL_EGL_GetWindowSurface(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void EGL_SetAttributeCallbacks(EGLAttribArrayCallback platformAttribCallback, EGLIntArrayCallback surfaceAttribCallback, EGLIntArrayCallback contextAttribCallback, nint userdata) => Native.SDL_EGL_SetAttributeCallbacks(platformAttribCallback, surfaceAttribCallback, contextAttribCallback, userdata);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_SetSwapInterval(int interval) => Native.SDL_GL_SetSwapInterval(interval);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_GetSwapInterval(out int interval) => Native.SDL_GL_GetSwapInterval(out interval);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_SwapWindow(in Window window) => Native.SDL_GL_SwapWindow(window);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_SwapWindow(Ptr<Window> window) => Native.SDL_GL_SwapWindow(window.Value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool GL_DestroyContext(GLContext context) => Native.SDL_GL_DestroyContext(context);
}
