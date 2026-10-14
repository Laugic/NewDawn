using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Projectiles.Mystic.Overflow
{
	public class OverflowSpore : MysticProjectile
    {
        public int dir = 0;
        bool justSpawned = false;
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Overflow Spore");
		}

		public override void SetDefaults()
		{
            dir = 0;
            justSpawned = false;
            Projectile.width = 12;
			Projectile.height = 12;
            Projectile.timeLeft = 360;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            NewDawnVars.ShroomProjectiles.Add(Projectile.type);
        }
		
		public override Color? GetAlpha(Color lightColor)
		{
			return ((Color.White * 0.5f) * (0.05f * Projectile.timeLeft));
		}
		
        public override void AI()
        {
			if (!justSpawned)
			{
                justSpawned = true;
				for (int i = 0; i < 15; i++)
				{
					Vector2 vector12 = Vector2.UnitX * 0f;
					vector12 += -Vector2.UnitY.RotatedBy((double)((float)i * (6.28318548f / 15)), default(Vector2)) * new Vector2(2f, 6f);
					vector12 = vector12.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
					int newDust = Dust.NewDust(Projectile.Center, 0, 0, 56, 0f, 0f, 50, default(Color), 1f);
					Main.dust[newDust].noGravity = true;
					Main.dust[newDust].position = Projectile.Center + vector12;
					Main.dust[newDust].velocity = Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 0.5f;
				}
			}
			else
			{
				for (int k = 0; k < 2; k++)
				{
					int newDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y) - Projectile.velocity, Projectile.width, Projectile.height, 56, 0f, 0f, 50, default(Color), 0.75f);
					Dust dust3 = Main.dust[newDust];
					dust3 = Main.dust[newDust];
					dust3.velocity *= 0f;
					Main.dust[newDust].noGravity = true;
				}
			}
			
            if(dir == 0)
            {
                
                if (Main.rand.Next(0, 2) == 0)
                    dir = -1;
                else
                    dir = 1;
            }
			if (Projectile.velocity.Y < 2)
                Projectile.velocity.Y += .1f;
            if(dir == 1)
            {
                if(Main.rand.Next(145) == 0)
                    dir = -1;
                if (Projectile.velocity.X < 2)
                    Projectile.velocity.X += .1f;
            }
            if (dir == -1)
            {
                if (Main.rand.Next(145) == 0)
                    dir = 1;
                if (Projectile.velocity.X > -2)
                    Projectile.velocity.X -= .1f;
            }
        }
		
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 56, Main.rand.Next((int)-2f, (int)2f), Main.rand.Next((int)-2f, (int)2f), 0, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
    }
}