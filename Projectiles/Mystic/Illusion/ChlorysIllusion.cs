using System;
using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
    public class ChlorysIllusion : IllusionProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
            Projectile.scale *= 1f;
            buffID = ModContent.BuffType<Fertile>();
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + .785f;
            Projectile.velocity.Y += Projectile.ai[0] + .1f;
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
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 0.8f;
            }
            return false;
        }
    }
}