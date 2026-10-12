using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class AnDioConjuration1 : PrimaryConjurationProjectile
    {

        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0;
        public float vMag = 0;
        public int delay = 0;
        float theta = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Energy Ball");
        }

        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 10 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            theta = 0;
            vMax = 28;
            vAccel = .1f;
            delay = 0;
        }


        public override void AI()
        {
            if(Main.rand.Next(4) == 0)
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Blue>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Projectile.tileCollide = false;

            Player player = Main.player[Projectile.owner];

            if (Projectile.ai[0] == 0)
                Projectile.ai[0] = player.ownedProjectileCounts[ModContent.ProjectileType<AnDioConjuration1>()];

            Projectile.tileCollide = false;
            theta += (float)(Math.PI / 40);
            float mag = 32 + Projectile.ai[0] * 4;
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

            CreateStalags();
        }

        private void CreateStalags()
        {
            delay++;
            Player player = Main.player[Projectile.owner];
            if (delay > 90)
            {
                if(Main.myPlayer == Projectile.owner)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X - 400 + Main.rand.Next(800), player.Center.Y - 500, 0, 0, ModContent.ProjectileType<AnDioStalactite>(), Projectile.damage, 5f, Projectile.owner);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X - 400 + Main.rand.Next(800), player.Center.Y + 500, 0, 0, ModContent.ProjectileType<AnDioStalagmite>(), Projectile.damage, 5f, Projectile.owner);
                }
                delay = 0;
            }
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);

            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].owner == Projectile.owner && Main.projectile[i].type == ModContent.ProjectileType<AnDioConjuration1>() && Main.projectile[i].ai[0] > Projectile.ai[0])
                    Main.projectile[i].ai[0]--;
            }
        }
    }
}