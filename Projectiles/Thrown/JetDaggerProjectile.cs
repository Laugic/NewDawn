using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class JetDaggerProjectile : ModProjectile
    {
        public int rot = 0;
        public int delay = 0;
        public bool reverse = false;
        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0;
        public float vMag = 0;

        public override void SetDefaults()
        {
            vMax = 20;
            vAccel = .2f;
            reverse = false;
            delay = 100;
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 600;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Projectile.velocity.Y += .1f;

            Vector2 delta = Main.player[Projectile.owner].Center - Projectile.Center;
            float dist = Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center);
            if (reverse)
            {
                if (Main.rand.Next(4) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<White>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                Projectile.tileCollide = false;
                tVel = dist / 15;
                if (vMag < vMax && vMag < tVel)
                {
                    vMag += vAccel;
                }
                
                if (vMag > tVel)
                {
                    vMag = tVel;
                }

                if (dist != 0)
                {
                    Projectile.velocity = Projectile.DirectionTo(Main.player[Projectile.owner].Center) * vMag;
                }

                if (Math.Abs(delta.X) < 16 && Math.Abs(delta.Y) < 16)
                    Projectile.Kill();
            }
            if(dist > 1800)
                Projectile.Kill();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            reverse = true;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            reverse = true;
        }
    }
}