using Terraria;

namespace NewDawn.Buffs
{
	public class OmegaJumpBoost : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Omega Jump Boost");
			// Description.SetDefault("'Reach for the next dimension.'");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.jumpSpeedBoost += 50.0f;
        }
        
	}
}
