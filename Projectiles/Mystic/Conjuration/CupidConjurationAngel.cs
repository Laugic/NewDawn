using System;
using NewDawn.Dusts;
using NewDawn.Projectiles.Mystic.Illusion;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.Mystic.Conjuration
{
    public class CupidConjurationAngel : ConjurationProjectile
    {
        public int delay = 4;
        private int index = 0;
        private bool spawned = false;
        private float theta = 0f;
        private float vMag = 0f;
        private float vMax = 0f;
        private float vAccel = 0f;
        private float reloadMax = 0;
        private int reload = 60;
        private int attack = 0;

        public override void SetDefaults()
        {
            reloadMax = 0;
            reload = 60;
            vMag = 0;
            vMax = 20;
            vAccel = .2f;
            theta = 0f;
            index = 0;
            spawned = false;
            delay = 2;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 5;
        }

        public override void AI()
        {
            Projectile.rotation = 0;
            
            if (!spawned)
            {
                spawned = true;
                Projectile.frame = Main.rand.Next(5);
                if(Projectile.frame == 0 || Projectile.frame == 2)
                {
                    reloadMax = 120;
                    attack = 1;
                }
                if (Projectile.frame == 1 || Projectile.frame == 4)
                {
                    reloadMax = 120;
                    attack = 2;
                }
                if (Projectile.frame == 3)
                {
                    reloadMax = 60;
                    attack = 3;
                }
            }

            //Movement
            Player player = Main.player[Projectile.owner];
            if (index == 0)
                index = player.ownedProjectileCounts[ModContent.ProjectileType<CupidConjurationAngel>()];
            Projectile.tileCollide = false;
            theta += (float)(Math.PI / 120);
            float mag = 48;
            Vector2 rot = Projectile.position;
            rot.X = (float)Math.Cos(theta) * mag;
            rot.Y = (float)Math.Sin(theta) * mag;
            Vector2 targetPos = player.Center + rot;
            Vector2 direction = targetPos - Projectile.Center;
            float dist = Vector2.Distance(targetPos, Projectile.Center);
            float tVel = dist / 15;
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
                Projectile.velocity = Projectile.DirectionTo(targetPos) * vMag;
            }

            //Attack
            reload++;
            if(reload >= reloadMax)
            {
                //if(attack == 1)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<CupidArrow>(), (int)(Projectile.damage / 1f), 3, Main.myPlayer);
                    }
                    reload = 0;
                }
                /*if (attack == 2)
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, projectile.Center.Y, 0, 0, ModContent.ProjectileType<CupidHoming>(), (int)(projectile.damage / 1f), 3, Main.myPlayer);
                    }
                    reload = 0;
                }
                if (attack == 3)
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, projectile.Center.Y, 0, 0, ModContent.ProjectileType<CupidBurst>(), (int)(projectile.damage / 1f), 3, Main.myPlayer);
                    }
                    reload = 0;
                }*/
            }
            if(Main.rand.Next(8) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Pink>(), 0, 0);
        }
    }
}