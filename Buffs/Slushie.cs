using NewDawn.NPCs;
using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class Slushie : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slushie");
            // Description.SetDefault("Squish");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().Slushie = true;
        }
    }
}
