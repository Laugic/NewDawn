using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class SaturnConjuration1 : ConjurationProjectile
    {
        float theta = 0;
        float mag = 0;
        int delay = 0;
        Vector2 targetPos;
        float vMax = 16;
        float vMag = 0;
        float vAccel = .8f;

        public override void SetDefaults()
        {
            vMag = 0;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.scale *= .8f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void AI()
        {
            theta += 3.1415f / 120;
            theta += 3.1415f / 120 * (mag / 320);
            AdjustMagnitude();
            targetPos.X = Main.player[Projectile.owner].Center.X + (float)(Math.Cos(theta) * mag);
            targetPos.Y = Main.player[Projectile.owner].Center.Y + (float)(Math.Sin(theta) * mag);
            Retarget();
            CheckBoom();
            CheckShoot();
            Projectile.rotation += .02f;
        }

        private void AdjustMagnitude()
        {
            if (mag < 80)
                mag += 2;
            if (mag < 160)
                mag += 1;
            if (mag < 240)
                mag += .5f;
            if (mag < 480)
                mag += .25f;
            else
                mag += .125f;
        }

        private void Retarget()
        {
            Projectile.position = targetPos;
        }

        private void CheckBoom()
        {
            delay++;
            if (delay > 8 * 60)
            {
                if (delay < 16 * 60)
                {
                    if (Main.rand.Next(16 * 60 - delay) == 0 && Main.rand.Next(2) == 0)
                        Boom();
                }
                else
                {
                    Boom();
                }
            }
        }

        private void Boom()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                for (int k = 0; k < 8; k++)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, Main.rand.Next((int)-10f, (int)10f), Main.rand.Next((int)-10f, (int)10f), ModContent.ProjectileType<SaturnConjuration2>(), Projectile.damage, 3f, Main.myPlayer);
                }
                Projectile.Kill();
            }
        }

        private void CheckShoot()
        {
            if (Main.rand.Next(16 * 60 - delay) == 0 || Main.rand.Next(18 * 60 - delay) == 0)
            {
                Vector2 direction = Projectile.DirectionTo(Main.player[Projectile.owner].Center);
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (int)(direction.X * mag / 16), (int)(direction.Y * mag / 16), ModContent.ProjectileType<SaturnConjuration2>(), Projectile.damage, 3f, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (int)(direction.X * -mag / 16), (int)(direction.Y * -mag / 16), ModContent.ProjectileType<SaturnConjuration2>(), Projectile.damage, 3f, Main.myPlayer);
                }
            }
        }
    }
}
