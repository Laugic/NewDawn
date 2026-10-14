using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class OrbitalBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Orbital");
            // Description.SetDefault("Into orbit!");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
    }
}
