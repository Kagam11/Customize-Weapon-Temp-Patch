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
            Log.Message($"[BorderlessCustomize] Found {allGuns.Count()} ThingDefs in the database.");
            allGuns = allGuns.Where(x => x.IsRangedWeapon);
            Log.Message($"[BorderlessCustomize] Found {allGuns.Count()} Ranged Weapon ThingDefs in the database.");
            allGuns = allGuns.Where(x => x.Verbs is not null);
            Log.Message($"[BorderlessCustomize] Found {allGuns.Count()} Weapon with verb in the database.");
            allGuns = allGuns.Where(x => x.Verbs.Any(v => v.verbClass.Name.StartsWith(typeof(Verb_Shoot).Name)));
            Log.Message($"[BorderlessCustomize] Found {allGuns.Count()} Gun ThingDefs in the database.");
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
                item.TryAddComp(new CompProperties_Renamable());
                item.TryAddComp(new CompProperties_Colorable());
                item.TryAddComp(new CompProperties_AbilityProvider());
                Log.Message($"[BorderlessCustomize] Modified Gun ThingDef: {item.defName}");
            }
        }
    }
}
