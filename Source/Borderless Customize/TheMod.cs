using CWF;
using System.Linq;
using Verse;

namespace BorderlessCustomize
{
    [StaticConstructorOnStartup]
    internal static class BcStartUp
    {
        static readonly BcSettings settings;

        static BcStartUp()
        {
            settings = BorderlessCustomize.settings;
            var partDefs = DefDatabase<PartDef>.AllDefs.ToList();
            var tags = Mods.AllWeaponTags;
            var allGuns = DefDatabase<ThingDef>.AllDefs;
            allGuns = allGuns.Where(x => x.IsRangedWeapon);
            allGuns = allGuns.Where(x => x.Verbs is not null);
            allGuns = allGuns.Where(x => x.Verbs.Any(v => v.verbClass.Name.StartsWith(typeof(Verb_Shoot).Name)));
            foreach (var item in allGuns)
            {
                item.comps ??= new();
                item.weaponTags ??= new();
                item.TryAddComp(new CompProperties_DynamicTraits());
                var dynamicTraits = item.comps.OfType<CompProperties_DynamicTraits>().FirstOrDefault();
                if (settings.OverwriteMode)
                {
                    dynamicTraits.supportParts?.Clear();
                }
                else if (dynamicTraits.supportParts.Any()) continue;
                dynamicTraits.supportParts.AddRange(partDefs);
                item.weaponTags = item.weaponTags.Union(tags).ToList();
                if (settings.IsAddRename) item.TryAddComp(new CompProperties_Renamable());
                if (settings.IsAddColor) item.TryAddComp(new CompProperties_Colorable());
                item.TryAddComp(new CompProperties_AbilityProvider());
            }
        }
    }
}
