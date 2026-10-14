using System;
using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.RockTwins
{
    public class AnDioSpiral2 : ModProjectile
    {
        public bool bitherial = true;
        public bool stopped = false;
        public int power = 0;
        public int damage = 0;
        public int delay = 0;
        public bool spawned = false;
        public float theta = 0;
        public float vel = 0;
        public bool zImmune = true;
        public float tVel = 0;
        public float distance = 0;
        public double targetX = 0;
        public double targetY = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AnDio Spiral");
        }
        public override void SetDefaults()
        {
            zImmune = true;
            theta = 0;
            vel = 0;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            power = 0;
            stopped = false;
            spawned = false;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 10 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            delay = 0;
        }


        public override void AI()
        {
            bitherial = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Blue>(), 0f, 0f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            theta -= 3.14f / 60;
            int mag = 360;
            Vector2 move = Vector2.Zero;
            float distance = 1400f;
            bool target = false;
            for (int k = 0; k < 8; k++)
            {
                if (Main.player[k].active)
                {
                    Vector2 newMove = Main.player[k].Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        distance = distanceTo;
                        targetX = Main.player[k].position.X + mag * Math.Cos(theta + 3.14) - Projectile.width / 2;
                        targetY = Main.player[k].position.Y + mag * Math.Sin(theta + 3.14);
                    }
                }
            }
            distance = (float)Math.Sqrt((targetX - Projectile.position.X) * (targetX - Projectile.position.X) + (targetY - Projectile.position.Y) * (targetY - Projectile.position.Y));
            tVel = distance / 10;

            if (vel < tVel)
            {
                vel += .1f;
                vel *= 1.05f;
            }
            if (vel > tVel)
            {
                vel -= .1f;
                vel *= .95f;
            }
            Projectile.velocity.X = (float)Math.Abs((Projectile.position.X - targetX) / distance * vel);
            if (targetX < Projectile.position.X)
                Projectile.velocity.X *= -1;
            Projectile.velocity.Y = (float)Math.Abs((Projectile.position.Y - targetY) / distance * vel);
            if (targetY < Projectile.position.Y)
                Projectile.velocity.Y *= -1;
            delay++;
            if((delay > 6 * 60 && Main.rand.Next(10 * 60 - delay) == 0 && Main.rand.Next(2) == 0) || delay > 9 * 60 + 50)
            {   
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 7, 0, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -7, 0, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 7, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, -7, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, 5, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, -5, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, -5, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, 5, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                }
                Projectile.Kill();
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<ForHonor>(), 300, true);
            target.AddBuff(ModContent.BuffType<ForGlory>(), 300, true);
        }
    }
}