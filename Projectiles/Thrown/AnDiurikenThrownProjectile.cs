using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Thrown
{
    public class AnDiurikenThrownProjectile : ModProjectile
    {
        public int rot = 0;
        public int delay = 0;
        public bool reverse = false;
        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0; //Target Velocity
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
            Projectile.rotation += 1.57f / 6;
            Projectile.velocity.Y += .1f;
            delay -= 1;
            if(delay <= 0 && reverse == false)
            {
                reverse = true;
            }
            if (reverse)
            {
                Projectile.tileCollide = false;
                Vector2 delta = Main.player[Projectile.owner].Center - Projectile.Center;
                float dist = Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center);
                tVel = dist / 15;
                if (vMag < vMax && vMag < tVel)
                {
                    vMag += vAccel;
                }
                /*
                if (vMag > tVel)
                {
                    vMag -= vAccel;
                }*/

                if (dist != 0)
                {
                    Projectile.velocity = Projectile.DirectionTo(Main.player[Projectile.owner].Center) * vMag;
                }
                //Return
                if (Math.Abs(delta.X) < 16 && Math.Abs(delta.Y) < 16)
                    Projectile.Kill();
            }
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //projectile.ai[0] += 0.1f;
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
        /*
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Steamy>(), 120);       //Add Onfire buff to the NPC for 1 second
        }*/
    }
}