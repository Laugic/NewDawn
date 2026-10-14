using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
	public class Rubrum : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rubrum");
			// Description.SetDefault("+10% Damage");
			Main.buffNoSave[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;
        }
	}
}
