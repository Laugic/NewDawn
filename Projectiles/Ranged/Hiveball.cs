using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
    public class Hiveball : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            var multiplier = Math.Min(600 - Projectile.timeLeft, 30) / 30f;
            Projectile.velocity.Y += .5f * multiplier;
            Projectile.velocity.X *= 1 - .015f * multiplier;

            Projectile.rotation += Projectile.velocity.X / 30;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Player player = Main.player[Projectile.owner];
                int numBees = 1;
                if (Main.rand.NextBool(3))
                    numBees++;
                for (int i = 0; i < numBees; i++)
                {
                    double theta = Main.rand.NextDouble() * Math.PI * 2;
                    if (player.strongBees && Main.rand.NextBool(3))
                        Projectile.NewProjectile(Main.player[Projectile.owner].GetSource_FromAI(), Projectile.Center.X - 4 + Main.rand.Next(9), Projectile.Center.Y - 4 + Main.rand.Next(9), 4 * (float)Math.Sin(theta), 4 * (float)Math.Cos(theta), ProjectileID.GiantBee, (int)(Projectile.damage * .8f), 2f, Projectile.owner);
                    else
                        Projectile.NewProjectile(Main.player[Projectile.owner].GetSource_FromAI(), Projectile.Center.X - 4 + Main.rand.Next(9), Projectile.Center.Y - 4 + Main.rand.Next(9), 4 * (float)Math.Sin(theta), 4 * (float)Math.Cos(theta), ProjectileID.Bee, (int)(Projectile.damage * .4f), 2f, Projectile.owner);
                }
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}
