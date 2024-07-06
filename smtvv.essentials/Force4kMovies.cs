using Reloaded.Hooks.Definitions;
using Reloaded.Memory.SigScan.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using riri.commonmodutils;
using smtvv.essentials.Configuration;

namespace smtvv.essentials
{
    public class Force4kMovies : ModuleBase<EssentialContext>
    {
        private delegate bool IsOriginalMovieResolutionDelegate();
        private IHook<IsOriginalMovieResolutionDelegate> _movieResHook;

        private string UEventFunctionLibrary__execIsOriginalMovieResolution_SIG = "48 83 EC 28 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 81 78 ?? D0 07 00 00";

        private static IReloadedHooks _hooks = null!;
        private IStartupScanner _scanner;

        private static bool bForce4kMovies;

        public unsafe Force4kMovies(EssentialContext context, Dictionary<string, ModuleBase<EssentialContext>> modules) : base(context, modules)
        {
            bForce4kMovies = _context._config.Force4kMovies;
            _hooks = _context._hooks;

            if (bForce4kMovies) _context._utils.SigScan(UEventFunctionLibrary__execIsOriginalMovieResolution_SIG, "UEventFunctionLibrary::execIsOriginalMovieResolution", _context._utils.GetDirectAddress,
                    addr => _movieResHook = _hooks.CreateHook<IsOriginalMovieResolutionDelegate>(IsOriginalMovieResolutionImpl, addr).Activate());
        }
        public override void Register()
        {

        }
        private static bool IsOriginalMovieResolutionImpl()
        {
            return false;
        }
    }
}
