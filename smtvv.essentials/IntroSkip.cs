using riri.commonmodutils;

namespace smtvv.essentials
{
    public class IntroSkip : ModuleBase<EssentialContext>
    {
        public unsafe IntroSkip(EssentialContext context, Dictionary<string, ModuleBase<EssentialContext>> modules) : base(context, modules)
        {
            if (_context._config.IntroSkip)
                _context._unrealEssentials.AddFromFolder(Path.Combine(_context._modLocation, "IntroSkip"));
        }
        public override void Register()
        {

        }
    }
}
