using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customize_Weapon_Temp_Patch
{
    public static class Races
    {

        /// <summary>
        /// 兼容的mod
        /// </summary>
        public enum ModRace
        {
            [PackageId("melondove.wolfeinrace")]
            Wolfein,

            [PackageId("miho.fortifiedoutremer")]
            Miho,

            [PackageId("ancot.kiirorace")]
            Kiiro,

            [PackageId("ancot.milirarace")]
            Milira,

            [PackageId("breadmo.cinders")]
            Cinder,

            [PackageId("fxz.solaris.ratkinracemod.odyssey")]
            Ratkin,

            [PackageId("bbb.ratkinweapon.morefailure")]
            RatkinWeaponPlus,

            [PackageId("oark.ratkinfaction.oberoniaaurea")]
            RatkinOberoniaAurea,

            [PackageId("nemonian.my")]
            Moyo,

            [PackageId("nemonian.mycartel")]
            MoyoCartel,

            [PackageId("aoba.redmoyo")]
            MoyoAbyss,

            [PackageId("nemonian.my2.beta")]
            Moyo2,

            [PackageId("kalospacer.ahndemi.panieltheautomata")]
            Paniel,

            [PackageId("roo.antyracemod")]
            Anty,

            [PackageId("waffelf.moosesianrace")]
            Moosesian,

            [PackageId("hssn.falloutweapons")]
            FalloutHssn,

            [PackageId("legendaryminuteman.frwp")]
            FalloutRetro,

            [PackageId("seioch.kurin.har")]
            KurinHar,

            [PackageId("eoralmilk.kurinmeowedition")]
            KurinMeow,

            [PackageId("rh2.metal.gear.solid")]
            Rh2Mgs,

            [PackageId("rh2.faction.militaires.sans.frontieres")]
            Rh2Msf,

            [PackageId("hentailoliteam.axolotl")]
            MoeLotl,

            [PackageId("rollob312.hinfunsc")]
            HaloInfinite,

            [PackageId("tot.celetech.mkiii")]
            CeleTech,

            [PackageId("asiimoves.cinderserodedweaponsaextended")]
            CinderEWE,

            [PackageId("darkkai.ratkinrangedweaponexpandednew")]
            RatkinRwen,

            [PackageId("browncofe.rcg")]
            RatkinGW,

            [PackageId("milkwater.destinymod")]
            Destiny,

            [PackageId("d2.exotic.weapons")]
            Destiny2,

            [PackageId("lof.astrologer")]
            Astrologer,

            [PackageId("xiyueym.miliraexpandedxy")]
            MiliraExpandedXY,

            //-------------------------------------------------------//

            [PackageIdRange("ayameduki")]
            Aya,

            [PackageIdRange("grimworld")]
            [PackageId("happypurging.ageofdarkness")]
            [PackageId("cvn.bloodpact")]
            Grimworld,
            //Yuran,
        };

        /// <summary>
        /// 兼容的mod扩展包
        /// </summary>
        public enum ExtensionMods
        {
            [WeaponTags(
                "CWE_Ammo_Gauss",
                "CWE_Ammo_Laser",
                "CWE_Ammo_Pellets",
                "CWE_Ammo_Plasma",
                "CWE_Ammo_Powder",
                "CWE_BurstFire_Gauss",
                "CWE_BurstFire_Gauss_L",
                "CWE_BurstFire_Gauss_M",
                "CWE_BurstFire_L",
                "CWE_BurstFire_Laser",
                "CWE_BurstFire_M",
                "CWE_BurstFire_Plasma",
                "CWE_Crossbow",
                "CWE_Gauss",
                "CWE_Launcher",
                "CWE_Magazine_L_Gauss",
                "CWE_Magazine_L_Laser",
                "CWE_Magazine_L_Plasma",
                "CWE_Magazine_L_Powder",
                "CWE_Magazine_M_Gauss",
                "CWE_Magazine_M_Laser",
                "CWE_Magazine_M_Plasma",
                "CWE_Magazine_M_Powder",
                "CWE_Magazine_S_Gauss",
                "CWE_Magazine_S_Laser",
                "CWE_Magazine_S_Plasma",
                "CWE_Magazine_S_Powder",
                "CWE_Milira_Energy",
                "CWE_Sights_L",
                "CWE_Sights_M",
                "CWE_Sights_S",
                "CWE_Trait_BioBurner",
                "CWE_Underbarrel_L",
                "CWE_Underbarrel_M",
                "CWE_Underbarrel_S",
                "CWE_Underbarrel_T"
            )]
            [PackageId("feliperathal.customizeweaponexpanded")]
            CwExpanded,
        }
    }
}
