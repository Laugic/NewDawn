using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
	public class ZuesBoltIllusion2 : IllusionProjectile
    {
		public int timer = 0;
		public float reduce = 0f;
        
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
			Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
			Projectile.friendly = true;
            Projectile.ignoreWater = true;
			Projectile.extraUpdates = 10;
        }

        public override void AI()
        {
            int num3;
			for (int num452 = 0; num452 < 3; num452 = num3 + 1)
			{
				Vector2 vector36 = Projectile.Center;
				vector36 -= Projectile.velocity * ((float)num452 * 0.25f);
				Projectile.alpha = 50;
				int num453 = Dust.NewDust(vector36, 1, 1, 156, 0f, 0f, 150, default(Color), 2.25f - reduce);
				Main.dust[num453].noGravity = true;
				Main.dust[num453].position = vector36;
				Dust dust3 = Main.dust[num453];
				dust3.velocity *= 0.2f;
				num3 = num452;
			}
			
			if (reduce < 2f)
			{
				reduce += 0.025f;
			}
			
			timer++;
			if (timer > 2)
			{
				Projectile.velocity.Y += Main.rand.NextFloat(-1.5f, 1.5f);
				Projectile.velocity.X += Main.rand.NextFloat(-1.5f, 1.5f);
				timer = 0;
            }
        }
        
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 156, 0.5f * -Projectile.velocity.X, 0.5f * -Projectile.velocity.Y, 100, default(Color), 0.5f);
				Main.dust[dust].noGravity = true;
			}
        }
    }
}