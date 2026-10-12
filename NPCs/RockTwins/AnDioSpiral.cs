using System;
using NewDawn.Buffs;
using NewDawn.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.RockTwins
{
    public class AnDioSpiral : ModProjectile
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
        public override void SetDefaults()
        {
            zImmune = true;
            theta = 0;
            vel = 0;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            power = 0;
            stopped = false;
            spawned = false;
            //mystDmg = (float)projectile.damage;
            //mystDur = 1f + projectile.knockBack;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 320;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }


        public override void AI()
        {
            bitherial = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Blue>(), 0f, 0f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            theta -= 3.14f / 60;
            if (vel < 20f)
            {
                vel += .1f;
                vel *= 1.01f;
            }
            Projectile.velocity.X = (float)(Math.Cos(theta) * vel);
            Projectile.velocity.Y = (float)(Math.Sin(theta) * vel);
        }
        public override void OnKill(int timeLeft)
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
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            player.AddBuff(ModContent.BuffType<ForHonor>(), 300, true);
            player.AddBuff(ModContent.BuffType<ForGlory>(), 300, true);
        }
    }
}