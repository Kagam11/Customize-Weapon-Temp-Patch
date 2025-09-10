using CWF;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;
using static Customize_Weapon_Temp_Patch.Utils;

namespace Customize_Weapon_Temp_Patch
{
    [StaticConstructorOnStartup]
    internal static class CwtpStartUp
    {
        static readonly CwtpSettings settings;
        private static Dictionary<string, List<ThingDef>> raceDefs;
        private static CompProperties_DynamicTraits parts = new CompProperties_DynamicTraits();
        private static List<string> weaponTags = new List<string> { "CWF_AssaultRifle", "CWF_BoltActionRifle", "CWF_BurstFire", "CWF_Charge", "CWF_ChargeLance", "CWF_Handgun", "CWF_LMG", "CWF_Minigun", "CWF_Shotgun", "CWF_SMG", "CWF_SniperRifle" };

        private const string defaultCategory = "WeaponsRanged";

        static CwtpStartUp()
        {
            raceDefs = GetThingDefsFromMods();
            settings = CustomizeWeaponTempPatch.settings;
            foreach (var part in Enum.GetValues(typeof(Part)).Cast<Part>())
            {
                parts.supportParts.Add(part);
            }

            ProcessRace(ModRaces.Wolfein);
            ProcessRace(ModRaces.Miho);
            ProcessRace(ModRaces.Kiiro);
            ProcessRace(ModRaces.Milira);
            ProcessRace(ModRaces.Cinder);
            ProcessRace(ModRaces.Ratkin);
            ProcessRace(ModRaces.RatkinWeaponPlus);
            ProcessRace(ModRaces.RatkinOberoniaAurea, "OARatkin_WeaponsRanged");
            ProcessRace(ModRaces.Moyo, "Moyo_WeaponsRanged");
            ProcessRace(ModRaces.MoyoCartel, "Moyo_WeaponsRanged");
            ProcessRace(ModRaces.MoyoAbyss, "Moyo_WeaponsRanged");
            ProcessRace(ModRaces.Moyo2, "Moyo2_WeaponsCategory_Ranged");
            ProcessRace(ModRaces.Paniel, "PN_WeaponsRanged");
            ProcessRace(ModRaces.Anty, "AT_Weapons");
            ProcessRace(ModRaces.Moosesian, "MS_WeaponsRanged");
            ProcessRace(ModRaces.FalloutHssn);
            ProcessRace(ModRaces.FalloutRetro);
            ProcessRace(ModRaces.KurinHar);
            ProcessRace(ModRaces.KurinMeow);
            //ProcessRace(ModRaces.Yuran, "YRWeapons");

            ProcessRaceRange(ModRaces.Aya);
            ProcessRaceRange(ModRaces.Grimworld);
        }
        private static void ProcessRace(ModRaces race, string category)
        {
            // return if race not enabled in modsettings
            if (!settings.Races[(int)race]) return;
            // return if correspond mod not active
            if (!ModsConfig.IsActive(ModPackageIds[race])) return;

            var defs = raceDefs[ModPackageIds[race]];
            foreach (var def in defs)
            {
                // Check if the ThingDef is a weapon and belongs to the specified category
                if (!(def?.thingCategories?.Any(x => x?.defName == category) ?? false)) continue;

                // Skip if the weapon is of Neolithic tech level
                if (def?.techLevel == RimWorld.TechLevel.Neolithic) continue;

                // Check if the ThingDef already has a CompProperties_DynamicTraits component
                if (def?.comps?.Any(x => x is CompProperties_DynamicTraits) ?? false) continue;

                def?.comps?.Add(parts);
                def?.weaponTags?.AddRange(weaponTags);
            }
            //Log.Message($"[Cwtp] Added for {race}");
        }
        private static void ProcessRace(ModRaces race) => ProcessRace(race, defaultCategory);
        private static void ProcessRaceRange(ModRaces race, string category)
        {
            // return if race not enabled in modsettings
            if (!settings.Races[(int)race]) return;
            // return if correspond mod not active
            if (!ModsConfig.ActiveModsInLoadOrder.Any(x => x.PackageId.StartsWith($"{ModPackageIdsRange[race]}."))) return;

            var defs = raceDefs[ModPackageIdsRange[race]];
            foreach (var def in defs)
            {
                // Check if the ThingDef is a weapon and belongs to the specified category
                if (!(def?.thingCategories?.Any(x => x?.defName == category) ?? false)) continue;
                // Check if the ThingDef already has a CompProperties_DynamicTraits component
                if (def?.comps?.Any(x => x is CompProperties_DynamicTraits) ?? false) continue;

                def?.comps?.Add(parts);
                def?.weaponTags?.AddRange(weaponTags);
            }
        }
        private static void ProcessRaceRange(ModRaces race) => ProcessRaceRange(race, defaultCategory);
    }
}
