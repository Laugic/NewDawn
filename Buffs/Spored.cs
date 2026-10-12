using NewDawn.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
    public class Spored : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spored");
            // Description.SetDefault("Smokin hot!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NewDawnGlobalNPC>().Spored = true;
        }
    }
}
