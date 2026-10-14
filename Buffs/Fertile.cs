using NewDawn.NPCs;
using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class Fertile : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fertile");
            // Description.SetDefault("Seeds grow within you.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }


        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().Fertile = true;
        }
    }
}
