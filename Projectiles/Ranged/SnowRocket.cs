using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using NewDawn.Dusts;
using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace NewDawn.Projectiles.Ranged
{
    public class SnowRocket : ModProjectile
    {
        int tileLife = 0;
        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 4 * 60;
            Projectile.tileCollide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.timeLeft = Math.Min(Projectile.timeLeft, 2);
            Projectile.ai[1] = 1;
            return false;
        }

        public override void AI()
        {
            Projectile.velocity *= 1.02f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);

            if (Main.rand.Next(4) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Smoke, -Projectile.velocity.X * 0.1f, -Projectile.velocity.Y * 0.1f);

            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 1 && Projectile.ai[1] == 1)
            {
                if (Main.player[Projectile.owner].active)
                {
                    var dist = Main.player[Projectile.owner].Center - Projectile.Center;
                    if (dist.Length() < 60)
                    {
                        var newVel = dist;
                        newVel.Normalize();
                        Main.player[Projectile.owner].velocity += newVel * Math.Min(40 / Math.Max(dist.Length(), 1) * 12, 30);
                    }
                }
                Projectile.velocity.X = 0f;
                Projectile.velocity.Y = 0f;
                Projectile.tileCollide = false;
                Projectile.alpha = 255;
                Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
                Projectile.width = 96;
                Projectile.height = 96;
                Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.timeLeft = Math.Min(Projectile.timeLeft, 2);
            Projectile.ai[1] = 1;
            base.ModifyHitNPC(target, ref modifiers);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int k = 0; k < 18; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 20, 0, 0);
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 217, 0, 0);
            }
        }
    }
}