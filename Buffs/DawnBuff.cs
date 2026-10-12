using Terraria;
using NewDawn.NPCs;

namespace NewDawn.Buffs
{
	public class DawnBuff : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dawn");
			// Description.SetDefault("Golden energy");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().dawn = true;
        }

    }
}
