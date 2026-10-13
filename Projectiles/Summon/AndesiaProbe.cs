using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using NewDawn.Dusts;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Summon
{

    public class AndesiaProbe : HoverShooter
    {
        public int vMax = 0;
        public float vAccel = 0;
        public float tVel = 0; //Target Velocity
        public float vMag = 0;
        public int index = 0;
        public float theta = 0f;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            theta = 0f;
            index = 0;
            vMax = 14;
            vAccel = .1f;
            Projectile.netImportant = true;
            Projectile.width = 24;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.minionSlots = .5f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            inertia = 12f;
            shoot = ModContent.ProjectileType<AndeshiardOrb>();
            shootCool = 60f;
            shootSpeed = 18f;
        }

        public override void CheckActive()
        {
            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.dead)
            {
                modPlayer.RockTwinsSummon = false;
            }
            if (modPlayer.RockTwinsSummon)
            {
                Projectile.timeLeft = 2;
            }
        }

        public override void CreateDust()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            if (Projectile.ai[0] == 0f)
            {
                if (Main.rand.Next(5) == 0)
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height / 2, ModContent.DustType<Blue>());
                    Main.dust[dust].velocity.Y -= 1.2f;
                }
            }
            else
            {
                if (Main.rand.Next(3) == 0)
                {
                    Vector2 dustVel = Projectile.velocity;
                    if (dustVel != Vector2.Zero)
                    {
                        dustVel.Normalize();
                    }
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Blue>());
                    Main.dust[dust].velocity -= 1.2f * dustVel;
                }
            }
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }

        public override void Behavior()
        {
            Player player = Main.player[Projectile.owner];
            if (index == 0)
                index = player.ownedProjectileCounts[ModContent.ProjectileType<AndesiaProbe>()];
            float spacing = (float)Projectile.width * spacingMult;
            Projectile.tileCollide = false;
            theta += (float)(Math.PI / 40);
            float mag = 48 + index * 16;
            Vector2 rot = Projectile.position;
            rot.X = (float)Math.Cos(theta) * mag;
            rot.Y = (float)Math.Sin(theta) * mag;
            Vector2 targetPos = player.Center + rot;
            Vector2 direction = targetPos - Projectile.Center;
            float dist = Vector2.Distance(targetPos, Projectile.Center);
            tVel = dist / 15;
            if (vMag < vMax && vMag < tVel)
            {
                vMag += vAccel;
            }

            if (vMag > tVel)
            {
                vMag = tVel;
            }

            if (dist != 0)
            {
                Projectile.velocity = Projectile.DirectionTo(targetPos) * vMag;
            }
                
            
            targetPos = Projectile.position;
            float targetDist = viewDist;
            bool target = false;
            Projectile.tileCollide = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                {
                    targetDist = Vector2.Distance(Projectile.Center, targetPos);
                    targetPos = npc.Center;
                    target = true;
                }
            }
            else for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    if (npc.CanBeChasedBy(this, false))
                    {
                        float distance = Vector2.Distance(npc.Center, Projectile.Center);
                        if ((distance < targetDist || !target) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                        {
                            targetDist = distance;
                            targetPos = npc.Center;
                            target = true;
                        }
                    }
                }

            if (Vector2.Distance(player.Center, Projectile.Center) > (target ? 1000f : 500f))
            {
                Projectile.ai[0] = 1f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
            }
            if (target && Projectile.ai[0] == 0f)
            {
                Vector2 tPos = targetPos + rot;
                direction = tPos - Projectile.Center;
                dist = Vector2.Distance(tPos, Projectile.Center);
                tVel = dist / 15;
                if (vMag < vMax && vMag < tVel)
                {
                    vMag += vAccel;
                }
                
                if (vMag > tVel)
                {
                    vMag = tVel;
                }

                if (dist != 0)
                {
                    Projectile.velocity = Projectile.DirectionTo(tPos) * vMag;
                }
            }
            else
            {
                if (!Collision.CanHitLine(Projectile.Center, 1, 1, player.Center, 1, 1))
                {
                    Projectile.ai[0] = 1f;
                }
                float speed = 6f;
                if (Projectile.ai[0] == 1f)
                {
                    speed = 15f;
                }
                Vector2 center = Projectile.Center;
                direction = player.Center - center;
                Projectile.ai[1] = 3600f;
                Projectile.netUpdate = true;
                int num = 1;
                for (int k = 0; k < Projectile.whoAmI; k++)
                {
                    if (Main.projectile[k].active && Main.projectile[k].owner == Projectile.owner && Main.projectile[k].type == Projectile.type)
                    {
                        num++;
                    }
                }
                direction.X -= (float)((10 + num * 40) * player.direction);
                direction.Y -= 70f;
                float distanceTo = direction.Length();
                if (distanceTo > 200f && speed < 9f)
                {
                    speed = 9f;
                }
                if (distanceTo < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (distanceTo > 2000f)
                {
                    Projectile.Center = player.Center;
                }
                if (distanceTo > 48f)
                {
                    direction.Normalize();
                    direction *= speed;
                    float temp = inertia / 2f;
                    //projectile.velocity = (projectile.velocity * temp + direction) / (temp + 1);
                }
                else
                {
                    //projectile.direction = Main.player[projectile.owner].direction;
                    //projectile.velocity *= (float)Math.Pow(0.9, 40.0 / inertia);
                }
            }
            //projectile.rotation = projectile.velocity.X * 0.05f;
            SelectFrame();
            CreateDust();
            /*if (projectile.velocity.X > 0f)
            {
                projectile.spriteDirection = (projectile.direction = -1);
            }
            else if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = (projectile.direction = 1);
            }*/
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += 1f;
                if (Main.rand.Next(3) == 0)
                {
                    Projectile.ai[1] += 1f;
                }
            }
            if (Projectile.ai[1] > shootCool)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 0f)
            {
                if (target)
                {
                    /*if ((targetPos - projectile.Center).X > 0f)
                    {
                        projectile.spriteDirection = (projectile.direction = -1);
                    }
                    else if ((targetPos - projectile.Center).X < 0f)
                    {
                        projectile.spriteDirection = (projectile.direction = 1);
                    }*/
                    if (Projectile.ai[1] == 0f)
                    {
                        Projectile.ai[1] = 1f;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            Vector2 shootVel = targetPos - Projectile.Center;
                            if (shootVel == Vector2.Zero)
                            {
                                shootVel = new Vector2(0f, 1f);
                            }
                            shootVel.Normalize();
                            shootVel *= shootSpeed;
                            int proj = Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, shootVel.X, shootVel.Y, shoot, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                            Main.projectile[proj].timeLeft = 300;
                            Main.projectile[proj].netUpdate = true;
                            Projectile.rotation = (float)Math.Atan2((double)shootVel.Y, (double)shootVel.X);
                            Projectile.netUpdate = true;
                        }
                    }
                }
            }
            Projectile.netUpdate = true;
        }
    }
}