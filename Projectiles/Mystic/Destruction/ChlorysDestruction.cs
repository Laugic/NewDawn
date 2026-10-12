using System;

namespace NewDawn.Projectiles.Mystic.Destruction
{
    public class ChlorysDestruction : DestructionProjectile
    {
        int delay = 0;
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            NewDawnVars.SeedProjectiles.Add(Projectile.type);
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            delay++;
            if (delay > 60)
            {
                Projectile.ai[0] += .02f;
                Projectile.velocity.Y += Projectile.ai[0];
            }
        }
    }
}