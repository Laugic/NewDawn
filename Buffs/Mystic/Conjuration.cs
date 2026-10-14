using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs.Mystic
{
    public class Conjuration : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Conjuration");
            // Description.SetDefault("Draw energy from other worlds");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
    }
}
