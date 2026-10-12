using Terraria;

namespace NewDawn.Buffs
{
	public class MagmaticVeins : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magmatic Veins");
			// Description.SetDefault("Power flows through you!");
			Main.debuff[Type] = false;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.GetModPlayer<NewDawnPlayer>().DamageBoost(.15f);
            player.statDefense += 10;

            player.moveSpeed += 3f;
            player.maxRunSpeed += 3f;
            player.jumpSpeedBoost += 3f;
        }
	}
}
