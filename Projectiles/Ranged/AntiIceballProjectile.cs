using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
    public class AntiIceballProjectile : ModProjectile
    {
        int delay = 0;
        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            delay = 0;
        }

        public override void AI()
        {
            delay++;
            if(delay > 10)
                Projectile.velocity.Y -= .6f;
            Projectile.velocity.X *= .975f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}