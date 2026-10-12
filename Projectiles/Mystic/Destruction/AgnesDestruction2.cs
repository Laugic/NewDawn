using System;

namespace NewDawn.Projectiles.Mystic.Destruction
{
    public class AgnesDestruction2 : DestructionProjectile
    {
        int delay = 0;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
            Projectile.ignoreWater = true;
            delay = 0;
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
                Projectile.alpha = Math.Max(Projectile.alpha - 20, 0);
        }
    }
}