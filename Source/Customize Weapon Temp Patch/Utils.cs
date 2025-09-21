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
            RatkinRwen,
            RatkinGW,
            Destiny,
            Destiny2,

            Aya,
            Grimworld,
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
            { ModRace.RatkinRwen,           new List<string>{ "darkkai.ratkinrangedweaponexpandednew" } },
            { ModRace.RatkinGW,             new List<string>{ "browncofe.rcg" } },
            { ModRace.Destiny,              new List<string>{ "milkwater.destinymod" } },
            { ModRace.Destiny2,             new List<string>{ "d2.exotic.weapons" } },


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

        public enum ExtensionMods
        {
            CwExpanded,
        }

        public static readonly Dictionary<ExtensionMods, string> ExtensionsPackageIds = new Dictionary<ExtensionMods, string>
        {
            { ExtensionMods.CwExpanded, "feliperathal.customizeweaponexpanded" },
        };

        public static readonly List<string> WeaponTagsVanilla = new List<string>
        {
            "CWF_Bow",
            "CWF_AssaultRifle",
            "CWF_BoltActionRifle",
            "CWF_BurstFire",
            "CWF_Charge",
            "CWF_ChargeLance",
            "CWF_Handgun",
            "CWF_LMG",
            "CWF_Minigun",
            "CWF_Shotgun",
            "CWF_SMG",
            "CWF_SniperRifle"
        };
        public static readonly Dictionary<ExtensionMods, List<string>> WeaponTagsExtensions = new Dictionary<ExtensionMods, List<string>>
        {
            { ExtensionMods.CwExpanded,new List<string>{"CWE_Crossbow", "CWE_Launcher", "CWE_Gauss" } },
        };

        #region 方法
        /// <summary>
        /// 检索所有已加载mod中的远程武器定义，并根据预定义的mod包名将它们分类到相应的mod种族中。
        /// </summary>
        /// <remarks>只有当mod包名与预定义的mod包名完全匹配或以其开头时，才会将远程武器定义归类到相应的mod种族中。</remarks>
        /// <returns>一个字典，键为ModRace枚举值，值为对应mod中的远程武器定义列表。</returns>

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

        /// <summary>
        /// 将两个包含值列表的字典合并，通过组合匹配键的列表来实现合并。
        /// </summary>
        /// <remarks>返回的字典在合并匹配键的列表时不会删除重复值。原始输入字典不会被修改。</remarks>
        /// <typeparam name="TKey">字典中键的类型。</typeparam>
        /// <typeparam name="TValue">字典中与每个键关联的列表中值的类型。</typeparam>
        /// <param name="first">合并的第一个字典。不能为空。</param>
        /// <param name="second">第二个进行合并的字典。不能为空。</param>
        /// <returns>一个新的字典，包含两个输入字典中的所有键。对于两个字典中都存在的键，值列表按它们在输入字典中出现的顺序连接。</returns>
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

        /// <summary>
        /// 检索并返回所有可用的武器标签列表，包括来自已启用扩展mod的标签。
        /// </summary>
        /// <remarks>只包含定制武器和启用了的扩展mod的标签。</remarks>
        /// <returns>包含来自基础游戏和任何启用的扩展mod的武器标签的字符串列表。如果没有可用的标签，则列表为空。
        public static List<string> GetAllWeaponTags()
        {
            var tags = new List<string>(WeaponTagsVanilla);
            foreach (var ext in Enum.GetValues(typeof(ExtensionMods)).Cast<ExtensionMods>())
            {
                if (!CustomizeWeaponTempPatch.settings.Extensions[(int)ext]) continue;
                if (!ModsConfig.IsActive(ExtensionsPackageIds[ext])) continue;
                tags.AddRange(WeaponTagsExtensions[ext]);
            }
            return tags;
        }
        #endregion
    }
}
