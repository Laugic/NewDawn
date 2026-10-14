using System;
using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Magic
{
    public class AndesiaStaffProjectile : ModProjectile
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
            delay = 180;
            Projectile.width = 88;
            Projectile.height = 88;
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 400;
        }
        
        public override void AI()
        {
            Projectile.rotation =  -1.57f / 2;
            Player projOwner = Main.player[Projectile.owner];
            projOwner.AddBuff(ModContent.BuffType<ForHonor>(), 120);
            delay -= 1;
            if (delay % 10 == 0 && !reverse && Main.myPlayer == Projectile.owner)
            {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X - 3 + Main.rand.Next(0, 6), Projectile.Center.Y - 360 - 3 + Main.rand.Next(0, 6), 0, 0, ModContent.ProjectileType<Dioritite>(), (int)(Projectile.damage), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X - 3 + Main.rand.Next(0, 6), Projectile.Center.Y + 360 - 3 + Main.rand.Next(0, 6), 0, 0, ModContent.ProjectileType<Andesimite>(), (int)(Projectile.damage), 3, Main.myPlayer);
            }
            if(delay <= 0 && reverse == false)
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
    }
}