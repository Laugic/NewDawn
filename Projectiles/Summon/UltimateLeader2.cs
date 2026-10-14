using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Summon
{

    public class UltimateLeader2 : ModProjectile
    {
        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0;
        public float vMag = 0;
        public int index = 0;
        public float theta = 0f;

        public override void SetDefaults()
        {
            theta = 0f;
            index = 0;
            vMax = 14;
            vAccel = .1f;
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = .5f;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
        }
        

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
            Player player = Main.player[Projectile.owner];
            if (index == 0)
                index = player.ownedProjectileCounts[ModContent.ProjectileType<UltimateLeader2>()] + 1;
            Projectile.tileCollide = false;
            theta = NewDawnPlayer.Get(player).theta + 3.14f / 4 * index;
            float mag = 48 + index * 16;
            Vector2 rot = Projectile.position;
            rot.X = (float)Math.Cos(theta) * mag;
            rot.Y = (float)Math.Sin(theta) * mag;
            Vector2 targetPos = Main.MouseWorld + rot;
            Vector2 direction = targetPos - Projectile.Center;
            float dist = Vector2.Distance(targetPos, Projectile.Center);
            tVel = dist / 15;
            if (vMag < vMax && vMag < tVel)
            {
                vMag = tVel;
            }

            if (vMag > tVel)
            {
                vMag = tVel;
            }

            if (dist != 0)
            {
                Projectile.velocity = Projectile.DirectionTo(targetPos) * vMag;
            }
                
            Projectile.netUpdate = true;

            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.dead)
            {
                modPlayer.UltraBoisSummon = false;
            }
            if (modPlayer.UltraBoisSummon)
            {
                Projectile.timeLeft = 2;
            }

            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height / 2, ModContent.DustType<White>());
                Main.dust[dust].velocity.Y -= 1.2f;
            }
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }
    }
}