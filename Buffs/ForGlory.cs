using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
	public class ForGlory : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("For Glory");
			// Description.SetDefault("+15% Damage \nNo life Regen");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            player.bleed = true;
            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.lifeRegen = 0;
        }
        
	}
}
