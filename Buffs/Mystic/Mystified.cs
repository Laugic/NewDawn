using NewDawn.NPCs;
using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs.Mystic
{
    public class Mystified : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mystified");
            // Description.SetDefault("Disintegrating!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).Mystified = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().mFied = true;
        }
    }
}
