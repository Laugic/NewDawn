using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
	public class ZuesBoltIllusion1 : IllusionProjectile
    {
		public int timer = 0;
        bool justSpawned = false;
        public override void SetDefaults()
        {
            justSpawned = false;
            Projectile.width = 14;
            Projectile.height = 14;
			Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
			Projectile.friendly = true;
            Projectile.ignoreWater = true;
			Projectile.extraUpdates = 10;
            //buffID = ModContent.BuffType<CosmicDisarray>();
        }

        public override void AI()
        {
			if (!justSpawned)
			{
                justSpawned = true;
                for (int i = 0; i < 5; i++)
                {
                    Vector2 newPos = Projectile.Center;
                    newPos -= Projectile.velocity * ((float)i * 0.25f);
                    int dust = Dust.NewDust(newPos, 1, 1, 156, 0f, 0f, 150, default(Color), 2.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = newPos;
                    Dust dust3 = Main.dust[dust];
                    dust3.velocity *= 0.2f;
                }
            }
			
			timer++;
			if (timer > 8)
			{
				if (Main.myPlayer == Projectile.owner)
				{
					Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Projectile.velocity.X * Main.rand.NextFloat(0.15f, 1.6f), Projectile.velocity.Y * Main.rand.NextFloat(0.15f, 1.5f), ModContent.ProjectileType<ZuesBoltIllusion2>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
				}
				timer = 0;
            }
        }
        
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 15; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 156, 0.85f * -Projectile.velocity.X, 0.85f * -Projectile.velocity.Y, 100, default(Color), 1.75f);
				Main.dust[dust].noGravity = true;
			}
        }
    }
}