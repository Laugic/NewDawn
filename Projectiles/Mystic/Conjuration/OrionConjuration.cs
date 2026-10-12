using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class OrionConjuration : ConjurationProjectile
    {
        int numAbsorbed = 0;
        float theta = 0;
        int lightRadius = 0;
        int darkRadius = 0;

        public override void SetDefaults()
        {
            darkRadius = 0;
            lightRadius = 0;
            theta = 0;
            numAbsorbed = 0;
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 9999;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 250;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.UsingMysticWeapon == 0 || modPlayer.MysticMode != 3)
            {
                Projectile.Kill();
            }
            DustParticles();
        }

        private void DustParticles()
        {
            DustSpiral();
            DustDarkPulse();
            DustLightPulse();
        }

        private void DustSpiral()
        {
            theta += (float)Math.PI / 60;
            float radius = 120;
            Vector2 position;
            position.X = Projectile.Center.X + (float)Math.Cos(theta) * radius;
            position.Y = Projectile.Center.Y + (float)Math.Sin(theta) * radius;
            float speedX = (float)Math.Cos(theta + Math.PI) * 4;
            float speedY = (float)Math.Sin(theta + Math.PI) * 4;
            Dust.NewDust(position, 0, 0, ModContent.DustType<GalacticLight>(), speedX, speedY);
            position.X = Projectile.Center.X + (float)Math.Cos(theta + Math.PI) * radius;
            position.Y = Projectile.Center.Y + (float)Math.Sin(theta + Math.PI) * radius;
            speedX = (float)Math.Cos(theta) * 4;
            speedY = (float)Math.Sin(theta) * 4;
            Dust.NewDust(position, 0, 0, ModContent.DustType<GalacticLight>(), speedX, speedY);
        }

        private void DustDarkPulse()
        {
            darkRadius += 7;
            if (darkRadius > 0 && darkRadius < 480)
            {
                for (int i = 0; i < 24; i++)
                {
                    float thetaDark = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                    Vector2 position;
                    position.X = Projectile.Center.X + (float)Math.Cos(thetaDark) * darkRadius;
                    position.Y = Projectile.Center.Y + (float)Math.Sin(thetaDark) * darkRadius;
                    Dust.NewDust(position, 0, 0, ModContent.DustType<GalacticDark>());
                }
            }
            else if (darkRadius > 540)
            {
                darkRadius = 0;
            }
        }

        private void DustLightPulse()
        {
            lightRadius -= 3;
            if(lightRadius > 0 && lightRadius < 240)
            {
                for(int i = 0; i < 12; i++)
                {
                    float thetaLight = (float)(Main.rand.NextDouble() * 2 * Math.PI);
                    Vector2 position;
                    position.X = Projectile.Center.X + (float)Math.Cos(thetaLight) * lightRadius;
                    position.Y = Projectile.Center.Y + (float)Math.Sin(thetaLight) * lightRadius;
                    Dust.NewDust(position, 0, 0, ModContent.DustType<GalacticLight>());
                }
            }
            else if(lightRadius <= 0)
            {
                lightRadius = 320;
            }
        }

        public override void OnKill(int timeLeft)
        {
            
        }
    }
}