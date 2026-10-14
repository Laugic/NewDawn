using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
	public class ForHonor : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("For Honor");
			// Description.SetDefault("+15% Damage \nDefense Halved");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            NewDawnPlayer.Get(player).HalfDef = true;
            player.GetDamage(DamageClass.Generic) += 0.15f;
        }
        
    }
}
