using System;
using NewDawn.Buffs;
using NewDawn.Dusts;
using NewDawn.Items.Loot;
using NewDawn.Items.Placeable;
using NewDawn.NPCs.Slybertron;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Bosses
{
    [AutoloadBossHead]
    public class TheAnnihilator : ModNPC
    {
        int Phase { get; set; }
        int FrameDelay { get; set; } = 0;
        int Delay { get; set; } = 0;
        bool Attacking { get; set; } = false;
        public static bool on = false;
        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            // DisplayName.SetDefault("The Annihilator");
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.width = 200;
            NPC.height = 240;
            NPC.damage = 80;
            NPC.defense = 32;
            NPC.lifeMax = 30000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Annihilator");
            bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<AnnihilatorTreasureBag>();
            NPC.buffImmune[ModContent.BuffType<Steamy>()] = true;
            Phase = 0;
            FrameDelay = 0;
            Delay = 0;
            Attacking = false;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 60000 + numPlayers * 30000;
            NPC.damage = 120;
        }

        public override void AI()
        {
            Calculations();
            PhaseChecks();
            if(Attacking)
                Attacks();
            Movement();
            Visuals();
        }

        private void Visuals()
        {
            Lighting.AddLight(NPC.position, .5f, .6f, .8f);
        }

        private void Movement()
        {
            if (Main.player[(int)NPC.target].statLife <= 0 || !Main.player[(int)NPC.target].active)
            {
                DespawnAI();
                return;
            }
            if ((!Attacking || NPC.life < NPC.lifeMax * .33 || (NPC.life < NPC.lifeMax * .5 && Main.expertMode)) && (NPC.ai[2] >= 0 && NPC.ai[2] < (15 - Phase) * 60 - 30))
                FollowAI();
            else
                Slow();
            if (NPC.life < NPC.lifeMax * .75)
                Teleportation();
        }

        private void Teleportation()
        {
            NPC.ai[2]++;
            if (NPC.ai[2] == (15 - Phase) * 60 - 30)
                SoundEngine.PlaySound(SoundID.Item28, NPC.position);
            if (NPC.ai[2] > (15 - Phase) * 60)
            {
                NPC.ai[2] = -30;
                NPC.velocity *= 0;
                Vector2 warp = (Main.player[NPC.target].Center - NPC.Center).RotatedByRandom(MathHelper.ToRadians(90));
                float mag = warp.Length();
                warp.Normalize();
                warp *= Math.Max(mag, 600);
                NPC.Teleport(Main.player[NPC.target].Center + warp, 1);
                SoundEngine.PlaySound(SoundID.Item8, NPC.position);
            }
        }

        private void Slow()
        {
            NPC.velocity *= .95f;
        }

        private void FollowAI()
        {
            Vector2 TargetPos = Main.player[NPC.target].Center;
            MoveTowardsAtSpeed(TargetPos, 3 + Phase + (Main.expertMode ? 2 : 0));
        }

        private void MoveTowardsAtSpeed(Vector2 targetPos, float mag)
        {
            Vector2 newVel = Vector2.Normalize(targetPos - NPC.Center);
            newVel *= Math.Min(mag, NPC.velocity.Length() + .3f);
            NPC.velocity = newVel;
        }

        private void Attacks()
        {
            Delay++;
            float numBalls = 0;
            double thetaInit = 0;
            if(Delay == 1 * 60)
            {
                var attack = Main.rand.Next(4);
                switch (attack)
                {
                    case 1:
                        SoundEngine.PlaySound(SoundID.Item33, NPC.position);
                        numBalls = 2 + ((NPC.life < NPC.lifeMax / 2)?2:0);
                        thetaInit = 0;
                        for (int i = 0; i < numBalls; i++)
                        {
                            float mag = 5;
                            if (Main.netMode != 1)
                                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y - 80, mag * (float)Math.Cos(thetaInit + (Math.PI * 2) * (i / numBalls)), mag * (float)Math.Sin(thetaInit + (Math.PI * 2) * (i / numBalls)),
                                    ModContent.ProjectileType<XOut>(), (int)(NPC.damage / 4), 3);
                        }
                        break;
                    case 2:
                        SoundEngine.PlaySound(SoundID.Item33, NPC.position);
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y + 60, 0, 8, ModContent.ProjectileType<SteamyShadow>(), NPC.damage / 3, 3f, NPC.target);
                        /*numBalls = 10 + Main.rand.Next(6);
                        thetaInit = Math.PI;
                        for (int i = 0; i < numBalls; i++)
                        {
                            float mag = 12 + Main.rand.NextFloat() * 8;
                            if (Main.netMode != 1)
                                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, npc.Center.Y, mag * (float)Math.Cos(thetaInit + (Math.PI) * (i / numBalls)), mag * (float)Math.Sin(thetaInit + (Math.PI) * (i / numBalls)),
                                    ModContent.ProjectileType<Electroshock>(), (int)(npc.damage / 4), 3, npc.target, .3f);
                        }*/
                        break;
                    default:
                        SoundEngine.PlaySound(SoundID.Item33, NPC.position);
                        numBalls = 12 + ((NPC.life < NPC.lifeMax / 2)?8:0) +  Main.rand.Next(8);
                        thetaInit = Math.PI * 2 * Main.rand.NextDouble();
                        for (int i = 0; i < numBalls; i++)
                        {
                            float mag = 6 + Main.rand.NextFloat() * 4;
                            if (Main.netMode != 1)
                                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, mag * (float)Math.Cos(thetaInit + (Math.PI * 2) * (i / numBalls)), mag * (float)Math.Sin(thetaInit + (Math.PI * 2) * (i / numBalls)),
                                    ModContent.ProjectileType<Electroshock>(), (int)(NPC.damage / 4), 3);
                        }
                        break;
                }
            }
            if (Delay > 2 * 60)
                Attacking = false;
        }

        private void PhaseChecks()
        {
            if(Phase == 0)
            {
                Phase = 1;
                PhaseChanges(Phase);
            }
            if (Phase == 1 && NPC.life < NPC.lifeMax * .75)
            {
                Phase = 2;
                PhaseChanges(Phase);
            }
            if (Phase == 2 && NPC.life < NPC.lifeMax * .5)
            {
                Phase = 3;
                PhaseChanges(Phase);
            }
            if (Phase == 3 && NPC.life < NPC.lifeMax * .25)
            {
                Phase = 4;
                PhaseChanges(Phase);
            }
            if (Phase == 4 && NPC.life < NPC.lifeMax * .1 && Main.expertMode)
            {
                Phase = 5;
                PhaseChanges(Phase);
            }
        }

        private void DespawnAI()
        {
            NPC.TargetClosest();
            if (Main.player[(int)NPC.target].statLife <= 0 || !Main.player[(int)NPC.target].active)
                NPC.position.Y += 8;
            else
                FollowAI();
        }
        private void PhaseChanges(int phase)
        {
            SoundEngine.PlaySound(SoundID.Roar, NPC.position);
            switch (phase)
            {
                case 1:
                    for (int i = 0; i < 4; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkDefender>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkDefender>()));
                    for (int i = 0; i < 8; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkZapper>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkZapper>()));
                    break;
                case 2:
                    for (int i = 0; i < 4; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkDefender>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkDefender>()));
                    for (int i = 0; i < 8; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkZapper>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkZapper>()));
                    break;
                case 3:
                    for (int i = 0; i < 4; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkDefender>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkDefender>()));
                    for (int i = 0; i < 8; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkZapper>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkZapper>()));
                    break;
                case 4:
                    for (int i = 0; i < 4; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkDefender>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkDefender>()));
                    for (int i = 0; i < 8; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkZapper>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkZapper>()));
                    break;
                case 5:
                    for (int i = 0; i < 4; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkDefender>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkDefender>()));
                    for (int i = 0; i < 8; i++)
                        NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SteampunkZapper>(), 0, NPC.whoAmI, NPC.CountNPCS(ModContent.NPCType<SteampunkZapper>()));
                    break;
                default:

                    break;
            }
        }

        private void Calculations()
        {
            NPC.ai[0] += (float)(Math.PI / 60);
            NPC.ai[1] += (float)(Math.PI / 80);
            if(!Attacking)
                Delay++;
            if (Delay > 4 * 60)
            {
                Attacking = true;
                Delay = 0;
            }
        }

        /*
        public override void AI()
        {
            npc.rotation = 0;

            if (npc.active)
                on = true;
            else
                on = false;
            if (npc.velocity.X > 12) npc.velocity.X = 12;
            if (npc.velocity.Y > 12) npc.velocity.Y = 12;
            if (npc.life < npc.lifeMax && spawn < 1 && Main.netMode != 1)
            {
                poof = true;
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 1;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>(), 0, npc.whoAmI, NPC.CountNPCS(ModContent.NPCType<MechanicalCreeper>()));
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>(), 0, npc.whoAmI, NPC.CountNPCS(ModContent.NPCType<MechanicalCreeper>()));
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .9 && spawn < 2 && Main.netMode != 1)
            {
                poof = true;
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 2;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .8 && spawn < 3 && Main.netMode != 1)
            {
                poof = true;
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 3;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .7 && spawn < 4 && Main.netMode != 1)
            {
                poof = true;
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 4;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .6 && spawn < 5 && Main.netMode != 1)
            {
                poof = true;
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 5;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .5 && spawn < 6 && Main.netMode != 1)
            {
                poof = true;
                if (Main.expertMode)
                {
                    npc.velocity *= 3;
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                }

                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 6;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .4 && spawn < 7 && Main.netMode != 1)
            {
                poof = true;
                if (Main.expertMode)
                {
                    npc.velocity *= 3;
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                }
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 7;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .3 && spawn < 8 && Main.netMode != 1)
            {
                poof = true;
                if (Main.expertMode)
                {
                    npc.velocity *= 3;
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                }
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 8;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .2 && spawn < 9 && Main.netMode != 1)
            {
                poof = true;
                if (Main.expertMode)
                {
                    npc.velocity *= 2;
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                }
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 9;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .12 && spawn < 10 && Main.netMode != 1)
            {
                poof = true;
                if (Main.expertMode)
                {
                    spawn = 10;
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalShelly>());
                }
                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                spawn = 10;
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
            }
            if (npc.life < npc.lifeMax * .02 && spawn < 11 && Main.expertMode && Main.netMode != 1)
            {
                if (Main.expertMode)
                {
                    poof = true;
                    spawn = 11;
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalSlimer>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCreeper>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalCrawler>());
                    NPC.NewNPC((int)npc.position.X + rnd.Next(0, npc.width), (int)npc.position.Y + rnd.Next(0, npc.height), ModContent.NPCType<MechanicalMimic>());
                }
            }

            if (poof)
            {
                poof = false;

                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Steam>(), 0f, 0f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Steam>(), 0f, 0f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Steam>(), 0f, 0f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Steam>(), 0f, 0f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Steam>(), 0f, 0f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Steam>(), 0f, 0f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Steam>(), 0f, 0f);
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Steam>(), 0f, 0f);
            }

            delay++;
            int phase = 0;
            if (npc.life < npc.lifeMax * .5)
                phase = 1;
            if (delay >= 4)
            {
                delay = 0;
                frame++;
                if (frame > phase * 4 + 3)
                    frame = phase * 4;
            }
            npc.frame.Y = fHeight * frame;
        }
        */

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.expertMode)
                player.AddBuff(ModContent.BuffType<Steamy>(), 2 * 60 + Main.rand.Next(60), true);
        }

        public override void OnKill()
        {

            NewDawnPlayer modPlayer = NewDawnPlayer.Get();
            if (NewDawnWorld.downedEtheria)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<CogOfEtheria>(), 1);
            }
            if (!Main.expertMode)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SteamBar>(), Main.rand.Next(15, 30));
               
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SoulOfThought>(), Main.rand.Next(20, 40));
            }

            if (Main.expertMode)
            {
                NPC.DropBossBags();
            }
            if (Main.rand.Next(10) == 0)
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<AnnihilatorTrophy>(), 1);
            NewDawnWorld.downedAnnihilator = true;

        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 499;
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = 0;
            FrameDelay++;
            if (FrameDelay >= 16)
                FrameDelay = 0;
            NPC.frame.Y = frameHeight * (((NPC.life < NPC.lifeMax * .5)?4:0) + FrameDelay / 4);
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Microsoft.Xna.Framework.Color color9 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
            float num66 = 0f;
            Vector2 vector11 = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Microsoft.Xna.Framework.Rectangle frame6 = NPC.frame;
            Microsoft.Xna.Framework.Color alpha15 = NPC.GetAlpha(color9);
            float alpha = 1.25f * (1f - (float)NPC.life / (float)NPC.lifeMax);
            alpha *= alpha;
            alpha = Math.Min(alpha, 1);
            alpha15.R = (byte)((float)alpha15.R * alpha);
            alpha15.G = (byte)((float)alpha15.G * alpha);
            alpha15.B = (byte)((float)alpha15.B * alpha);
            alpha15.A = (byte)((float)alpha15.A * alpha);
            for (int num213 = 0; num213 < 4; num213++)
            {
                Vector2 position9 = NPC.position;
                float num214 = Math.Abs(NPC.Center.X - Main.player[Main.myPlayer].Center.X);
                float num215 = Math.Abs(NPC.Center.Y - Main.player[Main.myPlayer].Center.Y);
                if (num213 == 0 || num213 == 2)
                {
                    position9.X = Main.player[Main.myPlayer].Center.X + num214;
                }
                else
                {
                    position9.X = Main.player[Main.myPlayer].Center.X - num214;
                }
                position9.X -= (float)(NPC.width / 2);
                if (num213 == 0 || num213 == 1)
                {
                    position9.Y = Main.player[Main.myPlayer].Center.Y + num215;
                }
                else
                {
                    position9.Y = Main.player[Main.myPlayer].Center.Y - num215;
                }
                position9.Y -= (float)(NPC.height / 2);
                Main.spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, new Vector2(position9.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[NPC.type].Value.Width * NPC.scale / 2f + vector11.X * NPC.scale, position9.Y - Main.screenPosition.Y + (float)NPC.height - (float)TextureAssets.Npc[NPC.type].Value.Height * NPC.scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * NPC.scale + num66 + NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), alpha15, NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - (float)TextureAssets.Npc[NPC.type].Value.Width * NPC.scale / 2f + vector11.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - (float)TextureAssets.Npc[NPC.type].Value.Height * NPC.scale / (float)Main.npcFrameCount[NPC.type] + 4f + vector11.Y * NPC.scale + num66 + NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(frame6), NPC.GetAlpha(color9), NPC.rotation, vector11, NPC.scale, spriteEffects, 0f);
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 0f;
            return null;
        }
        /*
		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(npc.position, npc.width, npc.height, dustType);
				Dust dust = Main.dust[dustIndex];
				dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
				dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
				dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
			}
		}*/
    }
}
