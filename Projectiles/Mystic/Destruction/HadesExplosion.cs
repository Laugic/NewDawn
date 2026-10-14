using Terraria;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class HadesExplosion : DestructionProjectile
    {
        int projMaxTimeLeft = 0;
        public override void SetDefaults()
        {
            projMaxTimeLeft = 0;
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = 999;
            Projectile.timeLeft = 32;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 8;
            Projectile.scale *= 2f;
        }

        public override void AI()
        {
            if (projMaxTimeLeft == 0)
                projMaxTimeLeft = Projectile.timeLeft;
            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;

            Projectile.frameCounter++;
            if (Projectile.frameCounter > projMaxTimeLeft / Main.projFrames[Projectile.type])
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 8)
            {
                Projectile.frame = 8;
            }
        }
    }
}