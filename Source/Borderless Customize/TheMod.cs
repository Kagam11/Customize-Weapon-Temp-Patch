using CWF;
using RimWorld;
using System.Collections.Generic;
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
            // get parts
            var partDefs = DefDatabase<PartDef>.AllDefs.ToList();
            //var tags = new List<string>();

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
            allGuns = allGuns.Where(x => CanShoot(x));
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
                //item.weaponTags = item.weaponTags.Union(tags).ToList();
                if (settings.IsAddRename) item.TryAddComp(new CompProperties_Renamable());
                if (settings.IsAddColor) item.TryAddComp(new CompProperties_Colorable());
                item.TryAddComp(new CompProperties_AbilityProvider());
            }
        }

        private static bool CanShoot(ThingDef thing)
        {
            if (thing == null) return false;
            if (!thing.IsRangedWeapon) return false;
            if (!(thing.Verbs is { })) return false;
            foreach (var verb in thing.Verbs)
            {
                var name = verb.verbClass.Name;
                // Vanilla and CE
                if (name.StartsWith(typeof(Verb_Shoot).Name)) return true;
                // Milira
                if (name.StartsWith("Verb_ChargeShoot")) return true;
                // Moelotl
                if (name.StartsWith("Verb_LotlQi")) return true;
            }
            return false;
        }
    }
}
