using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Overflow
{
    public class OverflowThermalite : MysticProjectile
    {
        public Color colorType;
        bool shot = false;
        Vector2 shotDir;
        public override void SetDefaults()
        {
            shot = false;
            shotDir = Vector2.Zero;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 3 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }
        public override void AI()
        {
            if(shotDir == Vector2.Zero)
            {
                shotDir = Projectile.position + Projectile.velocity * 60;
                double theta = Main.rand.NextFloat() * Math.PI * 2;
                float mag = 8;
                Projectile.velocity = new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag);
            }


            if(shot)
            {
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            }
            else
            {
                Projectile.rotation += Projectile.velocity.Length() * (Projectile.velocity.X > 0?1:-1) / 10f;
                Projectile.velocity *= .92f;
                if (Projectile.velocity.Length() < .1f)
                {
                    shot = true;
                    var newVel = shotDir - Projectile.position;
                    newVel.Normalize();
                    newVel *= 12;
                    Projectile.velocity = newVel;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 16, Main.rand.Next((int)-2f, (int)2f), Main.rand.Next((int)-2f, (int)2f), 0, new Color(0, 217, 255), 0.75f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = true;
            }
        }
    }
}