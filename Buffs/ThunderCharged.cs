using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class ThunderCharged : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Thunder Charged");
            // Description.SetDefault("Full of lightning");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {

        }
    }
}
