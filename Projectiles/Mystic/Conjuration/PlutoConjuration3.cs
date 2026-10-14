using System;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class PlutoConjuration3 : ConjurationProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
        }
    }
}