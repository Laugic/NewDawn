using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace NewDawn.Projectiles.Ranged
{
    public class BigSnowball : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
        }


        public override void AI()
        {
            var multiplier = Math.Min(300 - Projectile.timeLeft, 30) / 30f;
            Projectile.velocity.Y += .5f * multiplier;
            Projectile.velocity.X *= 1 - .02f * multiplier;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Water, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f);

        }
        public override void OnKill(int timeLeft)
        {
            int shards = 3;
            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < shards; i++)
                {
                    var theta = (float)(Math.PI * Main.rand.NextDouble() * 2);
                    var newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                    newVel *= 10;
                    Projectile.NewProjectile(Main.player[Projectile.owner].GetSource_FromAI(), Projectile.Center.X, Projectile.Center.Y, newVel.X, newVel.Y, (int)Projectile.ai[0], Projectile.damage, 3, Main.myPlayer);
                }
            }
            SoundEngine.PlaySound(SoundID.Item51, Projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            SoundEngine.PlaySound(SoundID.Item51, Projectile.position);
            return false;
        }
    }
}
