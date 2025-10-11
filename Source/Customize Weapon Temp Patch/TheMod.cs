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
        private const string defaultCategory = "WeaponsRanged";

        static CwtpStartUp()
        {
            raceDefs = GetThingDefsFromMods();
            settings = CustomizeWeaponTempPatch.settings;
            foreach (var part in Enum.GetValues(typeof(Part)).Cast<Part>())
            {
                parts.supportParts.Add(part);
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

            //ProcessRace(ModRace.Wolfein);
            //ProcessRace(ModRace.Miho);
            //ProcessRace(ModRace.Kiiro);
            //ProcessRace(ModRace.Milira);
            //ProcessRace(ModRace.Cinder);
            //ProcessRace(ModRace.Ratkin);
            //ProcessRace(ModRace.RatkinWeaponPlus);
            //ProcessRace(ModRace.RatkinOberoniaAurea, "OARatkin_WeaponsRanged");
            //ProcessRace(ModRace.Moyo, "Moyo_WeaponsRanged");
            //ProcessRace(ModRace.MoyoCartel, "Moyo_WeaponsRanged");
            //ProcessRace(ModRace.MoyoAbyss, "Moyo_WeaponsRanged");
            //ProcessRace(ModRace.Moyo2, "Moyo2_WeaponsCategory_Ranged");
            //ProcessRace(ModRace.Paniel, "PN_WeaponsRanged");
            //ProcessRace(ModRace.Anty, "AT_Weapons");
            //ProcessRace(ModRace.Moosesian, "MS_WeaponsRanged");
            //ProcessRace(ModRace.FalloutHssn);
            //ProcessRace(ModRace.FalloutRetro);
            //ProcessRace(ModRace.KurinHar);
            //ProcessRace(ModRace.KurinMeow);
            //ProcessRace(ModRace.Rh2Mgs);
            //ProcessRace(ModRace.Rh2Msf);
            //ProcessRace(ModRace.MoeLotl, "Axolotl_LotiQiRangedWeapon");
            //ProcessRace(ModRace.MoeLotl, "Axolotl_RangedWeapon");
            //ProcessRace(ModRace.HaloInfinite);
            //ProcessRace(ModRace.CeleTech, "TOT_Weapons_Cat");
            //ProcessRace(ModRace.CinderEWE);
            //ProcessRace(ModRace.RatkinRwen);
            //ProcessRace(ModRace.RatkinGW);
            //ProcessRace(ModRace.Destiny);
            //ProcessRace(ModRace.Destiny2);
            //ProcessRace(ModRace.Astrologer, "LOF_AS_Category_WeaponRanged");
            //ProcessRace(ModRace.MiliraExpandedXY);
            ////ProcessRace(ModRaces.Yuran, "YRWeapons");

            //ProcessRace(ModRace.Aya);
            //ProcessRace(ModRace.Grimworld);
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
    }
}
