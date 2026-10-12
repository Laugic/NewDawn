using Terraria;
using NewDawn.NPCs;

namespace NewDawn.Buffs
{
	public class Bubbly : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bubbly");
			// Description.SetDefault("Bubbles!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LaugicalGlobalNPCs>().bubbly = true;
        }

    }
}
