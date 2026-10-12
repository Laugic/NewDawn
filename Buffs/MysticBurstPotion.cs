using Terraria;

namespace NewDawn.Buffs
{
	public class MysticBurstPotion : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Burst Power");
			// Description.SetDefault("'Mystical energy swells within you.'\nDecreases Mystic Burst cooldown");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            NewDawnPlayer.Get(player).MysticSwitchCoolRate += 1;
        }
        
	}
}
