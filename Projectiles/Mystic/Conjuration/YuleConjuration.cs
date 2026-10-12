using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class YuleConjuration : ConjurationProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .2f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}