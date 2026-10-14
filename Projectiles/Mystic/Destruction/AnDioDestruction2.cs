using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Destruction
{
    public class AnDioDestruction2 : DestructionProjectile
    {
        public bool stopped = false;
        public int delay = 0;
        public bool spawned = false;
        public float theta = 0;
        public float vel = 0;
        public bool zImmune = true;
        public float tVel = 0;
        public float distance = 0;
        public Vector2 origin;
        public Vector2 originV;
        public double mag = 10;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Loki Spiral");
        }
        public override void SetDefaults()
        {
            mag = 10;
            zImmune = true;
            theta = 0;
            vel = 0;
            stopped = false;
            spawned = false;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 320;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

		public override Color? GetAlpha(Color lightColor)
		{
			return ((Color.White * 0.85f) * (0.1f * Projectile.timeLeft));
		}
		
        public override void AI()
        {
			if (Projectile.timeLeft == 320)
			{
				float num102 = 15f;
				int num103 = 0;
				while ((float)num103 < num102)
				{
					Vector2 vector12 = Vector2.UnitX * 0f;
					vector12 += -Vector2.UnitY.RotatedBy((double)((float)num103 * (6.28318548f / num102)), default(Vector2)) * new Vector2(2f, 6f);
					vector12 = vector12.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
					int num104 = Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<Blue>(), 0f, 0f, 100, default(Color), 1f);
					Main.dust[num104].scale = 1.35f;
					Main.dust[num104].noGravity = true;
					Main.dust[num104].position = Projectile.Center + vector12;
					Main.dust[num104].velocity = Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 1f;
					int num = num103;
					num103 = num + 1;
				}
			}
			
            if(origin.X == 0)
            {
                origin.X = Projectile.position.X;
                origin.Y = Projectile.position.Y;
                originV.X = Projectile.velocity.X;
                originV.Y = Projectile.velocity.Y;
            }
           
			for (int k = 0; k < 2; k++)
            {
				int num234 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y) - Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Blue>(), 0f, 0f, 100, default(Color), 1f);
				Dust dust3 = Main.dust[num234];
				dust3 = Main.dust[num234];
				dust3.velocity *= 0.5f;
				Main.dust[num234].noGravity = true;
			}
			
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            theta += 3.14f / 30;
            mag += .75;
            origin += originV/5;
            double targetX = origin.X + mag * Math.Cos(theta + 3.14) - Projectile.width / 2;
            double targetY = origin.Y + mag * Math.Sin(theta + 3.14);
            //projectile.position.X = (float)targetX;
            //projectile.position.Y = (float)targetY;
            distance = (float)Math.Sqrt((targetX - Projectile.position.X) * (targetX - Projectile.position.X) + (targetY - Projectile.position.Y) * (targetY - Projectile.position.Y));
            tVel = distance / 6;

            if (vel < tVel)
            {
                vel += .2f;
                vel *= 1.1f;
            }
            if (vel > tVel)
            {
                vel -= .1f;
                vel *= .95f;
            }
            Projectile.velocity.X = (float)Math.Abs((Projectile.position.X - targetX) / distance * vel);
            if (targetX < Projectile.position.X)
                Projectile.velocity.X *= -1;
            Projectile.velocity.Y = (float)Math.Abs((Projectile.position.Y - targetY) / distance * vel);
            if (targetY < Projectile.position.Y)
                Projectile.velocity.Y *= -1;
        }
    }
}