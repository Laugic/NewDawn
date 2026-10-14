using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
    public class JanusIllusionSandball : IllusionProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 4;
            Projectile.timeLeft = 600;
            buffID = ModContent.BuffType<SandyBuff>();
        }

        public override void AI()
        {
            Projectile.velocity.Y += Projectile.ai[0] + .4f;
            Vector2 newVel = -Projectile.velocity * 2 / 3f;
            float theta = Main.rand.Next(-30, 30) * (float)Math.PI / 180;
            float mag = Main.rand.Next(3, 7);
            newVel.X += (float)Math.Cos(theta) * mag;
            newVel.Y += (float)Math.Sin(theta) * mag;
            Dust.NewDust(Projectile.position - Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<White>(), newVel.X, newVel.Y);
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
                Projectile.velocity *= 0.75f;
            }
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                //Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Sandy>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}
