using System;
using NewDawn.Dusts;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
    [AutoloadBossHead]
    public class DuneSharkron : ModNPC
    {
        public bool Enraged { get; set; }
        private int AIPhase { get; set; }
        private int PrevAIPhase { get; set; }
        private int Frame { get; set; }
        private int JumpNum { get; set; }
        private Vector2 TargetPos;
        private bool Angry { get; set; }

        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            // DisplayName.SetDefault("Dune Sharkron");
        }

        public override void SetDefaults()
        {
            NPC.width = 150;
            NPC.height = 60;
            NPC.damage = 25;
            NPC.defense = 12;
            NPC.aiStyle = -1;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DuneSharkron");
            bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ModContent.ItemType<DuneSharkronTreasureBag>();
            NPC.scale = 1.25f;
            PrevAIPhase = AIPhase = 0;
            Angry = false;
            NPC.ai[1] = 0;
            NPC.ai[0] = -1;
            TargetPos = NPC.Center;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 3500 + numPlayers * 3000;
            NPC.damage = 50;
        }

        private void CheckEnrage()
        {
            Player player = Main.player[NPC.target];
            if (player.active && player.statLife > 0)
                Enraged = !player.ZoneDesert;
            else
                Enraged = true;

            NPC.netUpdate = true;
        }

        public override void AI()
        {
            CheckEnrage();
            HealthCheck();
            PickAI();
            Effects();
        }

        private void HealthCheck()
        {
            if (Main.player[(int)NPC.target].statLife <= 0 || !Main.player[(int)NPC.target].active)
            {
                AIPhase = 6;
                return;
            }
            if(NPC.life < NPC.lifeMax / 2 && !Angry)
            {
                Angry = true;
                SandnadoSpawn();
                SoundEngine.PlaySound(SoundID.Roar, NPC.position);
            }
        }

        private void Effects()
        {
            NPC.spriteDirection = -Math.Sign(NPC.velocity.X);
        }

        private void PickAI()
        {
            //Debug AI
            switch (AIPhase)
            {
                case 0:
                    InitializeAI();
                    break;
                case 1:
                    TripleJumpAI();
                    break;
                case 2:
                    JumpDashAI();
                    break;
                case 3:
                    LeapAI();
                    break;
                case 4:
                    TransitionAI();
                    break;
                default:
                    DespawnAI();
                    break;
            }
            //AIPhase = 4;
        }

        private void DespawnAI()
        {
            NPC.TargetClosest();
            if (Main.player[(int)NPC.target].statLife <= 0 || !Main.player[(int)NPC.target].active)
                NPC.position.Y += 8;
            else
                Transition();
        }

        private void TransitionAI()
        {
            if(NPC.ai[0] > Math.PI * 2 || NPC.ai[0] < 0)
            {
                NPC.ai[0] = 0;
                TargetPos = Main.player[NPC.target].Center;
            }
            NPC.ai[0] += (float)Math.PI / 180;
            var newPos = TargetPos;
            newPos.X += (float)Math.Cos(NPC.ai[0]) * 600;
            newPos.Y += 100;
            newPos.Y += (float)Math.Sin(NPC.ai[0] * 3) * 100;
            MoveTowards(newPos);
            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y * -NPC.spriteDirection, (double)NPC.velocity.X * -NPC.spriteDirection);
            NPC.ai[1]++;
            if (NPC.ai[1] > 6 * 60 || (NPC.ai[1] > 2 * 60 && Main.rand.Next(90) == 0))
            {
                NPC.ai[1] = 0;
                NPC.ai[0] = -1;
                switch (PrevAIPhase)
                {
                    case 1:
                        AIPhase = Main.rand.Next(2, 4);
                        break;
                    case 2:
                        AIPhase = 1 + 2 * Main.rand.Next(2);
                        break;
                    case 3:
                        AIPhase = Main.rand.Next(1, 3);
                        break;
                    default:
                        AIPhase = 1;
                        break;
                }
                if (Main.expertMode && Angry && Main.rand.Next(2) == 0)
                    SandnadoSpawn();
            }
        }

        private void LeapAI()
        {
            if(NPC.ai[1] == 0)
            {
                TargetPos = NPC.Center;
                TargetPos.Y = Main.player[NPC.target].Center.Y + 60;
            }
            if (NPC.ai[1] < 60)
            {
                MoveTowards(TargetPos);
                NPC.rotation = 0;
            }
            if (NPC.ai[1] == 60)
            {
                TargetPos = Main.player[NPC.target].Center;
                TargetPos.Y -= 40;
                NPC.velocity = NPC.Center - TargetPos;
                NPC.velocity.Normalize();
                NPC.velocity *= -16;
            }
            if(NPC.ai[1] > 60)
            {
                if (NPC.Distance(TargetPos) > 1600)
                    Slow();
                else
                {
                    NPC.velocity.Y = (float)Math.Min(8, NPC.velocity.Y + .05f);
                    if(NPC.position.Y < Main.player[NPC.target].Center.Y)
                        NPC.velocity.Y = (float)Math.Min(8, NPC.velocity.Y + .15f);
                }
                NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y * -NPC.spriteDirection, (double)NPC.velocity.X * -NPC.spriteDirection);
            }
            NPC.ai[1]++;
            if (NPC.ai[1] > 3 * 60)
                Transition();
        }

        private void JumpDashAI()
        {
            if(NPC.ai[1] == 0)
            {
                TargetPos = NPC.Center;
                TargetPos.Y = Main.player[NPC.target].Center.Y;
            }
            if(NPC.ai[1] < 60)
            {
                var dirToPlayer = NPC.Center - Main.player[NPC.target].Center;
                dirToPlayer.Normalize();
                NPC.rotation = -(float)Math.PI / 2;
                MoveTowardsSharp(TargetPos);
            }
            if (NPC.ai[1] == 60)
            {
                TargetPos = Main.player[NPC.target].Center;
                NPC.velocity = NPC.Center - TargetPos;
                NPC.velocity.Normalize();
                NPC.velocity *= -16;
                CrystalBurst((Angry ? 30 : 20), false, (Angry ? 6 : 3));
                if (Angry)
                    SandnadoSpawn();
            }
            if (NPC.ai[1] > 60)
            {
                if (NPC.Distance(TargetPos) > 1800)
                    Slow();
                NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y * -NPC.spriteDirection, (double)NPC.velocity.X * -NPC.spriteDirection);
            }
            NPC.ai[1]++;
            if (NPC.ai[1] > 2 * 60 + 30)
                Transition();
        }

        private void TripleJumpAI()
        {
            NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y * -NPC.spriteDirection, (double)NPC.velocity.X * -NPC.spriteDirection);
            if (NPC.ai[1] % 2 == 0)
            {
                TargetPos = Main.player[NPC.target].Center;
                TargetPos.Y += 240;
                MoveTowardsSharp(TargetPos);
                if (NPC.Distance(TargetPos) < 12 || Main.rand.Next(60) == 0)
                {
                    NPC.velocity.Y = -16;
                    NPC.velocity.X *= .9f;
                    if (NPC.ai[1] == 4)
                        SandnadoSpawn();
                    NPC.ai[1]++;
                }
            }
            else
            {
                NPC.velocity.Y = Math.Min(NPC.velocity.Y + .2f, 16);
                if(NPC.velocity.Y > 0 && NPC.ai[0] != NPC.ai[1])
                {
                    NPC.ai[0] = NPC.ai[1];
                    CrystalBurst((Angry ? 30 : 20), false, (Angry ? 6 : 3));
                }
                if(NPC.position.Y > Main.player[NPC.target].Center.Y && NPC.velocity.Y > 0)
                {
                    NPC.ai[1]++;
                    if (NPC.ai[1] > 5)
                        Transition();
                }
            }
        }

        private void SandnadoSpawn()
        {
            if(Main.netMode != 1)
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(0, -4), ModContent.ProjectileType<Sandnado>(), NPC.damage / 4, 3f, NPC.target, 0, 1);
        }

        private void CrystalBurst(int amount, bool above, float speed)
        {
            if (amount < 1)
                amount = 1;
            for (int i = 0; i < amount; i++)
            {
                float theta = Main.rand.NextFloat() * (float)Math.PI * (above?-1:2);
                float mag = speed + Main.rand.NextFloat() * (Angry ? 6 : 3);
                if(Main.netMode != 1)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<SharkronCrystalShard>(), NPC.damage / 4, 3f);
            }
        }

        private void InitializeAI()
        {
            NPC.TargetClosest(false);
            TargetPos = Main.player[NPC.target].Center;
            AIPhase = 4;
            NPC.ai[1] = 0;
            NPC.ai[0] = -1;
        }

        private void Slow()
        {
            NPC.velocity *= .95f;
        }

        private void QuickSlow()
        {
            NPC.velocity *= .9f;
        }

        private void MoveTowards(Vector2 targetPos)
        {
            Vector2 newVel = Vector2.Normalize(targetPos - NPC.Center);
            newVel *= Math.Min(Vector2.Distance(NPC.Center, targetPos) / 4, Math.Min(NPC.velocity.Length() + .6f, 8));
            NPC.velocity = NPC.velocity * .8f + newVel * .2f;
        }

        private void MoveTowardsSharp(Vector2 targetPos)
        {
            Vector2 newVel = Vector2.Normalize(targetPos - NPC.Center);
            newVel *= Math.Min(Vector2.Distance(NPC.Center, targetPos) / 4, Math.Min(NPC.velocity.Length() + .6f, 8));
            NPC.velocity = newVel;
        }

        private void MoveTowardsAtSpeed(Vector2 targetPos, float mag)
        {
            Vector2 newVel = Vector2.Normalize(targetPos - NPC.Center);
            newVel *= Math.Min(mag, NPC.velocity.Length() + .6f);
            NPC.velocity = newVel;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitByProjectile(projectile, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = 0;// frameHeight * Frame;
        }

        private void Transition()
        {
            PrevAIPhase = AIPhase;
            AIPhase = 4;
            NPC.ai[1] = 0;
            NPC.ai[0] = -1;
            Frame = 0;
        }
        public override void OnKill()
        {
            if (NewDawnWorld.downedEtheria)
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Etheramind>(), 1);

            if (!Main.expertMode)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<AncientShard>(), Main.rand.Next(1, 3));
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Crystilla>(), Main.rand.Next(4, 11));

                int ran = Main.rand.Next(1, 8);
                if (ran == 1) Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SandstorminaBottle, 1);
                if (ran == 2) Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.FlyingCarpet, 1);
                if (ran == 3) Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.BandofRegeneration, 1);
                if (ran == 4) Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.MagicMirror, 1);
                if (ran == 5) Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.CloudinaBottle, 1);
                if (ran == 6) Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.HermesBoots, 1);
                if (ran == 7) Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.EnchantedBoomerang, 1);
            }

            if (Main.expertMode)
                NPC.DropBossBags();
            if(Main.rand.Next(10) == 0)
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<DuneSharkronTrophy>(), 1);

            NewDawnWorld.downedDuneSharkron = true;
        }


        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = 188;
        }
    }
}
