using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.SoulStone
{
    public class FriendlyProbeProj : ModProjectile
    {
        float theta = 0;
        Vector2 targetPos;
        int counter = 0;

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 800;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            GetTarget();
            if (Projectile.ai[0] != 0)
            {
                Move();
                NPC npc = Main.npc[(int)Projectile.ai[0]];
                if (!npc.active)
                    Projectile.ai[0] = 0;
                else
                    Shoot(npc);
            }
            else
                Wander();
        }

        public void GetTarget()
        {
            float dist = 700;

            foreach (NPC npc in Main.npc)
            {
                if (npc.damage > 0)
                {
                    if (npc.Distance(Projectile.Center) < dist && npc.life > 0)
                    {
                        dist = npc.Distance(Projectile.Center);
                        Projectile.ai[0] = npc.whoAmI;
                    }
                    if (npc.Distance(Main.player[Projectile.owner].Center) < dist / 2 && npc.life > 0)
                    {
                        dist = npc.Distance(Main.player[Projectile.owner].Center);
                        Projectile.ai[0] = npc.whoAmI;
                    }
                }
            }
            if (!Main.npc[(int)Projectile.ai[0]].active || Main.npc[(int)Projectile.ai[0]].life < 1)
                Projectile.ai[0] = 0;
        }

        public void Shoot(NPC npc)
        {
            if (counter <= 0)
            {
                counter = Main.rand.Next(100, 160);
                SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position, new Vector2(Projectile.DirectionTo(npc.Center).X * 12, Projectile.DirectionTo(npc.Center).Y * 12), ModContent.ProjectileType<FriendlyProbeLaserProj>(), Projectile.damage, 4, Projectile.owner);
            }
            else
                counter--;

            Projectile.rotation = Projectile.DirectionTo(Main.npc[(int)Projectile.ai[0]].Center).ToRotation();
        }

        public void Move()
        {
            NPC npc = Main.npc[(int)Projectile.ai[0]];

            theta += (float)Math.PI / 120;
            targetPos.X = (float)Math.Cos(theta) * 80;
            targetPos.Y = (float)Math.Sin(theta) * 80;

            float dist = Vector2.Distance(npc.Center + targetPos, Projectile.position);
            if (dist != 0)
            {
                float tVel = dist / 15;
                Projectile.velocity = Projectile.DirectionTo(npc.Center + targetPos) * tVel;
            }
        }

        public void Wander()
        {
            Player player = Main.player[Projectile.owner];
            theta += (float)Math.PI / 120;
            targetPos.X = (float)Math.Cos(theta) * 80;
            targetPos.Y = (float)Math.Sin(theta) * 80;

            float dist = Vector2.Distance(player.Center + targetPos, Projectile.position);
            if (dist != 0)
            {
                float tVel = dist / 15;
                Projectile.velocity = Projectile.DirectionTo(player.Center + targetPos) * tVel;
            }
        }
    }
}