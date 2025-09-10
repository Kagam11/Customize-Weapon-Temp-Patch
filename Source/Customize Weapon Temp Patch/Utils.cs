using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Customize_Weapon_Temp_Patch
{
    public static class Utils
    {
        public enum ModRaces
        {
            Wolfein,
            Miho,
            Kiiro,
            Milira,
            Cinder,
            Ratkin,
            RatkinWeaponPlus,
            RatkinOberoniaAurea,
            Moyo,
            MoyoCartel,
            MoyoAbyss,
            Moyo2,
            Paniel,
            Anty,
            Moosesian,
            Aya,
            Grimworld,
            FalloutHssn,
            FalloutRetro,
            KurinHar,
            KurinMeow,
            //Yuran,
        };
        public static Dictionary<ModRaces, string> ModPackageIds = new Dictionary<ModRaces, string>
        {
            { ModRaces.Wolfein, "melondove.wolfeinrace" },
            { ModRaces.Miho, "miho.fortifiedoutremer" },
            { ModRaces.Kiiro, "ancot.kiirorace" },
            { ModRaces.Milira, "ancot.milirarace" },
            { ModRaces.Cinder, "breadmo.cinders" },
            { ModRaces.Ratkin, "fxz.solaris.ratkinracemod.odyssey" },
            { ModRaces.RatkinWeaponPlus, "bbb.ratkinweapon.morefailure" },
            { ModRaces.RatkinOberoniaAurea, "oark.ratkinfaction.oberoniaaurea" },
            { ModRaces.Moyo, "nemonian.my" },
            { ModRaces.MoyoCartel, "nemonian.mycartel" },
            { ModRaces.MoyoAbyss, "aoba.redmoyo" },
            { ModRaces.Moyo2, "nemonian.my2.beta" },
            { ModRaces.Paniel, "kalospacer.ahndemi.panieltheautomata" },
            { ModRaces.Anty, "roo.antyracemod" },
            { ModRaces.Moosesian, "waffelf.moosesianrace" },
            { ModRaces.FalloutHssn, "hssn.falloutweapons" },
            { ModRaces.FalloutRetro, "legendaryminuteman.frwp"},
            { ModRaces.KurinHar, "seioch.kurin.har" },
            { ModRaces.KurinMeow, "eoralmilk.kurinmeowedition" },
            //{ ModRaces.Yuran, "rooandgloomy.yuranracemod" },
        };
        public static Dictionary<ModRaces, string> ModPackageIdsRange = new Dictionary<ModRaces, string>
        {
            { ModRaces.Aya, "ayameduki" },
            { ModRaces.Grimworld, "grimworld" },
        };
        public static Dictionary<string, List<ThingDef>> GetThingDefsFromMods()
        {
            var defs = DefDatabase<ThingDef>.AllDefs;
            var raceIdSet = new HashSet<string>(ModPackageIds.Values.Select(x => x.ToLower()));
            var remainingDefs = new List<ThingDef>();

            var groupsDict = raceIdSet.ToDictionary(k => k, k => new List<ThingDef>());
            foreach (var def in defs)
            {
                if (raceIdSet.Contains(def?.modContentPack?.PackageId.ToLower()))
                {
                    groupsDict[def.modContentPack.PackageId.ToLower()].Add(def);
                }
                else
                {
                    remainingDefs.Add(def);
                }
            }

            var rangeDict = ModPackageIdsRange.ToDictionary(k => k.Value.ToLower(), k => new List<ThingDef>());
            foreach (var def in remainingDefs)
            {
                foreach (var key in rangeDict.Keys)
                {
                    if (def?.modContentPack?.PackageId.ToLower().StartsWith($"{key}.") ?? false)
                    {
                        rangeDict[key].Add(def);
                        break;
                    }
                }
            }
            return groupsDict.Concat(rangeDict).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
