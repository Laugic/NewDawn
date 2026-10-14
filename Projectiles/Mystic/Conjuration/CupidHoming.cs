using System;
using NewDawn.Dusts;
using Terraria;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class CupidHoming : ConjurationProjectile
    {
        public float mystDmg = 0;
        public float mystDur = 0;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];
                if (!target.friendly)
                {
                    float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                    float shootToY = target.position.Y - Projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                    
                    if (distance < 480f && !target.friendly && target.active && target.damage > 0)
                    {
                        distance = 1f / distance;
                        shootToX *= distance * 5;
                        shootToY *= distance * 5;
                        
                        int mag = 2;
                        if(Projectile.velocity.X < shootToX)
                        {
                            Projectile.velocity.X += (shootToX - Projectile.velocity.X) / mag;
                        }
                        if(Projectile.velocity.Y < shootToY)
                        {
                            Projectile.velocity.Y += (shootToY - Projectile.velocity.Y) / mag;
                        }
                        if (Projectile.velocity.X > shootToX)
                        {
                            Projectile.velocity.X -= (Projectile.velocity.X - shootToX) / mag;
                        }
                        if (Projectile.velocity.Y > shootToY)
                        {
                            Projectile.velocity.Y -= (Projectile.velocity.Y - shootToY) / mag;
                        }
                    }
                }
            }
            if(Main.rand.Next(4) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Pink>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        }
    }
}