using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class HermesConjuration : PrimaryConjurationProjectile
    {
        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0;
        public float vMag = 0;
        float theta = 0;
        public override void SetDefaults()
        {
            theta = 0;
            vMax = 28;
            vAccel = .1f;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.timeLeft = 10 * 60;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 2;
        }

        public override void AI()
        {
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<HermesDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Projectile.tileCollide = false;


            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;

            Player player = Main.player[Projectile.owner];

            if(Projectile.ai[0] == 0)
                Projectile.ai[0] = player.ownedProjectileCounts[ModContent.ProjectileType<HermesConjuration>()];

            Projectile.tileCollide = false;
            theta += (float)(Math.PI / 40);
            float mag = 48 + Projectile.ai[0] * 8;
            Vector2 rot = Projectile.position;
            rot.X = (float)Math.Cos(theta) * mag;
            rot.Y = (float)Math.Sin(theta) * mag;
            Vector2 targetPos = player.Center + rot;
            Vector2 direction = targetPos - Projectile.Center;
            float dist = Vector2.Distance(targetPos, Projectile.Center);
            tVel = dist / 15;
            if (vMag < vMax && vMag < tVel)
                vMag += vAccel;

            if (vMag > tVel)
                vMag = tVel;

            if (dist != 0)
                Projectile.velocity = Projectile.DirectionTo(targetPos) * vMag;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].owner == Projectile.owner && Main.projectile[i].type == ModContent.ProjectileType<HermesConjuration>() && Main.projectile[i].ai[0] > Projectile.ai[0])
                    Main.projectile[i].ai[0]--;
            }
        }
    }
}