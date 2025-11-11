using System.Linq;
using Verse;

namespace BorderlessCustomize
{
    public static class Utils
    {
        public static void TryAddComp(this ThingDef weaponDef, CompProperties newComp)
        {
            if (!weaponDef.comps.Any(comp => comp.compClass == newComp.compClass))
            {
                weaponDef.comps.Add(newComp);
            }
        }
    }
}
