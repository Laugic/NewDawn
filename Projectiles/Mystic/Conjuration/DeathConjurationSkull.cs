using Microsoft.Xna.Framework;
using Terraria;


namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class DeathConjurationSkull : ConjurationProjectile
    {
        public Color colorType;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.95f;

            if (Projectile.velocity.X > 0f)
            {
                Projectile.rotation += 0.25f;
            }
            else
            {
                Projectile.rotation -= 0.25f;
            }

            if (Projectile.timeLeft < 20)
            {
                Projectile.alpha += 8;
            }
        }
    }
}
