using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Customize_Weapon_Temp_Patch
{
    public static class Utils
    {
        /// <summary>
        /// 兼容的mod
        /// </summary>
        public enum ModRace
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
            Rh2Mgs,
            Rh2Msf,
            MoeLotl,
            HaloInfinite,
            CeleTech,
            CinderEWE,
            //Yuran,
        };

        /// <summary>
        /// 单个mod对应的包名
        /// </summary>
        public static readonly Dictionary<ModRace, List<string>> ModPackageIds = new Dictionary<ModRace, List<string>>
        {
            { ModRace.Wolfein,              new List<string>{ "melondove.wolfeinrace" } } ,
            { ModRace.Miho,                 new List<string>{ "miho.fortifiedoutremer"} },
            { ModRace.Kiiro,                new List<string>{ "ancot.kiirorace" } },
            { ModRace.Milira,               new List<string>{ "ancot.milirarace" }},
            { ModRace.Cinder,               new List<string>{ "breadmo.cinders" } },
            { ModRace.Ratkin,               new List<string>{ "fxz.solaris.ratkinracemod.odyssey" } },
            { ModRace.RatkinWeaponPlus,     new List<string>{ "bbb.ratkinweapon.morefailure" } },
            { ModRace.RatkinOberoniaAurea,  new List<string>{ "oark.ratkinfaction.oberoniaaurea" } },
            { ModRace.Moyo,                 new List<string>{ "nemonian.my" } },
            { ModRace.MoyoCartel,           new List<string>{ "nemonian.mycartel" } },
            { ModRace.MoyoAbyss,            new List<string>{ "aoba.redmoyo" } },
            { ModRace.Moyo2,                new List<string>{ "nemonian.my2.beta" } },
            { ModRace.Paniel,               new List<string>{ "kalospacer.ahndemi.panieltheautomata" } },
            { ModRace.Anty,                 new List<string>{ "roo.antyracemod" } },
            { ModRace.Moosesian,            new List<string>{ "waffelf.moosesianrace" } },
            { ModRace.FalloutHssn,          new List<string>{ "hssn.falloutweapons" } },
            { ModRace.FalloutRetro,         new List<string>{ "legendaryminuteman.frwp" } },
            { ModRace.KurinHar,             new List<string>{ "seioch.kurin.har" } },
            { ModRace.KurinMeow,            new List<string>{ "eoralmilk.kurinmeowedition" } },
            { ModRace.Rh2Mgs,               new List<string>{ "rh2.metal.gear.solid" } },
            { ModRace.Rh2Msf,               new List<string>{ "rh2.faction.militaires.sans.frontieres" } },
            { ModRace.MoeLotl,              new List<string>{ "hentailoliteam.axolotl" } },
            { ModRace.HaloInfinite,         new List<string>{ "rollob312.hinfunsc" } },
            { ModRace.CeleTech,             new List<string>{ "tot.celetech.mkiii" } },
            { ModRace.CinderEWE,            new List<string>{ "asiimoves.cinderserodedweaponsaextended" } },


            // 下方是虽然属于mod系列，但包名不以统一前缀开头的，通过包名全名指定。
            { ModRace.Grimworld,            new List<string>{ "happypurging.ageofdarkness", "cvn.bloodpact" } },
        };

        /// <summary>
        /// 通常系列mod以统一的包名开头
        /// </summary>
        public static readonly Dictionary<ModRace, string> ModPackageIdsRange = new Dictionary<ModRace, string>
        {
            { ModRace.Aya, "ayameduki" },
            { ModRace.Grimworld, "grimworld" },
        };

        public static Dictionary<ModRace, List<ThingDef>> GetThingDefsFromMods()
        {
            var defs = DefDatabase<ThingDef>.AllDefs;
            var allModIds = ModPackageIds.Values.SelectMany(x => x).ToHashSet();
            var modRangeIds = ModPackageIdsRange.Values.ToHashSet();
            defs = defs.Where(x => x.IsWeapon && !x.IsMeleeWeapon);

            var defs1 = defs.Where(x => allModIds.Contains(x?.modContentPack?.PackageId));
            var defs2 = defs.Where(x => modRangeIds.Contains(x?.modContentPack?.PackageId));
            var returnDict = Enum.GetValues(typeof(ModRace)).Cast<ModRace>().ToDictionary(k => k, _ => new List<ThingDef>());
            foreach (var def in defs1)
            {
                foreach (var key in ModPackageIds.Keys)
                {
                    if (ModPackageIds[key].Contains(def?.modContentPack?.PackageId))
                    {
                        returnDict.TryAdd(key, new List<ThingDef>());
                        returnDict[key].Add(def);
                    }
                }
            }
            foreach (var def in defs2)
            {
                foreach (var key in ModPackageIdsRange.Keys)
                {
                    if (def?.modContentPack?.PackageId.StartsWith($"{ModPackageIdsRange[key]}.") ?? false)
                    {
                        returnDict.TryAdd(key, new List<ThingDef>());
                        returnDict[key].Add(def);
                    }
                }
            }
            return returnDict;
        }

        public static Dictionary<TKey, List<TValue>> MergeLists<TKey, TValue>(this IDictionary<TKey, List<TValue>> first, IDictionary<TKey, List<TValue>> second)
        {
            var result = new Dictionary<TKey, List<TValue>>(first);

            foreach (var kvp in second)
            {
                if (result.TryGetValue(kvp.Key, out var existingList))
                {
                    // 合并列表（这里不去重）
                    existingList.AddRange(kvp.Value);
                }
                else
                {
                    // 直接添加新键
                    result[kvp.Key] = new List<TValue>(kvp.Value);
                }
            }

            return result;
        }
    }
}
