using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Items.Equipables;
using NewDawn.Items.Loot;
using NewDawn.Items.Placeable;
using NewDawn.Items.Weapons.Snowball;
using NewDawn.Particles;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.PreTrio
{
    [AutoloadBossHead]
    public class Ragnar : ModNPC
    {
        private int AIPhase { get; set; }
        private int PrevAIPhase { get; set; }
        private int Counter { get; set; }
        private int Frame { get; set; }
        private Vector2 TargetPos;
        double rotSpeed = .1;

        private bool Angry { get; set; }
        public List<Particle> Particles;
        public List<Particle> BkgParticles;
        public static float HEART_HP = .2f;
        Texture2D Heart;

        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            // DisplayName.SetDefault("Ragnar");
        }
        public override void SetDefaults()
        {
            NPC.width = 64;
            NPC.height = 120;
            NPC.damage = 35;
            NPC.defense = 16;
            NPC.aiStyle = 0;
            NPC.lifeMax = 5000;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
            Music = global::Terraria.ModLoader.MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Ragnar");
            PrevAIPhase = AIPhase = 0;
            Counter = 0;
            TargetPos = NPC.Center;
            Angry = false;
            NPC.ai[0] = -1;
            Frame = 0;
            Particles = new List<Particle>();
            BkgParticles = new List<Particle>();
            NPC.velocity = Vector2.Zero;
            if (!Main.dedServ)
            {
                //Heart = ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/RagnarHeart").Value;
                Heart = ModContent.Request<Texture2D>("NewDawn/NPCs/PreTrio/RagnarHeart").Value;
            }

        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 10000 + (numPlayers * 2000);
            NPC.damage = 70;
        }

        public override void AI()
        {
            HealthCheck();
            PickAI();
            Effects();
            Counter++;
        }

        #region behavior AIs
        private void PickAI()
        {
            switch (AIPhase)
            {
                case 1:
                    FireballWave();
                    break;
                case 2:
                    FireballToss();
                    break;
                case 3:
                    FireballSpiral();
                    break;
                default:
                    Chase();
                    break;
            }
        }

        private void FireballSpiral()
        {
            Slow();
            if (Counter > 1 * 60 && Counter <= 5 * 60)
            {
                float mag = 10;
                double theta = Counter / 30.0;
                if (Counter % 10 == 0)
                {
                    int proj = Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(mag * (float)Math.Cos(theta), mag * (float)Math.Sin(theta)), ModContent.ProjectileType<RagnarFireball>(), NPC.damage / 2, 4f);
                    Main.projectile[proj].ai[0] = 3;
                    proj = Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(mag * (float)Math.Cos(theta + Math.PI), mag * (float)Math.Sin(theta + Math.PI)), ModContent.ProjectileType<RagnarFireball>(), NPC.damage / 2, 4f);
                    Main.projectile[proj].ai[0] = 3;
                }
            }
            if (Counter >= 6 * 60)
                ResetAttack();
        }

        private void FireballToss()
        {
            Slow();
            if (Counter > 1 * 60 && Counter <= 4 * 60)
            {
                int curOff = (Counter - (1 * 60) - ((Counter - (1 * 60)) % 15)) / 15;

                if (Counter % 60 == 30)
                {
                    int proj = Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(4, -4), ModContent.ProjectileType<RagnarFireballDown>(), NPC.damage / 2, 4f);
                    Main.projectile[proj].ai[0] = 2;
                    Main.projectile[proj].ai[1] = NPC.whoAmI;
                }
                if (Counter % 60 == 0)
                {
                    int proj = Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(-4, -4), ModContent.ProjectileType<RagnarFireballDown>(), NPC.damage / 2, 4f);
                    Main.projectile[proj].ai[0] = 2;
                    Main.projectile[proj].ai[1] = NPC.whoAmI;
                }
            }
            if (Counter >= 5 * 60)
                ResetAttack();
        }

        //TODO: Make pick random instead of one attack.
        private void PickRandomAttack()
        {
            Counter = 0;
            AIPhase = 2;
        }

        private void ResetAttack()
        {
            Counter = 0;
            AIPhase = 0;
        }

        private void Chase()
        {
            TargetPos = Target();
            if (Main.expertMode)
                NewMove(8, 1.5f + (1.5f * (NPC.life / NPC.lifeMax)));
            else
                NewMove(6, 3);
            if (Counter >= 6 * 60)
                PickRandomAttack();
        }

        private void FireballWave()
        {
            int xOffset = 60;
            int yOffset = 500;
            if (Counter < 2)
            {
                TargetPos = Main.player[NPC.target].Center;
                TargetPos.Y -= 180;
            }
            if (Counter < 1 * 60)
                MoveTowards(TargetPos, 8 + (Main.expertMode ? 4 : 0));
            else
                Slow();
            if (Counter > 3 * 60)
            {
                int curOff = (Counter - (3 * 60) - ((Counter - (3 * 60)) % 15)) / 15;
                if (Counter % 3 == 0)
                {
                    BkgParticles.Add(new Particle(ModContent.Request<Texture2D>("NewDawn/Particles/Flame").Value, new Vector2(NPC.Center.X + (xOffset * curOff) - 20 + Main.rand.Next(40), NPC.Center.Y + yOffset - 8 + Main.rand.Next(16)), Vector2.Zero, 7));
                    BkgParticles.Add(new Particle(ModContent.Request<Texture2D>("NewDawn/Particles/Flame").Value, new Vector2(NPC.Center.X - (xOffset * curOff) - 20 + Main.rand.Next(40), NPC.Center.Y + yOffset - 8 + Main.rand.Next(16)), Vector2.Zero, 7));
                }
                //Dust.NewDust(new Vector2(npc.Center.X + xOffset * curOff, npc.Center.Y + yOffset), 40, 10, ModContent.DustType<Magma>(), -2 + Main.rand.Next(5), -2 + Main.rand.Next(5));
                //Dust.NewDust(new Vector2(npc.Center.X - xOffset * curOff, npc.Center.Y + yOffset), 40, 10, ModContent.DustType<Magma>(), -2 + Main.rand.Next(5), -2 + Main.rand.Next(5));
                if (Counter % 15 == 0)
                {
                    int proj = Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(NPC.Center.X - (xOffset * curOff), NPC.Center.Y + yOffset), new Vector2(0, -18), ModContent.ProjectileType<RagnarFireball>(), NPC.damage / 2, 4f);
                    Main.projectile[proj].ai[0] = 1;
                    proj = Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(NPC.Center.X + (xOffset * curOff), NPC.Center.Y + yOffset), new Vector2(0, -18), ModContent.ProjectileType<RagnarFireball>(), NPC.damage / 2, 4f);
                    Main.projectile[proj].ai[0] = 1;
                }
            }
            if (Counter >= 8 * 60)
                ResetAttack();
        }

        #endregion


        private void Effects()
        {
            //Particles.Add(new Particle(mod.GetTexture("Particles/Spark"), npc.Center, Vector2.Zero, 8));
            AddParticles();
            ParticleManager.UpdateParticles(ref BkgParticles);
            ParticleManager.UpdateParticles(ref Particles);
        }

        private void AddParticles()
        {
            //Background
            if (Counter % 15 == 0)
                BkgParticles.Add(new Particle(ModContent.Request<Texture2D>("NewDawn/Particles/Flame").Value, NPC.Center + new Vector2((-NPC.width / 4) + Main.rand.Next(NPC.width / 2), 20 - (NPC.height / 4) + Main.rand.Next(NPC.height / 3)), Vector2.Zero, 7));
            if (Counter % 10 == 0)
            {
                BkgParticles.Add(new Particle(ModContent.Request<Texture2D>("NewDawn/Particles/Blaze").Value, NPC.Center + new Vector2(12 + (NPC.direction * 4) - 8 + Main.rand.Next(16), 20 + Main.rand.Next(16)), new Vector2(0, -1 - (float)Main.rand.NextDouble()), 6, 1, (float)(Main.rand.NextDouble() * 2 * Math.PI), .02f * (Main.rand.NextBool() ? 1 : -1))); BkgParticles.Add(new Particle(ModContent.Request<Texture2D>("NewDawn/Particles/Blaze").Value, NPC.Center + new Vector2(-30 + Main.rand.Next(60) + (NPC.direction * 4), 20 - (NPC.height / 4) + Main.rand.Next(NPC.height / 3)), new Vector2(0, -1 - (float)Main.rand.NextDouble()), 6, 1, (float)(Main.rand.NextDouble() * 2 * Math.PI), .02f * (Main.rand.NextBool() ? 1 : -1)));
                BkgParticles.Add(new Particle(ModContent.Request<Texture2D>("NewDawn/Particles/Blaze").Value, NPC.Center + new Vector2(12 + (NPC.direction * 4) - 8 + Main.rand.Next(16), 20 + Main.rand.Next(16)), new Vector2(-1 + (2 * (float)Main.rand.NextDouble()), -1 + (2 * (float)Main.rand.NextDouble())), 6, .95f, (float)(Main.rand.NextDouble() * 2 * Math.PI), .02f * (Main.rand.NextBool() ? 1 : -1)));
            }
            if (Counter % 5 == 0)
                BkgParticles.Add(new Particle(ModContent.Request<Texture2D>("NewDawn/Particles/Spark").Value, NPC.Center + new Vector2(12 + (NPC.direction * 4) - 8 + Main.rand.Next(16), 20 + Main.rand.Next(16)), new Vector2((float)(-1 + (2 * Main.rand.NextDouble())) / 4f, -1 - (float)Main.rand.NextDouble()), 8));

            //Foreground
            if (Counter % 25 == 0)
                Particles.Add(new Particle(ModContent.Request<Texture2D>("NewDawn/Particles/Flame").Value, NPC.Center + new Vector2((-NPC.width / 2) + Main.rand.Next(NPC.width), 20 - (NPC.height / 4) + Main.rand.Next(NPC.height / 2)), Vector2.Zero, 7));
        }

        private void HealthCheck()
        {
            if (NPC.life <= (int)(HEART_HP * NPC.lifeMax) && Main.expertMode)
            {
                if (Music != global::Terraria.ModLoader.MusicLoader.GetMusicSlot(Mod, "Sounds/Music/RagnarHeart"))
                    HeartChange();
            }
        }

        private void HeartChange()
        {
            Music = global::Terraria.ModLoader.MusicLoader.GetMusicSlot(Mod, "Sounds/Music/RagnarHeart");
        }

        private void MoveTowards(Vector2 targetPos, float maxVel = 16f)
        {
            Vector2 newVel = Vector2.Normalize(targetPos - NPC.Center);
            newVel *= Math.Min(Math.Min(Vector2.Distance(NPC.Center, targetPos) / 4, NPC.velocity.Length() + .6f), maxVel);
            NPC.velocity = newVel;
        }

        private void MoveTo(Vector2 targetVector, float maxVel = 16f, float accel = 1.1f)
        {
            var tarRot = targetVector.ToRotation();
            var curRot = NPC.velocity.ToRotation();
            var rotGoal = tarRot - curRot;

            if (rotGoal > Math.PI)
                rotGoal -= 2 * (float)Math.PI;
            if (rotGoal < -Math.PI)
                rotGoal += 2 * (float)Math.PI;

            if (rotGoal > rotSpeed)
                NPC.velocity = NPC.velocity.RotatedBy(rotSpeed) / accel;
            if (rotGoal < -rotSpeed)
                NPC.velocity = NPC.velocity.RotatedBy(-rotSpeed) / accel;

            if (NPC.velocity.Length() < maxVel)
            {
                if (NPC.velocity.Length() < maxVel / 10)
                    MoveTowards(targetVector, maxVel);
                NPC.velocity *= accel;
            }
        }

        private void NewMove(float maxSpeed = 8f, float rotationSlowness = 2)
        {
            var targetPos = Target();
            var goalDirection = (targetPos - NPC.Center).ToRotation();
            var direction = NPC.velocity.ToRotation();
            var speed = NPC.velocity.Length();
            var rotDir = GetRotDiff(direction, goalDirection);
            if (speed > 4)
            {
                rotDir = Math.Sign(rotDir) * Math.Abs(rotDir) / Math.Max(NPC.velocity.Length() * rotationSlowness, 1);
                direction = direction + rotDir;

                var dist = (targetPos - NPC.Center).Length();
                if (dist != 0)
                    speed = Math.Min(dist / 8, maxSpeed);
                else
                    speed = 0;

                NPC.velocity = new Vector2((float)Math.Cos(direction) * speed, (float)Math.Sin(direction) * speed);
            }
            else
                MoveTowards(targetPos);
        }

        private float GetRotDiff(float ang1, float ang2)
        {
            var distance = ang2 - ang1;
            if (Math.Abs(distance) > Math.PI)
                return -Math.Sign(distance) * ((2 * (float)Math.PI) - Math.Abs(distance));
            return distance;
        }


        private Vector2 Target()
        {
            var targetPos = NPC.Center;
            targetPos.X += 800;
            bool spotted = false;
            for (int i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player.active && player.statLife > 0)//(npc.GetGlobalNPC<EtherialGlobalNPC>().bitherial || (NewDawnVars.eNPCs.Contains(npc.type) && NewDawnWorld.downedEtheria) || (!NewDawnVars.eNPCs.Contains(npc.type) && !NewDawnWorld.downedEtheria)))
                {
                    Vector2 newMove = player.Center - NPC.Center;
                    var curMove = targetPos - NPC.Center;
                    if (newMove.Length() < curMove.Length())
                    {
                        targetPos = player.Center;
                        NPC.target = i;
                        spotted = true;
                    }
                }
            }
            if (spotted)
                return targetPos;
            return Main.player[NPC.target].Center;
        }

        private void Slow()
        {
            NPC.velocity *= .95f;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            ParticleManager.DrawParticles(spriteBatch, Color.White, ref BkgParticles);

            //Draw Heart
            Rectangle rect = new Rectangle(0, (int)Math.Floor(Counter / 4.0) % 8 * Heart.Height / 8, Heart.Width, Heart.Height / 8);
            spriteBatch.Draw(Heart, NPC.Center - Main.screenPosition + new Vector2(-28 + (NPC.direction * 4), -10), rect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);

            return NPC.life > (int)(HEART_HP * NPC.lifeMax) && Main.expertMode;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            base.PostDraw(spriteBatch, screenPos, drawColor);
            ParticleManager.DrawParticles(spriteBatch, Color.White, ref Particles);
        }



        public override void OnKill()
        {
            if (NewDawnWorld.downedEtheria)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<MoltenEtheria>(), 1);
            }

            if (!Main.expertMode)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<DarkShard>(), Main.rand.Next(1, 3));
                int obsidiumItem = 0;
                int rand = Main.rand.Next(7);
                switch (rand)
                {
                    case 0:
                        obsidiumItem = ItemID.LavaCharm;
                        break;
                    case 1:
                        obsidiumItem = ModContent.ItemType<ObsidiumLily>();
                        break;
                    case 2:
                        obsidiumItem = ModContent.ItemType<FireDust>();
                        break;
                    case 3:
                        obsidiumItem = ModContent.ItemType<Eruption>();
                        break;
                    case 4:
                        obsidiumItem = ModContent.ItemType<CrystalizedMagma>();
                        break;
                    case 5:
                        obsidiumItem = ModContent.ItemType<Ragnashia>();
                        break;
                    default:
                        obsidiumItem = ModContent.ItemType<MagmaHeart>();
                        break;
                }
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, obsidiumItem, 1);
                if (Main.rand.Next(4) == 0)
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<BlackIce>(), 1);
                if (Main.rand.Next(10) == 0)
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<RagnarTrophy>(), 1);
            }
            if (!NewDawnWorld.downedRagnar)
                Main.NewText("Fury runs through the Obsidium Caverns.", 250, 150, 50);
            NewDawnWorld.downedRagnar = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }


        public List<int> Fireballs { get; set; }
    }
}