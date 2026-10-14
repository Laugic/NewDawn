using System;
using NewDawn.Dusts;
using NewDawn.NPCs.RockTwins;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class GaiaConjuration : PrimaryConjurationProjectile
    {
        public Color colorType;
        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0;
        public float vMag = 0;
        float theta = 0;
        int index = 0;
        public override void SetDefaults()
        {
            index = 0;
            theta = 0;
            vMax = 28;
            vAccel = .1f;
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 12 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 6;
        }

        public override void AI()
        {
            Visuals();
            Movement();
            Effects();
        }

        private void Effects()
        {

        }

        private void Movement()
        {
            Player player = Main.player[Projectile.owner];

            if (Projectile.ai[0] == 0)
                Projectile.ai[0] = player.ownedProjectileCounts[ModContent.ProjectileType<GaiaConjuration>()];

            theta += (float)(Math.PI / 40);
            float mag = 48 + Projectile.ai[0] * 2;
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

        private void Visuals()
        {
            Projectile.frame = (int)(Projectile.ai[1]);

            if (Projectile.ai[1] == 0) { colorType = new Color(255, 0, 0); }
            if (Projectile.ai[1] == 1) { colorType = new Color(255, 226, 0); }
            if (Projectile.ai[1] == 2) { colorType = new Color(8, 255, 0); }
            if (Projectile.ai[1] == 3) { colorType = new Color(0, 217, 255); }
            if (Projectile.ai[1] == 4) { colorType = new Color(209, 0, 255); }
            if (Projectile.ai[1] == 5) { colorType = new Color(255, 255, 255); }

            if (Projectile.timeLeft < 80)
            {
                Projectile.alpha += 2;
            }
            if (Projectile.timeLeft < 2 && Projectile.ai[1] == 0)
            {
                Burst();
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].owner == Projectile.owner && Main.projectile[i].type == ModContent.ProjectileType<GaiaConjuration>() && Main.projectile[i].ai[0] > Projectile.ai[0])
                    Main.projectile[i].ai[0]--;
            }

            base.OnKill(timeLeft);
        }

        public void Burst()
        {
            if(Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < 12; i++)
                {
                    theta = (float)(Math.PI * Main.rand.NextDouble() * 2);
                    Vector2 newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                    newVel *= 10;
                    int id = Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, newVel.X, newVel.Y, ModContent.ProjectileType<GemShard>(), Projectile.damage, 3, Main.myPlayer);
                    Main.projectile[id].ai[1] = 3;
                }
            }
        }
    }
}