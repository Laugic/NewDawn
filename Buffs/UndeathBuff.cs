using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class UndeathBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Undeath");
            // Description.SetDefault("Not quite immortality");
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
