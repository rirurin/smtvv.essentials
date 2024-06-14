using Reloaded.Mod.Interfaces.Structs;
using smtvv.essentials.Template.Configuration;
using System.ComponentModel;

namespace smtvv.essentials.Configuration
{
    public class Config : Configurable<Config>
    {

        [DisplayName("Render In Background")]
        [Description("Makes the game continue running when not in focus.")]
        [DefaultValue(true)]
        public bool NoPauseUnfocus { get; set; } = true;

        [DisplayName("Intro Skip")]
        [Description("Skip to the main menu")]
        [DefaultValue(true)]
        public bool IntroSkip { get; set; } = true;
    }

    /// <summary>
    /// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
    /// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
    /// </summary>
    public class ConfiguratorMixin : ConfiguratorMixinBase
    {
        // 
    }
}
