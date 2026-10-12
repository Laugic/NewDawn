using NewDawn.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
	public class MagmaticCoreBuff : NewDawnBuff
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Magmatic Core");
            // Description.SetDefault("This ball of hardened magma will fight for you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
			if (player.ownedProjectileCounts[ModContent.ProjectileType<MagmaticCoreProjectile>()] > 0)
			{
				modPlayer.MoltenCoreSummon = true;
			}
			if (!modPlayer.MoltenCoreSummon)
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