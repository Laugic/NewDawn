using Terraria;
using NewDawn.NPCs;

namespace NewDawn.Buffs
{
	public class DepthBubbles : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Depth Bubbles");
			// Description.SetDefault("Bubbles!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}
    }
}
