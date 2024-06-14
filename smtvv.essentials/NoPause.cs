using Reloaded.Hooks.Definitions;
using Reloaded.Mod.Interfaces;
using riri.commonmodutils;
using smtvv.essentials.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static smtvv.essentials.Native;

namespace smtvv.essentials
{
    // See https://github.com/AnimatedSwine37/p3rpc.essentials/blob/master/p3rpc.essentials/Patches/NoPauseOnFocusLoss.cs
    public class NoPause : ModuleBase<EssentialContext>
    {
        private static IReloadedHooks _hooks = null!;
        private static WndProcHook _wndProcHook = null!;
        private static Action<string> Debug;
        private static IHook<SetupWindowDelegate> _setupWindowHook;
        private static bool bNoPauseUnfocus;

        private string FWindowsWindow_Initialize_SIG = "4C 8B DC 53 55 56 41 54 41 55 41 56";
        
        public unsafe NoPause(EssentialContext context, Dictionary<string, ModuleBase<EssentialContext>> modules) : base(context, modules)
        {
            bNoPauseUnfocus = _context._config.NoPauseUnfocus;
            _hooks = _context._hooks;
            Debug = str => _context._utils.Log($"{str}");
            _context._utils.SigScan(FWindowsWindow_Initialize_SIG, "FWindowsWindow::Initialize", _context._utils.GetDirectAddress,
                    addr => _setupWindowHook = _context._utils.MakeHooker<SetupWindowDelegate>(SetupWindow, addr));
        }
        public override void Register()
        {
            
        }

        public override void OnConfigUpdated(IConfigurable newConfig)
        {
            base.OnConfigUpdated(newConfig);
            bNoPauseUnfocus = ((Config)newConfig).NoPauseUnfocus;
        }

        private static unsafe void SetupWindow(WindowInfo* info, nuint param_2, nuint param_3, nuint param_4, nuint param_5)
        {
            _setupWindowHook.OriginalFunction(info, param_2, param_3, param_4, param_5);
            Debug("Got Window, Hooking WndProc.");
            var wndProcHandlerPtr = (IntPtr)_hooks.Utilities.GetFunctionPointer(typeof(NoPause), nameof(WndProcImpl));
            _wndProcHook = WndProcHook.Create(_hooks, info->hWnd, Unsafe.As<IntPtr, WndProcFn>(ref wndProcHandlerPtr));
        }

        [UnmanagedCallersOnly]
        private static unsafe IntPtr WndProcImpl(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
        {
            if (!bNoPauseUnfocus)
                return _wndProcHook.Hook.OriginalFunction.Value.Invoke(hWnd, uMsg, wParam, lParam);

            switch (uMsg)
            {
                case WM_ACTIVATE:
                case WM_ACTIVATEAPP:
                    if (wParam == IntPtr.Zero)
                        return IntPtr.Zero;

                    break;

                case WM_KILLFOCUS:
                    return IntPtr.Zero;
            }

            return _wndProcHook.Hook.OriginalFunction.Value.Invoke(hWnd, uMsg, wParam, lParam);
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct WindowInfo
        {
            [FieldOffset(0x28)]
            public nint hWnd;
        }

        private unsafe delegate void SetupWindowDelegate(WindowInfo* info, nuint param_2, nuint param_3, nuint param_4, nuint param_5);
    }
}
