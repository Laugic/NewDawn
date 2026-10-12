using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Summon
{
    public class ArcticHydraHead : ModProjectile
    {
        public float dust = 0f;
        bool justSpawned = false;
        Vector2 offSet;
        float mag = 20;
        float range = 200;
        Vector2 targetPos;
        bool targetFound = false;
        int npcTarget = -1;
        float npcDistance = 8000;
        float theta = 0;
        int delay = 0;
        int index = 0;
        float vMag = 0;
        int shootDelay = 0;
        int mouthOpen = 0;
        public override void SetDefaults()
        {
            justSpawned = false;
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1;
            Projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Main.projFrames[Projectile.type] = 2;
        }

        private void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.dead)
            {
                modPlayer.ArcticHydraSummon = false;
            }
            if (modPlayer.ArcticHydraSummon)
            {
                Projectile.timeLeft = 2;
            }
            if (!justSpawned)
            {
                justSpawned = true;
                GetMinionSlots(player.ownedProjectileCounts[ModContent.ProjectileType<ArcticHydraHead>()]);
                range = 200 + 50 * player.ownedProjectileCounts[ModContent.ProjectileType<ArcticHydraHead>()];
                Projectile.damage += index * 20;
                if ((NewDawnPlayer.Get(Main.player[Projectile.owner]).Etherable > 2 || NewDawnWorld.downedEtheria) && NewDawnWorld.downedTrueEtheria)
                {
                    Projectile.damage += 20 * index;
                    range = range * 2;
                }
                targetPos.X = player.Center.X;
                targetPos.Y = player.Center.Y;
                theta = (float)(Main.rand.NextDouble() * Math.PI * 2);
                index = player.ownedProjectileCounts[ModContent.ProjectileType<ArcticHydraHead>()];
                offSet.X = 10 * index * (float)Math.Cos(Math.PI / 4 * index);
                if (index == 1)
                    offSet.X = 20;
                offSet.Y = -30 - (8 * index * Math.Abs((float)Math.Sin(Math.PI / 4 * index)));
            }
        }

        public override void AI()
        {
            CheckActive();
            GetTarget();
            Movement();
            if(npcTarget != -1)
                Shoot();
            GetDirection();
            GetFrame();
        }

        private void GetTarget()
        {
            targetFound = false;
            npcTarget = -1;
            npcDistance = 8000;
            foreach (NPC npc in Main.npc)
            {
                if (npc.damage > 0 && !npc.friendly)
                {
                    Vector2 newMove = npc.Center - Projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (Main.player[Projectile.owner].Distance(npc.Center) <= range && Projectile.Distance(npc.Center) < npcDistance)
                    {
                        npcTarget = npc.whoAmI;
                        //targetPos = npc.Center;
                        npcDistance = Projectile.Distance(npc.Center);
                    }
                }
            }
            if (npcTarget != -1)
            {
                if (!Main.npc[npcTarget].active)
                {
                    npcTarget = -1;
                    npcDistance = 8000;
                }
            }
        }

        private void Movement()
        {
            //if(npcTarget == -1 || index % 2 == 0)
            //{
                targetPos = Main.player[Projectile.owner].Center;
                targetPos += offSet;
            //}
            theta += (float)Math.PI / 60f;
            targetPos.Y = targetPos.Y + 8 * (float)Math.Sin(theta);
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            delay++;
            if (Main.myPlayer == Projectile.owner && delay > 10 && npcDistance != 8000)
            {
                float mag = 12f;

                float diffX = targetPos.X - Projectile.Center.X;
                float diffY = targetPos.Y - Projectile.Center.Y;
                float dist = (float)Math.Sqrt((double)(diffX * diffX + diffY * diffY));
                if(dist != 0)
                    dist = mag / dist;
                diffX *= dist;
                diffY *= dist;

                Projectile.velocity.X = (Projectile.velocity.X * 20f + diffX) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + diffY) / 21f;
                if (Math.Abs(Projectile.velocity.X) <= .2f && Math.Abs(diffX) <= .2f)
                    Projectile.velocity.X = 0;
                if (Math.Abs(Projectile.velocity.Y) <= .2f && Math.Abs(diffY) <= .2f)
                    Projectile.velocity.Y = 0;
            }
            else if (Main.myPlayer == Projectile.owner && delay > 10 && npcDistance == 8000)
            {
                Projectile.velocity = Projectile.DirectionTo(targetPos) * (Projectile.Distance(targetPos) / 8);
            }
            GetDust();
        }

        private void GetMinionSlots(int numHeads)
        {
            switch((int)numHeads / 2)
            {
                case 0:
                    Projectile.minionSlots = 1;
                    break;
                case 1:
                    Projectile.minionSlots = .5f;
                    break;
                case 2:
                    Projectile.minionSlots = .33f;
                    break;
                default:
                    Projectile.minionSlots = .25f;
                    break;
            }
        }

        private void GetDust()
        {
            if (Main.rand.Next(8) == 0)
                Dust.NewDust(Projectile.Center, 0, 0, ModContent.DustType<ArcticHydra>());
        }

        private void GetFrame()
        {
            if (mouthOpen > 0)
            {
                mouthOpen--;
                Projectile.frame = 1;
            }
            else
                Projectile.frame = 0;
        }

        private void Shoot()
        {
            shootDelay++;
            Vector2 vector = Main.npc[npcTarget].Center - Projectile.Center;
            float speed = 16f;
            float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (mag > speed && mag != 0)
            {
                mag = speed / mag;
            }
            vector *= mag * 3;
            if (shootDelay >= 60 && Main.myPlayer == Projectile.owner && npcDistance != 8000 && npcTarget != -1)
            {
                shootDelay = 0;
                mouthOpen = 30;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, vector.X, vector.Y, ModContent.ProjectileType<BysmalBlast2>(), Projectile.damage, 3f, Projectile.owner);
            }
        }

        private void GetDirection()
        {
            //if (npcDistance == 8000)
            {
                if (Main.player[Projectile.owner].velocity.X > 0f)
                {
                    Projectile.spriteDirection = -1;
                }
                if (Main.player[Projectile.owner].velocity.X < 0f)
                {
                    Projectile.spriteDirection = 1;
                }
            }
            /*else
            {
                if (projectile.velocity.X > 0f)
                {
                    projectile.spriteDirection = -1;
                }
                if (projectile.velocity.X < 0f)
                {
                    projectile.spriteDirection = 1;
                }
            }*/
        }
    }
}