using Terraria;
using NewDawn.NPCs;

namespace NewDawn.Buffs
{
	public class EerinessBuff : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eeriness");
			// Description.SetDefault("Bubbles!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
			npc.GetGlobalNPC<LaugicalGlobalNPCs>().HitDelay++;
        }

    }
}
