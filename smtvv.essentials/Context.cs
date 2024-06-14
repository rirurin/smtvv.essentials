using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Memory;
using Reloaded.Memory.SigScan.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using riri.commonmodutils;
using SharedScans.Interfaces;
using smtvv.essentials.Configuration;
using UnrealEssentials.Interfaces;

namespace smtvv.essentials
{
    public class EssentialContext : Context
    {
        public new Config _config { get; set; }
        public IUnrealEssentials _unrealEssentials { get; private set; }
        public EssentialContext(long baseAddress, IConfigurable config, ILogger logger, IStartupScanner startupScanner, IReloadedHooks hooks,
            string modLocation, Utils utils, Memory memory, ISharedScans sharedScans, IUnrealEssentials unrealEssentials)
            : base(baseAddress, config, logger, startupScanner, hooks, modLocation, utils, memory, sharedScans)
        {
            _config = (Config)config;
            _unrealEssentials = unrealEssentials;
        }
        public override void OnConfigUpdated(IConfigurable newConfig) => _config = (Config)newConfig;
    }
}
