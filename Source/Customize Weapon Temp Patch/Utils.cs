using CWF;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using static Customize_Weapon_Temp_Patch.Races;

namespace Customize_Weapon_Temp_Patch
{
    public static class Utils
    {
        public static readonly List<string> WeaponTagsVanilla = new List<string>
        {
            "CWF_AssaultRifle",
            "CWF_BoltActionRifle",
            "CWF_SniperRifle",
            "CWF_Shotgun",
            "CWF_SMG",
            "CWF_LMG",
            "CWF_Minigun",
            "CWF_Handgun",
            "CWF_ChargeLance",
            "CWF_Bow",
            "CWF_BeamRepeater",
            "CWF_BurstFire",
            "CWF_Charge",
        };

        /// <summary>
        /// 检索所有已加载mod中的远程武器定义，并根据预定义的mod包名将它们分类到相应的mod种族中。
        /// </summary>
        /// <remarks>只有当mod包名与预定义的mod包名完全匹配或以其开头时，才会将远程武器定义归类到相应的mod种族中。</remarks>
        /// <returns>一个字典，键为ModRace枚举值，值为对应mod中的远程武器定义列表。</returns>

        public static Dictionary<ModRace, List<ThingDef>> GetThingDefsFromMods()
        {
            var defs = DefDatabase<ThingDef>.AllDefs;
            //Log.Message($"ThingDef count:{defs.Count()}");
            var allModIds = Enum.GetValues(typeof(ModRace))
                .Cast<ModRace>()
                .SelectMany(x => x.GetPackageIds<PackageIdAttribute>() ?? Enumerable.Empty<string>())
                .ToHashSet();
            //Log.Message($"allModIds count:{allModIds.Count}");
            var modRangeIds = Enum.GetValues(typeof(ModRace))
                .Cast<ModRace>()
                .SelectMany(x => x.GetPackageIds<PackageIdRangeAttribute>() ?? Enumerable.Empty<string>())
                .ToHashSet();
            //Log.Message($"modRangeIds count:{modRangeIds.Count}");
            defs = defs.Where(x => x.IsWeapon && !x.IsMeleeWeapon);

            var defs1 = defs.Where(x => allModIds.Contains(x?.modContentPack?.PackageId));
            //Log.Message($"defs1 count:{defs1.Count()}");
            var defs2 = defs.Where(x => modRangeIds.Contains(x?.modContentPack?.PackageId));
            //Log.Message($"defs2 count:{defs2.Count()}");
            var returnDict = GetRaces().ToDictionary(k => k, _ => new List<ThingDef>());
            foreach (var def in defs1)
            {
                foreach (var race in GetRaces())
                {
                    //Log.Message($"Checking {race} for {def.defName}");
                    var ids = race.GetPackageIds<PackageIdAttribute>();
                    if (ids.NullOrEmpty()) continue;
                    //ids.ForEach(id => Log.Message($"  id:{id}"));
                    if (!ids.Contains(def?.modContentPack?.PackageId)) continue;
                    //Log.Message($"Matched");
                    returnDict.TryAdd(race, new List<ThingDef>());
                    //Log.Message($"Adding {def.defName}");
                    returnDict[race].Add(def);
                }
            }
            foreach (var def in defs2)
            {
                foreach (var race in GetRaces())
                {
                    if (race.GetPackageIds<PackageIdRangeAttribute>().NullOrEmpty()) continue;

                    foreach (var id in race.GetPackageIds<PackageIdRangeAttribute>())
                    {
                        if (def?.modContentPack?.PackageId.StartsWith($"{id}.") ?? false)
                        {
                            returnDict.TryAdd(race, new List<ThingDef>());
                            returnDict[race].Add(def);
                        }
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
                var packageIds = ext.GetPackageIds<PackageIdAttribute>();
                if (packageIds == null || !packageIds.Any()) continue;
                if (!ModsConfig.IsActive(packageIds.First())) continue;
                tags.AddRange(ext.GetWeaponTags());
            }
            return tags;
        }

        public static IEnumerable<ModRace> GetRaces() => Enum.GetValues(typeof(ModRace)).Cast<ModRace>();
    }
}
