using Terraria;

namespace NewDawn.Buffs
{
	public class DestructionBoost : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Destruction Power");
			// Description.SetDefault("'Hell's energy strengthens you.'\n+10% Mystic Damage");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			NewDawnPlayer.Get(player).MysticDamage += 0.1f;
        }
        
	}
}
