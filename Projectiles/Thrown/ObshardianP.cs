using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class ObshardianP : ModProjectile
    {
        bool boosted = false;
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 26;
            Projectile.friendly = true;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 2;
            Projectile.extraUpdates = 1;
            AIType = 48;
            boosted = false;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1f;
            if (!boosted)
            {
                Projectile.penetrate += (int)Projectile.ai[1];
                boosted = true;
            }
            if (Projectile.ai[0] >= 150f)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.15f;
                Projectile.velocity.X = Projectile.velocity.X * 0.99f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 0.85f;
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 2 * 60 + Main.rand.Next(60));
        }
    }
}