using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.SoulStone
{
    public class FriendlyTrueEyeProj : ModProjectile
    {
        float theta = 0;
        int cooldown = 0;
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
            CheckActive();
            GetTarget();

            if (Projectile.ai[0] != 0)
            {
                NPC npc = Main.npc[(int)Projectile.ai[0]];
                if (!npc.active)
                    Projectile.ai[0] = 0;
                else
                    Shoot(npc);
            }
            else
            {
                if (Projectile.velocity.X < 0)
                    Projectile.spriteDirection = -1;
                else
                    Projectile.spriteDirection = 1;
            }

            Wander();

            if (!Main.player[Projectile.owner].active)
                Projectile.Kill();

        }

        public void CheckActive()
        {
            if (Main.player[Projectile.owner].statLife <= (Main.player[Projectile.owner].statLifeMax2 / 2) + 1)
                Projectile.timeLeft = 4;
        }

        public void GetTarget()
        {
            float dist = 700;

            foreach (NPC npc in Main.npc)
            {
                if (npc.damage > 0)
                {
                    if (npc.Distance(Projectile.Center) < dist)
                    {
                        dist = npc.Distance(Projectile.Center);
                        Projectile.ai[0] = npc.whoAmI;
                        if (npc.position.X < Projectile.position.X)
                            Projectile.spriteDirection = -1;
                        else
                            Projectile.spriteDirection = 1;
                    }
                    if (npc.Distance(Main.player[Projectile.owner].Center) < dist / 2)
                    {
                        dist = npc.Distance(Main.player[Projectile.owner].Center);
                        Projectile.ai[0] = npc.whoAmI;
                        if (npc.position.X < Projectile.position.X)
                            Projectile.spriteDirection = -1;
                        else
                            Projectile.spriteDirection = 1;
                    }
                }
            }
            if (!Main.npc[(int)Projectile.ai[0]].active)
                Projectile.ai[0] = 0;
        }

        public void Shoot(NPC npc)
        {
            if (counter <= 0)
            {
                counter = Main.rand.Next(100, 160);
                SoundEngine.PlaySound(SoundID.Item33, Projectile.position);
                if (Main.myPlayer == Projectile.owner)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.position, new Vector2(Projectile.DirectionTo(npc.Center).X * 6, Projectile.DirectionTo(npc.Center).Y * 6), ProjectileID.MagnetSphereBall, Projectile.damage, 4, Projectile.owner);
            }
            else
                counter--;
        }

        public void Wander()
        {
            Player player = Main.player[Projectile.owner];
            theta += (float)Math.PI / 60;
            float mag = 120;
            targetPos.X = (float)Math.Cos(theta) * mag;
            targetPos.Y = (float)Math.Sin(theta) * mag;

            float dist = Vector2.Distance(player.Center + targetPos, Projectile.position);
            if (dist != 0)
            {
                float tVel = dist / 15;
                Projectile.velocity = Projectile.DirectionTo(Main.player[Projectile.owner].Center + targetPos) * tVel;
            }
        }
    }
}