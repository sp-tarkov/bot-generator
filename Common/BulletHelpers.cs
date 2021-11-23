using System.Linq;

namespace Common;

public static class BulletHelpers
{
    private static readonly string[] blackList = {
        "56dff3afd2720bba668b4567", // 5.45x39 ps
        "5c0d56a986f774449d5de529", // 9x19mm rip
        "58864a4f2459770fcc257101", // 9x19mm pso
        "5efb0e16aeb21837e749c7ff", // 9x19mm quakemaker
        "5c3df7d588a4501f290594e5", // 9x19mm T
        "5737218f245977612125ba51", // 9x18mm sp8
        "57371aab2459775a77142f22", // 9x18mm pmm pstm
        "5a269f97c4a282000b151807", // 9x21mm sp10
        "5a26abfac4a28232980eabff", // 9x21mm sp11
        "5a26ac06c4a282000c5a90a8", // 9x21mm sp12
        "59e6918f86f7746c9f75e849", // 5.56x45mm mk 255 mod 0
        "5735ff5c245977640e39ba7e", // 7.62x25mm fmj
        "573601b42459776410737435", // 7.62x25mm lrn
        "59e6658b86f77411d949b250", // .366tkm
        "5c0d591486f7744c505b416f", // 12/70 rip
        "5d6e6869a4b9361c140bcfde", // 12/70 grizzly
        "5e85a9f4add9fe03027d9bf1", // 23x75mm flashbang round
        "5cadf6e5ae921500113bb973", // 12.7x55 mm PS12A
        "5cadf6ddae9215051e1c23b2", // 12.7x55 mm PS12
        "5ea2a8e200685063ec28c05a", // .45 acp rip
        "5fbe3ffdf8b6a877a729ea82", // .300 aac bcp fmj
        "5e023e6e34d52a55c3304f71", // 7.62x51mm tcw sp
        "5e023e88277cce2b522ff2b1"  // 7.62x51 ultra nosler
    };

    public static bool BulletIsOnBlackList(string bullet)
    {
        return blackList.Any(x => x.Contains(bullet));
    }
}