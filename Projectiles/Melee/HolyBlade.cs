using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
    public class HolyBlade : ModProjectile
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
            vMax = 14;
            vAccel = .2f;
            reverse = false;
            delay = 20;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 400;
            Projectile.scale = 1.5f;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
            Player projOwner = Main.player[Projectile.owner];
            delay -= 1;
            if (delay <= 0 && reverse == false)
            {
                Projectile.velocity.X *= .965f;
                Projectile.velocity.Y *= .965f;
                if (Math.Abs(Projectile.velocity.X) < 4f && Math.Abs(Projectile.velocity.Y) < 4f)
                {
                    Projectile.velocity.X = -Projectile.velocity.X;
                    Projectile.velocity.Y = -Projectile.velocity.Y;
                    reverse = true;
                }
            }
            if (reverse)
            {
                Vector2 delta = Main.player[Main.myPlayer].Center - Projectile.Center;
                float dist = Vector2.Distance(Main.player[Main.myPlayer].Center, Projectile.Center);
                tVel = dist / 15;
                if (vMag < vMax && vMag < tVel)
                {
                    vMag += vAccel;
                }

                if (dist != 0)
                {
                    Projectile.velocity = Projectile.DirectionTo(Main.player[Main.myPlayer].Center) * vMag;
                }

                if (Math.Abs(delta.X) < 16 && Math.Abs(delta.Y) < 16)
                    Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 0, 466, (int)(Projectile.damage), 3, Main.myPlayer);
        }
    }
}