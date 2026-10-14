using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class InfernalBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infernal");
            // Description.SetDefault("Burning in your Armor");
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
