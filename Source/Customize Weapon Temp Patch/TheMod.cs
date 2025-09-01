using CWF;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using static Customize_Weapon_Temp_Patch.Utils;

namespace Customize_Weapon_Temp_Patch
{
    public class CustomizeWeaponTempPatch : Mod
    {
        public static CwtpSettings settings;
        public CustomizeWeaponTempPatch(ModContentPack content) : base(content)
        {
            settings = GetSettings<CwtpSettings>();
            
        }
        public override string SettingsCategory() => "CustomizeWeaponTempPatch".Translate();
        public override void DoSettingsWindowContents(Rect inRect)
        {
            var list = new Listing_Standard();
            list.Begin(inRect);
            foreach (var race in Enum.GetValues(typeof(ModRaces)).Cast<ModRaces>())
            {
                list.CheckboxLabeled(race.ToString().Translate(), ref settings.Races[(int)race]);
                list.GapLine(5);
            }
            list.End();
        }
    }

    public class CwtpSettings : ModSettings
    {
        private const bool defaultValue = false;
        public bool[] Races = Enumerable.Repeat(defaultValue, Enum.GetNames(typeof(ModRaces)).Length).ToArray();

        public override void ExposeData()
        {
            foreach (var race in Enum.GetValues(typeof(ModRaces)).Cast<ModRaces>())
            {
                Scribe_Values.Look(ref Races[(int)race], nameof(race), defaultValue);
            }
            base.ExposeData();
        }
    }

    [StaticConstructorOnStartup]
    internal static class CwtpStartUp
    {
        static readonly CwtpSettings settings;
        static CwtpStartUp()
        {
            settings = CustomizeWeaponTempPatch.settings;
            var raceDefs = GetThingDefsFromMods();
            //Log.Message($"raceDefs length {raceDefs.Count}");
            var parts = new CompProperties_DynamicTraits
            {
                supportParts = Enum.GetValues(typeof(Part)).Cast<Part>().ToList()
            };
            var weaponTags = new List<string> { "CWF_AssaultRifle", "CWF_BoltActionRifle", "CWF_BurstFire", "CWF_Charge", "CWF_ChargeLance", "CWF_Handgun", "CWF_LMG", "CWF_Minigun", "CWF_Shotgun", "CWF_SMG", "CWF_SniperRifle" };

            void ProcessRace(ModRaces race, string category)
            {
                if (settings.Races[(int)race] && ModsConfig.IsActive(ModPackageIds[race]))
                {
                    var defs = raceDefs[(int)race];
                    foreach (var def in defs)
                    {
                        if (((def?.thingCategories?.Any(x => x?.defName == category)) ?? false) && !(def?.weaponTags?.Any(x => x == "Neolithic") ?? false))
                        {
                            def?.comps?.Add(parts);
                            def?.weaponTags?.AddRange(weaponTags);
                        }
                    }
                }
                //Log.Message($"[Cwtp] Added for {race}");
            }
            ;

            ProcessRace(ModRaces.Wolfein, "WeaponsRanged");
            ProcessRace(ModRaces.Miho, "WeaponsRanged");
            ProcessRace(ModRaces.Kiiro, "WeaponsRanged");
            ProcessRace(ModRaces.Milira, "WeaponsRanged");
            ProcessRace(ModRaces.Cinder, "WeaponsRanged");
            ProcessRace(ModRaces.Ratkin, "WeaponsRanged");
            ProcessRace(ModRaces.RatkinWeaponPlus, "WeaponsRanged");
            ProcessRace(ModRaces.RatkinOberoniaAurea, "OARatkin_WeaponsRanged");
            ProcessRace(ModRaces.Moyo, "Moyo_WeaponsRanged");
            ProcessRace(ModRaces.MoyoCartel, "Moyo_WeaponsRanged");
            ProcessRace(ModRaces.MoyoAbyss, "Moyo_WeaponsRanged");
            ProcessRace(ModRaces.Moyo2, "Moyo2_WeaponsCategory_Ranged");

        }
    }
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
        };
        public static List<List<ThingDef>> GetThingDefsFromMods()
        {
            var output = new List<List<ThingDef>>(Enum.GetValues(typeof(ModRaces)).Length);
            for (int i = 0; i < Enum.GetValues(typeof(ModRaces)).Length; i++)
            {
                output.Add(new List<ThingDef>());
            }
            //Log.Message($"output length {output.Count}");
            var defs = DefDatabase<ThingDef>.AllDefs;
            //Log.Message($"races count {Enum.GetValues(typeof(ModRaces)).Cast<ModRaces>().Count()}");
            foreach (var def in defs)
            {
                foreach (var race in Enum.GetValues(typeof(ModRaces)).Cast<ModRaces>())
                {
                    if (def?.modContentPack?.PackageId == ModPackageIds[race])
                    {
                        output[(int)race].Add(def);
                    }
                }
            }
            return output;
            //Log.Message($"output length {output.Count}");
        }
    }
}
