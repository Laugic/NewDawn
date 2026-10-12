using System;
using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.RockTwins
{
    public class AnDioEnergy : ModProjectile
    {
        public bool bitherial = true;
        public bool stopped = false;
        public int power = 0;
        public bool spawned = false;
        public int time = 0;
        public bool zImmune = true;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("AnDio Energy");
        }
        public override void SetDefaults()
        {
            zImmune = true;
            time = 0;
            NewDawnVars.eProjectiles.Add(Projectile.type);
            power = 0;
            stopped = false;
            spawned = false;
            //mystDmg = (float)projectile.damage;
            //mystDur = 1f + projectile.knockBack;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.timeLeft = 320;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1.25f;
        }


        public override void AI()
        {
            bitherial = true;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Red>(), 0f, 0f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Blue>(), 0f, 0f);
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            if (Projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 1f;
            }
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
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                Projectile.velocity = (20 * Projectile.velocity + move) / 2f;
                AdjustMagnitude(ref Projectile.velocity);
            }
            time++;
            if (time > 180 && Main.netMode != 1)
            {
                int dist = 3;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 7, 0, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -7, 0, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 7, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, -7, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, 5, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 5, -5, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, -5, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, -5, 5, ModContent.ProjectileType<DioBall>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<DioBallShot>(), (int)(Projectile.damage / 1.2f), 3, Main.myPlayer);
                /*Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X + 140 * dist, projectile.position.Y + 000 * dist, -.7f, 0.0f, ModContent.ProjectileType<AndeBall>(), (int)(projectile.damage / 1.2f), 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X + 100 * dist, projectile.position.Y - 100 * dist, -.5f, 0.5f, ModContent.ProjectileType<AndeBall>(), (int)(projectile.damage / 1.2f), 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X + 000 * dist, projectile.position.Y - 140 * dist, -.0f, 0.7f, ModContent.ProjectileType<AndeBall>(), (int)(projectile.damage / 1.2f), 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X - 100 * dist, projectile.position.Y - 100 * dist, 0.5f, 0.5f, ModContent.ProjectileType<AndeBall>(), (int)(projectile.damage / 1.2f), 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X - 140 * dist, projectile.position.Y + 000 * dist, 0.7f, 0.0f, ModContent.ProjectileType<AndeBall>(), (int)(projectile.damage / 1.2f), 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X - 100 * dist, projectile.position.Y + 100 * dist, 0.5f, -.5f, ModContent.ProjectileType<AndeBall>(), (int)(projectile.damage / 1.2f), 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X + 000 * dist, projectile.position.Y + 140 * dist, -.0f, -.7f, ModContent.ProjectileType<AndeBall>(), (int)(projectile.damage / 1.2f), 3f, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position.X + 100 * dist, projectile.position.Y + 100 * dist, -.5f, -.5f, ModContent.ProjectileType<AndeBall>(), (int)(projectile.damage / 1.2f), 3f, Main.myPlayer);*/
                Projectile.Kill();
            }
        }


        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 6f / magnitude;
            }
        }


        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            player.AddBuff(ModContent.BuffType<ForHonor>(), 300, true);
            player.AddBuff(ModContent.BuffType<ForGlory>(), 300, true);
        }
    }
}