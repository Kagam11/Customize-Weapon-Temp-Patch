using System.Collections.Generic;
using System.Linq;

namespace BorderlessCustomize
{
    public static class Mods
    {
        public static List<string> VanillaWeaponTags = new()
        {
            "CWF_AssaultRifle",
            "CWF_BoltActionRifle",
            "CWF_SniperRifle",
            "CWF_Shotgun",
            "CWF_SMG",
            "CWF_LMG",
            "CWF_Minigun",
            "CWF_Handgun",
            "CWF_ChargeLance",
            "CWF_Bow",
            "CWF_BeamRepeater",
            "CWF_BurstFire",
            "CWF_Charge",
        };
        public static List<string> CwfWeaponTags = new()
        {
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
            "CWE_ModuleAmmo",
            "CWE_ModuleBarrel",
            "CWE_ModuleGrip",
            "CWE_ModuleMagazine",
            "CWE_ModuleReceiver",
            "CWE_ModuleSight",
            "CWE_ModuleStock",
            "CWE_ModuleTrigger",
            "CWE_ModuleUnderbarrel",
            "CWE_Sights_L",
            "CWE_Sights_M",
            "CWE_Sights_S",
            "CWE_Underbarrel_L",
            "CWE_Underbarrel_M",
            "CWE_Underbarrel_S",
        };
        public static List<string> AllWeaponTags => VanillaWeaponTags.Union(CwfWeaponTags).ToList();
    }
}
