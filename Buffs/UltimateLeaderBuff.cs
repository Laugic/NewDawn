using NewDawn.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
	public class UltimateLeaderBuff : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Ultimate Leader");
            // Description.SetDefault("This nebula swarm will fight for you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
			if (player.ownedProjectileCounts[ModContent.ProjectileType<UltimateLeader1>()] > 0)
			{
				modPlayer.UltraBoisSummon = true;
			}
			if (!modPlayer.UltraBoisSummon)
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