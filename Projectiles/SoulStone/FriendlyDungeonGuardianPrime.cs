using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.SoulStone
{
    public class FriendlyDungeonGuardianPrime : ModProjectile
    {
        Vector2 targetPos;
        int counter = 0;

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
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
                if (npc.life < 1)
                    Projectile.ai[0] = 0;
                else
                    HomeIn(npc);
            }
            else
                Wander();

            if (!Main.player[Projectile.owner].active)
                Projectile.Kill();

            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get(Main.player[Projectile.owner]);

            if (!NewDawnPlayer.Focus.IsCapacity() || !NewDawnPlayer.SkeletronPrimeEffect)
                Projectile.Kill();
        }

        public void CheckActive()
        {
            if (Main.player[Projectile.owner].statLife <= (Main.player[Projectile.owner].statLifeMax2 / 2) + 1)
                Projectile.timeLeft = 4;
        }

        public void GetTarget()
        {
            float dist = 500;

            foreach (NPC npc in Main.npc)
            {
                if (npc.damage > 0 && npc.type != NPCID.TargetDummy && !npc.townNPC)
                {
                    if (npc.Distance(Projectile.Center) < dist)
                    {
                        dist = npc.Distance(Projectile.Center);
                        Projectile.ai[0] = npc.whoAmI;
                    }
                    if (npc.Distance(Main.player[Projectile.owner].Center) < dist / 2)
                    {
                        dist = npc.Distance(Main.player[Projectile.owner].Center);
                        Projectile.ai[0] = npc.whoAmI;
                    }
                }
            }
            if (Projectile.Distance(Main.player[Projectile.owner].Center) > 800)
            {
                Projectile.ai[0] = 0;
                Wander();
            }
        }

        public void HomeIn(NPC npc)
        {
            Projectile.rotation += .08f;
            Projectile.velocity = Projectile.DirectionTo(npc.Center) * 6;
            counter = 0;
            if (!npc.active || npc.life < 1 || npc.type == NPCID.TargetDummy)
                Projectile.ai[0] = 0;
        }

        public void Wander()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += .06f;
            if (counter <= 0)
            {
                counter = Main.rand.Next(60, 120);
                float wanderTheta = Main.rand.NextFloat() * 2 * (float)Math.PI;
                float mag = (Main.rand.NextFloat() * 60) + 60;
                targetPos.X = (float)Math.Cos(wanderTheta) * mag;
                targetPos.Y = (float)Math.Sin(wanderTheta) * mag / 2;
            }
            counter--;

            float dist = Vector2.Distance(player.Center + targetPos, Projectile.position);
            if (dist >= 4)
            {
                float tVel = dist / 25;
                if (tVel > 12)
                    tVel = 12;
                Projectile.velocity = Projectile.DirectionTo(Main.player[Projectile.owner].Center + targetPos) * tVel;
            }
        }
    }
}