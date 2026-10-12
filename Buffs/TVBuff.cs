using NewDawn.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
	public class TVBuff : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("TV");
			// Description.SetDefault("Don't watch- it's just fake news.");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);

			if (player.ownedProjectileCounts[ModContent.ProjectileType<TV>()] > 0)
				modPlayer.TVSummon = true;

            if (!modPlayer.TVSummon)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
				player.buffTime[buffIndex] = 18000;
		}
	}
}