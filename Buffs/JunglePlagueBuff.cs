using Terraria;
using NewDawn.NPCs;

namespace NewDawn.Buffs
{
    public class JunglePlagueBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Jungle Plague");
            // Description.SetDefault("Sporified!");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().JunglePlague = true;
        }

    }
}
