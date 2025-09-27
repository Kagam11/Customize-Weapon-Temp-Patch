using CWF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static Customize_Weapon_Temp_Patch.Races;

namespace Customize_Weapon_Temp_Patch
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class PackageIdAttributeBase : Attribute
    {
        public string Value { get; }
        public PackageIdAttributeBase(string packageId) => Value = packageId;
    }

    public class PackageIdAttribute : PackageIdAttributeBase
    {
        public PackageIdAttribute(string packageId) : base(packageId)
        {
        }
    }

    public class PackageIdRangeAttribute : PackageIdAttributeBase
    {
        public PackageIdRangeAttribute(string packageId) : base(packageId)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class WeaponTagsAttribute : Attribute
    {
        public string[] Value { get; }
        public WeaponTagsAttribute(params string[] weaponTags) => Value = weaponTags;
    }

    public static class EnumExtensions
    {
        //private static readonly Dictionary<Enum, List<string>> idRangeCache = new Dictionary<Enum, List<string>>();
        //private static readonly Dictionary<Enum, List<string>> idCache = new Dictionary<Enum, List<string>>();
        //private static readonly Dictionary<Type, Dictionary<Enum, List<string>>> raceMap = new Dictionary<Type, Dictionary<Enum, List<string>>>
        //{
        //    { typeof(PackageIdAttribute),idCache },
        //    { typeof(PackageIdRangeAttribute), idRangeCache },
        //};

        public static List<string> GetPackageIds<TAttr>(this Enum race) where TAttr : PackageIdAttributeBase
        {
            //if (raceMap.TryGetValue(typeof(TAttr), out var cache))
            //{
            //    if (cache.TryGetValue(race, out var cached))
            //        return cached;
            //}

            var field = race.GetType().GetField(race.ToString());
            var attrs = field.GetCustomAttributes(typeof(TAttr), false).Cast<TAttr>().ToList();

            var values = attrs?.Select(a => a.Value).ToList() ?? new List<string>();

            //raceMap[typeof(TAttr)][race] = values;
            return values;
        }

        public static List<string> GetWeaponTags(this ExtensionMods mod)
        {
            var field = typeof(ExtensionMods).GetField(mod.ToString());
            var attr = Attribute.GetCustomAttribute(field, typeof(WeaponTagsAttribute)) as WeaponTagsAttribute;
            return attr?.Value?.ToList() ?? new List<string>();
        }
    }
}
