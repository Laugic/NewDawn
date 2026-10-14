using Terraria;
using NewDawn.NPCs;

namespace NewDawn.Buffs
{
	public class TrueDawn : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("TrueDawn");
			// Description.SetDefault("Star energy");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().trueDawn = true;
        }

    }
}
