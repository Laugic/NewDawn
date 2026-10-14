using NewDawn.NPCs;
using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs.Mystic
{
    public class HermesBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hermes");
            // Description.SetDefault("Aerial predator");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }


        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().AerialWeakness = true;
        }
    }
}
