using Terraria;

namespace NewDawn.Buffs
{
	public class MysticPower : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mystic Power");
			// Description.SetDefault("'The energy of the cosmos strengthens you.'\n+10% Mystic Damage");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().MysticDamage += 0.1f;
        }
        
	}
}
