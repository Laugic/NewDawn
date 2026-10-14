using Terraria;

namespace NewDawn.Buffs
{
	public class JumpBoost : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Jump Boost");
			// Description.SetDefault("'Reach for the sky.'");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.jumpSpeedBoost += 5.0f;
        }
        
	}
}
