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
            [ItemCategory("OARatkin_WeaponsRanged")]
            RatkinOberoniaAurea,

            [PackageId("nemonian.my")]
            [ItemCategory("Moyo_WeaponsRanged")]
            Moyo,

            [PackageId("nemonian.mycartel")]
            [ItemCategory("Moyo_WeaponsRanged")]
            MoyoCartel,

            [PackageId("aoba.redmoyo")]
            [ItemCategory("Moyo_WeaponsRanged")]
            MoyoAbyss,

            [PackageId("nemonian.my2.beta")]
            [ItemCategory("Moyo2_WeaponsCategory_Ranged")]
            Moyo2,

            [PackageId("kalospacer.ahndemi.panieltheautomata")]
            [ItemCategory("PN_WeaponsRanged")]
            Paniel,

            [PackageId("roo.antyracemod")]
            [ItemCategory("AT_Weapons")]
            Anty,

            [PackageId("waffelf.moosesianrace")]
            [ItemCategory("MS_WeaponsRanged")]
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
            [ItemCategory("Axolotl_LotiQiRangedWeapon")]
            [ItemCategory("Axolotl_RangedWeapon")]
            MoeLotl,

            [PackageId("rollob312.hinfunsc")]
            HaloInfinite,

            [PackageId("tot.celetech.mkiii")]
            [ItemCategory("TOT_Weapons_Cat")]
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
            [ItemCategory("LOF_AS_Category_WeaponRanged")]
            Astrologer,

            [PackageId("xiyueym.miliraexpandedxy")]
            MiliraExpandedXY,

            [PackageId("src.core.markeazzyh")]
            [ItemCategory("SR_Weapons")]
            StarRing,

            [PackageId("zawodroshen.pandora.frontier.remake")]
            PandoraFR,

            [PackageId("vanya.polarisbloc.securityforce.tmp")]
            PolarisblocSF,

            [PackageId("dual.weap")]
            DualWeapon,

            [PackageIdRange("aoba.deadmanswitch")]
            DeadManSwitch,

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
                "CWE_Magazine_L_Plasma",
                "CWE_Magazine_L_Powder",
                "CWE_Magazine_M_Plasma",
                "CWE_Magazine_M_Powder",
                "CWE_Magazine_S_Gauss",
                "CWE_Magazine_S_Laser",
                "CWE_Magazine_S_Plasma",
                "CWE_Magazine_S_Powder",
                "CWE_Sights_L",
                "CWE_Sights_M",
                "CWE_Sights_S",
                "CWE_Underbarrel_L",
                "CWE_Underbarrel_M",
                "CWE_Underbarrel_S"
            )]
            [PackageId("feliperathal.customizeweaponexpanded")]
            CwExpanded,
        }
    }
}
