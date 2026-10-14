using NewDawn.NPCs;
using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class PollinatedBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pollinated");
            // Description.SetDefault("I'm about to sneeze");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().Pollinated = true;
        }
    }
}
