using Microsoft.Xna.Framework;
using System;

namespace NewDawn.Projectiles.Mystic.Destruction
{
    public class CharonDestruction : DestructionProjectile
    {
        int delay = 0;
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X / 40;

            Projectile.velocity.Y += .4f;
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
            Projectile.velocity.Y += .1f;
            Projectile.velocity *= 0.9f;
            return false;
        }
    }
}