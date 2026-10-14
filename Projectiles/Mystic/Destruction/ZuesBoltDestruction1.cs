using System;
using Terraria;
using Microsoft.Xna.Framework;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class ZuesBoltDestruction1 : DestructionProjectile
    {
		public int timer = 0;
		public int timer2 = 0;
		
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;
			Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Main.projFrames[Projectile.type] = 5;
		}

        public override void AI()
        {
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            
            for (int i = 0; i < 5; i++)
            {
                Vector2 dustPos = Projectile.Center;
                dustPos -= Projectile.velocity * ((float)i * 0.25f);
                int newDust = Dust.NewDust(dustPos, 1, 1, 156, 0f, 0f, 150, default(Color), 1.5f);
                Main.dust[newDust].noGravity = true;
                Main.dust[newDust].position = dustPos;
                Dust dust3 = Main.dust[newDust];
                dust3.velocity *= 0.2f;
            }
        }
        
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 8; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 156, Main.rand.Next((int)-4f, (int)4f), Main.rand.Next((int)-4f, (int)4f), 125, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
		
		public override void PostAI()
        {         
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 5)
            {
                Projectile.frame = 0;
                return;
            }
        }
    }
}