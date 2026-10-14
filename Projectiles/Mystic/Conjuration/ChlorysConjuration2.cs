using System;
using Microsoft.Xna.Framework;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class ChlorysConjuration2 : ConjurationProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
            Projectile.scale *= 1f;
            NewDawnVars.SeedProjectiles.Add(Projectile.type);
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            Projectile.ai[0] += .01f;
            Projectile.velocity.Y += Projectile.ai[0];
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 0.8f;
            }
            return false;
        }
    }
}