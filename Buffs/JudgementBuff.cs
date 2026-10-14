using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class JudgementBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Judgement");
            // Description.SetDefault("Bring down weight upon you");
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
