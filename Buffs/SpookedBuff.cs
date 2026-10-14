using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class SpookedBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spooked");
            // Description.SetDefault("Many spoops!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
    }
}
