using NewDawn.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
	public class SandySharkBuff : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Sandy Shark");
            // Description.SetDefault("It looks a bit pouty");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
			if (player.ownedProjectileCounts[ModContent.ProjectileType<SandySharkProjectile>()] > 0)
			{
				modPlayer.SandSharkSummon = true;
			}
			if (!modPlayer.SandSharkSummon)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}