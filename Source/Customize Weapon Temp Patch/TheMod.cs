using CWF;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using static Customize_Weapon_Temp_Patch.Races;
using static Customize_Weapon_Temp_Patch.Utils;

namespace Customize_Weapon_Temp_Patch
{
    [StaticConstructorOnStartup]
    internal static class CwtpStartUp
    {
        static readonly CwtpSettings settings;
        private static readonly Dictionary<ModRace, List<ThingDef>> raceDefs;
        private static readonly CompProperties_DynamicTraits parts = new CompProperties_DynamicTraits();
        private static readonly List<PartDef> partDefs = new List<PartDef>();
        private const string defaultCategory = "WeaponsRanged";

        static CwtpStartUp()
        {
            raceDefs = GetThingDefsFromMods();
            settings = CustomizeWeaponTempPatch.settings;
            partDefs.AddRange(DefDatabase<PartDef>.AllDefs.ToList());
            //Log.Message($"Total parts count: {partDefs.Count}");
            //foreach (var part in Enum.GetValues(typeof(Part)).Cast<Part>())
            //{
            //    parts.supportParts.Add(part);
            //}

            if (settings.ForceMode)
            {
                var tags = GetAllWeaponTags();
                var allGuns = DefDatabase<ThingDef>.AllDefs;
                allGuns = allGuns.Where(x => x.IsRangedWeapon && (x.Verbs?.Any(v => v.verbClass == typeof(Verb_Shoot)) ?? false));
                foreach (var item in allGuns)
                {
                    //Log.Message($"Processing weapon {item.label}");
                    if (item.comps?.FirstOrDefault(x => x is CompProperties_DynamicTraits) is CompProperties_DynamicTraits dynamicTraits)
                    {
                        //Log.Message($"exists");
                        if (settings.OverwriteMode)
                        {
                            if (dynamicTraits.supportParts.Count > 0)
                            {
                                //Log.Message($"Clearing existing parts for weapon {item.label}");
                                dynamicTraits.supportParts.Clear();
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        //Log.Message($"not exist");
                        item.comps.Add(new CompProperties_DynamicTraits());
                    }

                    dynamicTraits = item.comps?.FirstOrDefault(x => x is CompProperties_DynamicTraits) as CompProperties_DynamicTraits;
                    var supportParts = dynamicTraits.supportParts;
                    //Log.Message($"Adding parts to weapon {item.label}");
                    item.weaponTags?.AddRange(tags);
                    //Log.Message($"Adding parts to weapon {item.label} - before count: {supportParts.Count}");
                    (item.comps?.FirstOrDefault(x => x is CompProperties_DynamicTraits) as CompProperties_DynamicTraits)?.supportParts?.AddRange(partDefs);
                    //Log.Message($"Adding parts to weapon {item.label} - after count: {supportParts.Count}");
                }
                return;
            }

            Enum.GetValues(typeof(ModRace))
                .Cast<ModRace>()
                .ToList()
                .ForEach(r =>
                {
                    var cate = r.GetItemCategories();
                    if (cate.NullOrEmpty())
                        ProcessRace(r);
                    else
                        cate.ForEach(c => ProcessRace(r, c));
                });
        }
        private static void ProcessRace(ModRace race, string category)
        {
            // 设置没开就不处理
            if (!settings.Races[(int)race]) return;
            // mod没启用就不处理
            // 应该是没有必要的，因为没启用的话raceDefs里也不会有对应的东西
            //if (!ModsConfig.IsActive(ModPackageIds[race])) return;

            var defs = raceDefs[race];
            AddParts(defs, category);
        }
        private static void ProcessRace(ModRace race) => ProcessRace(race, defaultCategory);

        private static void AddParts(List<ThingDef> defs, string category)
        {
            var tags = GetAllWeaponTags();
            foreach (var def in defs)
            {
                // 只处理对应的武器类
                if (!(def?.thingCategories?.Any(x => x?.defName == category) ?? false)) continue;
                // 检测是否已有组件
                if (settings.OverwriteMode)
                {
                    def?.comps?.RemoveAll(x => x is CompProperties_DynamicTraits);
                    def?.weaponTags?.RemoveAll(x => tags.Contains(x));
                }
                else if (def?.comps?.Any(x => x is CompProperties_DynamicTraits) ?? false) continue;

                def?.comps?.Add(parts);
                def?.weaponTags?.AddRange(tags);
            }
        }
        [Obsolete]
        private static void AddPartsNotSure(List<ThingDef> defs, string category)
        {
            var tags = GetAllWeaponTags();
            foreach (var def in defs)
            {
                // 只处理对应的武器类
                if (!(def?.thingCategories?.Any(x => x?.defName == category) ?? false)) continue;
                // 检测是否已有组件
                if (settings.OverwriteMode)
                {
                    def?.comps?.RemoveAll(x => x is CompProperties_DynamicTraits);
                    def?.weaponTags?.RemoveAll(x => tags.Contains(x));
                }
                else if (def?.comps?.Any(x => x is CompProperties_DynamicTraits) ?? false) continue;
                var graphicData = new GraphicData()
                {
                    texPath = def.graphicData.texPath,
                    graphicClass = def.graphicData.graphicClass,
                };
                //var weaponTags = tags;
                var adapter = new AdapterDef()
                {
                    defName = def.defName,
                    //graphicData = graphicData,
                    //weaponTags = weaponTags,
                };
                var privateGraphicData = typeof(AdapterDef).GetField("graphicData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                privateGraphicData.SetValue(adapter, graphicData);
                var privateWeaponTags = typeof(AdapterDef).GetField("weaponTags", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                privateWeaponTags.SetValue(adapter, tags);

                def?.comps?.Add(parts);
                def?.weaponTags?.AddRange(tags);

                DefDatabase<Def>.Add(adapter);

            }
        }
    }
}
