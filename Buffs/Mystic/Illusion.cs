using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs.Mystic
{
    public class Illusion : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Illusion");
            // Description.SetDefault("The power of the mind is yours");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }
    }
}
