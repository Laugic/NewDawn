using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
	public class BurningFragrance : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Burning Fragrance");
			// Description.SetDefault("Increased attack stats");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.12f;
        }
        
	}
}
