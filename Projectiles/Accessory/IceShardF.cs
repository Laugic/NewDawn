using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.Accessory
{
	public class IceShardF : ModProjectile
    {
        public float delay = 4;

        public override void SetDefaults()
        {
            delay = 4;
            Projectile.width = 54;
            Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
        }

        

        public override void AI()
        {
            Projectile.rotation += .2f;

            delay -= 1;
            if (delay <= 0)
            {
                delay = 2;
                for (int i = 0; i < 200; i++)
                {
                    NPC target = Main.npc[i];
                    if (!target.friendly)
                    {
                        float shootToX = target.position.X + (float)target.width * 0.5f - Projectile.Center.X;
                        float shootToY = target.position.Y - Projectile.Center.Y;
                        float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                        if (distance < 480f && !target.friendly && target.active)
                        {
                            distance = 1f / distance;

                            shootToX *= distance * 5;
                            shootToY *= distance * 5;

                            if (Projectile.velocity.X < shootToX)
                            {
                                Projectile.velocity.X += (shootToX - Projectile.velocity.X) / 6;
                            }
                            if (Projectile.velocity.Y < shootToY)
                            {
                                Projectile.velocity.Y += (shootToY - Projectile.velocity.Y) / 6;
                            }
                            if (Projectile.velocity.X > shootToX)
                            {
                                Projectile.velocity.X -= (Projectile.velocity.X - shootToX) / 6;
                            }
                            if (Projectile.velocity.Y > shootToY)
                            {
                                Projectile.velocity.Y -= (Projectile.velocity.Y - shootToY) / 6;
                            }
                        }
                    }
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, (int)(120));
        }
    }
}