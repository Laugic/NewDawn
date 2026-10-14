using Microsoft.Xna.Framework;
using NewDawn.Buffs;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Fungon
{
    public class FungonIllusion : IllusionProjectile
    {
        public bool shift = false;

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 8;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Main.projFrames[Projectile.type] = 2;
            buffID = ModContent.BuffType<Spored>();
            Projectile.tileCollide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft -= 2;
            Projectile.velocity *= .92f;
            return false;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            Projectile.velocity *= .97f;

            if (Main.rand.Next(2) == 0)
            {
                int DustID = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 56, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 0, default(Color), 1.5f);
                Main.dust[DustID].noGravity = true;
            }

            if (Projectile.timeLeft > 20)
            {
                if (!shift)
                {
                    Projectile.alpha += 2;
                    Projectile.scale += 0.0075f;
                }
                else
                {
                    Projectile.alpha -= 2;
                    Projectile.scale -= 0.0075f;
                }
                if (Projectile.alpha > 175 && !shift)
                {
                    shift = true;
                }
                if (Projectile.alpha <= 25)
                {
                    shift = false;
                }
            }
            else
            {
                Projectile.alpha += 5;
            }
        }

        public override void PostAI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 15)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 2)
            {
                Projectile.frame = 0;
                return;
            }
        }
    }
}