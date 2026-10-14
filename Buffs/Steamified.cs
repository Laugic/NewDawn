using NewDawn.NPCs;
using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class Steamified : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Steamified");
            // Description.SetDefault("Steamin hot!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().steamified = true;
            npc.takenDamageMultiplier = npc.GetGlobalNPC<LaugicalGlobalNPCs>().damageMult * 1.15f;
        }
    }
}
