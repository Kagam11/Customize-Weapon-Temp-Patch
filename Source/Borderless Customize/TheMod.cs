using CWF;
using System.Linq;
using Verse;

namespace BorderlessCustomize
{
    [StaticConstructorOnStartup]
    internal static class BcStartUp
    {
        private static ThingCategoryDef grenadeDef;
        static readonly BcSettings settings;

        static BcStartUp()
        {
            settings = BorderlessCustomize.settings;
            // get parts
            var partDefs = DefDatabase<PartDef>.AllDefs.ToList();

            grenadeDef = DefDatabase<ThingCategoryDef>.GetNamed("Grenades");

            // get weapontags
            var modules = CWF.ModuleDatabase.AllModuleDefs.ToList();
            foreach (var module in modules)
            {
                if (module.GetModExtension<TraitModuleExtension>() is { } extension)
                {
                    extension.requiredWeaponTags = null;
                    extension.requiredWeaponDefs = null;
                    extension.excludeWeaponDefs = null;
                    extension.excludeWeaponTags = null;
                }
            }


            var allGuns = DefDatabase<ThingDef>.AllDefs;
            allGuns = allGuns.Where(x => NotGrenade(x));
            foreach (var item in allGuns)
            {
                if (item.defName == "Gun_Scattergun") continue;
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
                if (settings.IsAddRename) item.TryAddComp(new CompProperties_Renamable());
                if (settings.IsAddColor) item.TryAddComp(new CompProperties_Colorable());
                item.TryAddComp(new CompProperties_AbilityProvider());
            }
        }

        private static bool NotGrenade(ThingDef thing)
        {
            if (thing == null) return false;
            if (!thing.IsRangedWeapon) return false;
            //if (thing.thingCategories.Any(x => x.defName == "Grenades")) return false;
            if (grenadeDef.ContainedInThisOrDescendant(thing)) return false;
            return true;
        }
    }
}
