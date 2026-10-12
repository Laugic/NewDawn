using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs.Mystic
{
    public class Destruction : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Destruction");
            // Description.SetDefault("Hell is your catalyst");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
    }
}
