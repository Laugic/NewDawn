using Terraria;
using Microsoft.Xna.Framework;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class ZuesBoltDestruction3 : DestructionProjectile
    {
		public int timer = 0;
		public float reduce = 0f;
	
		public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
			Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
			Projectile.friendly = true;
            Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

        public override void AI()
        {
			for (int i = 0; i < 3; i++)
			{
				Vector2 dustPos = Projectile.Center;
				dustPos -= Projectile.velocity * ((float)i * 0.25f);
				Projectile.alpha = 50;
				int newDust = Dust.NewDust(dustPos, 1, 1, 156, 0f, 0f, 150, default(Color), 1.75f - reduce);
				Main.dust[newDust].noGravity = true;
				Main.dust[newDust].position = dustPos;
				Dust dust3 = Main.dust[newDust];
				dust3.velocity *= 0.2f;
			}
			
			if (reduce < 2f)
			{
				reduce += 0.025f;
			}
			
			if (Projectile.ai[0] == 1f)
			{
				Projectile.extraUpdates = 2;
				timer++;
				if (timer > 1)
				{
					Projectile.velocity.Y += Main.rand.NextFloat(-3f, 3f);
					Projectile.velocity.X += Main.rand.NextFloat(-3f, 3f);
					timer = 0;
				}
			}
			else
			{
				Projectile.extraUpdates = 4;
				timer++;
				if (timer > 2)
				{
					Projectile.velocity.Y += Main.rand.NextFloat(-1.15f, 1.15f);
					Projectile.velocity.X += Main.rand.NextFloat(-1.15f, 1.15f);
					timer = 0;
				}
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