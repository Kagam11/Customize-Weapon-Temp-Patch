using RimWorld;
using System;
using System.Linq;
using UnityEngine;
using Verse;
using static Customize_Weapon_Temp_Patch.Utils;

namespace Customize_Weapon_Temp_Patch
{
    public class CustomizeWeaponTempPatch : Mod
    {
        public static CwtpSettings settings;
        private Vector2 scrollPos = Vector2.zero;
        private float viewHeight = 1500f;
        public CustomizeWeaponTempPatch(ModContentPack content) : base(content)
        {
            settings = GetSettings<CwtpSettings>();
        }
        public override string SettingsCategory() => "CustomizeWeaponTempPatch".Translate();
        public override void DoSettingsWindowContents(Rect inRect)
        {
            var originFont = Text.Font;
            var outRect = inRect;
            // 用上次内容高度，初始值给个较大数
            var viewRect = new Rect(0f, 0f, outRect.width - 16f, viewHeight);

            Widgets.BeginScrollView(outRect, ref scrollPos, viewRect);
            var list = new Listing_Standard();
            list.Begin(viewRect);

            Text.Font = GameFont.Medium;
            list.Label("CwtpSettingsDesc".Translate());
            Text.Font = originFont;
            list.CheckboxLabeled("CwtpOverwriteModeLabel".Translate(), ref settings.OverwriteMode);
            list.Gap(15);
            Text.Font = GameFont.Medium;
            list.Label("CwtpExtensionSettingsLabel".Translate());
            Text.Font = originFont;
            foreach (var ext in Enum.GetValues(typeof(ExtensionMods)).Cast<ExtensionMods>())
            {
                list.CheckboxLabeled($"Cwtp{ext}".Translate(), ref settings.Extensions[(int)ext]);
                list.GapLine(5);
            }
            list.Gap(15);
            var races = Enum.GetValues(typeof(ModRace)).Cast<ModRace>().ToList();
            races.SortBy(r => r.ToString().Translate().ToString());
            Text.Font = GameFont.Medium;
            list.Label("CwtpRaceSettingsLabel".Translate());
            Text.Font = originFont;
            //list.Label($"races count:{races.Count}");
            foreach (var race in races)
            {
                //list.Label(race.ToString());
                list.CheckboxLabeled($"Cwtp{race}".Translate(), ref settings.Races[(int)race]);
                list.GapLine(5);
            }
            list.End();

            // 只在渲染后更新 viewHeight
            viewHeight = list.CurHeight;
            viewRect.height = viewHeight;

            Widgets.EndScrollView();
        }
    }

    public class CwtpSettings : ModSettings
    {
        private const bool defaultValue = false;
        public bool[] Races = Enumerable.Repeat(defaultValue, Enum.GetNames(typeof(ModRace)).Length).ToArray();
        public bool[] Extensions = Enumerable.Repeat(defaultValue, Enum.GetNames(typeof(ExtensionMods)).Length).ToArray();
        public bool OverwriteMode = defaultValue;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref OverwriteMode, nameof(OverwriteMode), defaultValue);
            foreach (var ext in Enum.GetValues(typeof(ExtensionMods)).Cast<ExtensionMods>())
            {
                Scribe_Values.Look(ref Extensions[(int)ext], ext.ToString(), defaultValue);
            }
            foreach (var race in Enum.GetValues(typeof(ModRace)).Cast<ModRace>())
            {
                Scribe_Values.Look(ref Races[(int)race], race.ToString(), defaultValue);
            }
            base.ExposeData();
        }
    }
}
