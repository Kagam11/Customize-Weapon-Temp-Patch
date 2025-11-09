packageIds = """vortex.customizeweapon
melondove.wolfeinrace
miho.fortifiedoutremer
ancot.kiirorace
ancot.milirarace
breadmo.cinders
fxz.solaris.ratkinracemod.odyssey
bbb.ratkinweapon.morefailure
oark.ratkinfaction.oberoniaaurea
nemonian.my
nemonian.mycartel
aoba.redmoyo
nemonian.my2.beta
kalospacer.ahndemi.panieltheautomata
roo.antyracemod
waffelf.moosesianrace
hssn.falloutweapons
legendaryminuteman.frwp
seioch.kurin.har
eoralmilk.kurinmeowedition
rh2.faction.militaires.sans.frontieres
rh2.metal.gear.solid
hentailoliteam.axolotl
rollob312.hinfunsc
tot.celetech.mkiii
asiimoves.cinderserodedweaponsaextended
darkkai.ratkinrangedweaponexpandednew
browncofe.rcg
milkwater.destinymod
d2.exotic.weapons
lof.astrologer
xiyueym.miliraexpandedxy
lof.astrologer
xiyueym.miliraexpandedxy
src.core.markeazzyh
zawodroshen.pandora.frontier.remake
vanya.polarisbloc.securityforce.tmp
dual.weap
aoba.deadmanswitch.core
runnelatki.rabbieracemod
lsk.rabbieweaponsandequipment
ariandel.miliraimperium
 
ayameduki.ayaracepremise
akarthus.thecosmicodyssey.core
feliperathal.customizeweaponexpanded"""

packageIds = packageIds.split("\n")
results = []
path = r"E:\Games\Steam\steamapps\workshop\content\294100"
# iterate through each folder in the path
import os
import regex as re
from lxml import etree

def lower_inside_angle_brackets(text: str) -> str:
    # 使用正则匹配 <...>，并在替换时调用 lambda 转小写
    return re.sub(r"<(.*?)>", lambda m: f"<{m.group(1).lower()}>", text)

for folder in os.listdir(path):
    steamid = folder.split("\\")[-1]
    # print(steamid)  
    aboutXmlPath = os.path.join(path, folder, "About", "About.xml")
    print(aboutXmlPath)
    if os.path.exists(aboutXmlPath):
        with open(aboutXmlPath, "r", encoding="utf-8") as f:
            aboutXml = f.read()
            # remove the description element first (case-insensitive, dot matches newline)
            aboutXml = re.sub(r"(?is)<description>.*?</description>", "", aboutXml)
            aboutXml = lower_inside_angle_brackets(aboutXml)
            # print(aboutXml)
        tree = etree.fromstring(aboutXml.encode("utf-8"))

        name = tree.xpath("/modmetadata/name")[0].text
        packageId = tree.xpath("/modmetadata/packageid")[0].text
        if packageId.lower() in packageIds:
            print(f"Found: {name} ({packageId})")
            # packageIds.remove(packageId)
            results.append(
                (f"[url=https://steamcommunity.com/sharedfiles/filedetails/?id={steamid}]{name}[/url]",name)
            )
results.sort(key=lambda x: x[1].lower())
for r in results:
    print(r[0])
