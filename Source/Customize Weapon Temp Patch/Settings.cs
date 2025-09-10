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
        private float viewHeight;
        public CustomizeWeaponTempPatch(ModContentPack content) : base(content)
        {
            settings = GetSettings<CwtpSettings>();
        }
        public override string SettingsCategory() => "CustomizeWeaponTempPatch".Translate();
        public override void DoSettingsWindowContents(Rect inRect)
        {
            var outRect = inRect;
            // 用上次内容高度，初始值给个较大数
            var viewRect = new Rect(0f, 0f, outRect.width - 16f, viewHeight > 0 ? viewHeight : 1000f);

            Widgets.BeginScrollView(outRect, ref scrollPos, viewRect);

            var list = new Listing_Standard();
            list.Begin(viewRect);
            foreach (var race in Enum.GetValues(typeof(ModRaces)).Cast<ModRaces>())
            {
                list.CheckboxLabeled(race.ToString().Translate(), ref settings.Races[(int)race]);
                list.GapLine(5);
            }
            list.End();

            // 只在渲染后更新 viewHeight
            viewHeight = list.CurHeight;

            Widgets.EndScrollView();
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
}
