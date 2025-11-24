using CWF;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace BorderlessCustomize
{
    public class BorderlessCustomize : Mod
    {
        public static BcSettings settings;
        private Vector2 scrollPos = Vector2.zero;
        private float viewHeight = 1500f;
        public BorderlessCustomize(ModContentPack content) : base(content)
        {
            settings = GetSettings<BcSettings>();
        }
        public override string SettingsCategory() => "BorderlessCustomize".Translate();
        public override void DoSettingsWindowContents(Rect inRect)
        {
            //var originFont = Text.Font;
            //var outRect = inRect;
            //// 用上次内容高度，初始值给个较大数
            //var viewRect = new Rect(0f, 0f, outRect.width - 16f, viewHeight);

            //Widgets.BeginScrollView(outRect, ref scrollPos, viewRect);
            var list = new Listing_Standard();
            list.Begin(inRect);

            list.CheckboxLabeled("OverwriteModeLabel".Translate(), ref settings.OverwriteMode, tooltip: "OverwriteModeDesc".Translate());
            list.GapLine();
            list.CheckboxLabeled("AddColorLabel".Translate(), ref settings.IsAddColor, tooltip: "AddColorDesc".Translate());
            list.CheckboxLabeled("AddRenameLabel".Translate(), ref settings.IsAddRename, tooltip: "AddRenameDesc".Translate());
            list.GapLine();
            if (Current.Game is { } game && list.ButtonText("RemoveColorComps".Translate()))
            {
                var things = game.CurrentMap.listerThings.GetAllThings();
                foreach (var thing in things)
                {
                    if (thing is ThingWithComps twc)
                    {
                        twc.AllComps.RemoveWhere(x => x is CWF.CompColorable);
                    }
                }
            }
            list.End();

            //// 只在渲染后更新 viewHeight
            //viewHeight = list.CurHeight;
            //viewRect.height = viewHeight;

            //Widgets.EndScrollView();
        }
    }

    public class BcSettings : ModSettings
    {
        private const bool defaultValue = false;
        public bool OverwriteMode = defaultValue;
        public bool IsAddColor = true;
        public bool IsAddRename = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref OverwriteMode, nameof(OverwriteMode), defaultValue);
            Scribe_Values.Look(ref IsAddColor, nameof(IsAddColor), true);
            Scribe_Values.Look(ref IsAddRename, nameof(IsAddRename), true);
            base.ExposeData();
        }
    }
}
