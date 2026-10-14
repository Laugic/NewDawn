using NewDawn.Projectiles.Mystic.Misc;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class ThorShadowConjuration : ConjurationProjectile
    {
        int delay = 0;
        public override void SetDefaults()
        {
            Projectile.width = 130;
            Projectile.height = 130;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10 * 60;
            Main.projFrames[Projectile.type] = 4;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation = 0;
            Projectile.velocity *= .95f;
            delay++;
            if(delay >= 45)
            {
                delay = 0;
                if (Main.myPlayer == Projectile.owner)
                {
                    //Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 122);
                    for (int i = 0; i < 6; i++)
                    {
                        double theta = Main.rand.NextDouble() * Math.PI * 2;
                        float mag = 12;
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + 2 * mag * (float)Math.Cos(theta), Projectile.Center.Y + 2 * mag * (float)Math.Sin(theta), (float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<Bolt>(), (int)(Projectile.damage), 8, Main.myPlayer);
                    }
                }
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
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
                return;
            }
        }
    }
}