using Terraria;

namespace NewDawn.Buffs
{
	public class UltraJumpBoostBuff : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ultra Jump Boost");
			// Description.SetDefault("'Reach for the stars.'");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.jumpSpeedBoost += 15.0f;
        }
        
	}
}
