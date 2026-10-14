using Terraria.ModLoader;

namespace NewDawn.Projectiles.Plague
{
    public class JunglePlagueSpore : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
        }

        public override void AI()
        {
            Projectile.velocity *= .98f;
            Projectile.alpha += 3;
            if (Projectile.alpha > 250)
                Projectile.Kill();
        }
    }
}