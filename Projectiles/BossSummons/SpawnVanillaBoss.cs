using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.BossSummons
{
	public class SpawnVanillaBoss : ModProjectile
	{
        int _id = 1;
		public override void SetDefaults()
		{
            _id = 1;
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 1;
			Projectile.scale = 1f;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 20;
			Projectile.tileCollide = false;
			AIType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Projectile.ai[1]++;
			
			if (Projectile.ai[1] >= 0)
			{
				SoundEngine.PlaySound(SoundID.Roar, new Vector2(player.position.X, (int)player.position.Y-50));
				NPC.SpawnOnPlayer(player.whoAmI, _id);
				Projectile.ai[1] = -30;
			}
		}
	}
}