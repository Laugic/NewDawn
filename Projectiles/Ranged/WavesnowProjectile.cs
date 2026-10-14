using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
    public class WavesnowProjectile : ModProjectile
    {
        int delay = 0;
        bool spawned = false;
        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 3 * 60;
            Projectile.tileCollide = false;
            delay = 15;
            Projectile.penetrate = -1;
            spawned = false;
        }

        public override void AI()
        {
            if(!spawned)
            {
                spawned = true;
                Projectile.ai[0] = 1 - Main.rand.Next(2) * 2;
            }
            delay++;
            if (delay > 30)
            {
                delay = 0;
                Projectile.ai[0] *= -1;
            }
            Projectile.velocity.Y += .8f * Projectile.ai[0];
            Projectile.velocity.X *= .99f;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}