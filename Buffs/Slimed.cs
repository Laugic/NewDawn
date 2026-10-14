using Terraria;
using NewDawn.NPCs;

namespace NewDawn.Buffs
{
	public class Slimed : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Slimed");
			// Description.SetDefault("Slowly losing life");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().slimed = true;
        }

    }
}
