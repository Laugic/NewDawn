using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Dusts;
using System;
using Microsoft.Xna.Framework;

namespace NewDawn.Projectiles.Ranged
{
    public class BysballProjectile : ModProjectile
    {
        int tileLife = 0;
        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 8;
            Projectile.timeLeft = 3 * 60;
            tileLife = 0;
            Projectile.tileCollide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = oldVelocity;
            tileLife++;
            if (tileLife > 15)
                Projectile.Kill();
            return false;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .3f;
            Projectile.velocity.X *= .985f;
            if(Main.rand.Next(4) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            var theta = (float)(Math.PI * Main.rand.NextDouble() * 2);
            var newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
            newVel *= 120;
            DrawDust(Projectile.position, newVel, 20);
            Projectile.position += newVel;
            newVel /= 120;
            newVel *= -12;
            Projectile.velocity = newVel;
            base.ModifyHitNPC(target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        private void DrawDust(Vector2 position, Vector2 shift, int numDust)
        {
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDust(Projectile.position + shift / numDust * i, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}