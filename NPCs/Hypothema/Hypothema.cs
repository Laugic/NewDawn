using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Dusts;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    [AutoloadBossHead]
    public class Hypothema : ModNPC
    {
        #region Initializations
        public int despawn = 0;
        static int NPC_WIDTH = 194, CLOUD_HEIGHT = 66,
            INITIALIZE = 0, TRANSITION = 1, FOLLOW = 2, STEWARD = 3, SLOW = 4, SNOWBOMB = 5, SHOTGUN = 6, SMASH = 7, UPSHOOT = 8, DASH = 9, HATTHROW = 10, GUNTWIRL = 11, CLOUDS = 12;
        public enum AIPhase
        {
            RainChase,
            ShotgunDash,
            HatThrow,
            DashChase,
            Steward,
            Boobomb,
            Stomp,
            RainChase2,
            Dash2,
            DirectChase2,
            RapidDash2,
            FlyHigh,
            SpinCycle,
            DashIntoAttack,
            DoubleSteward,
            UpwardSpray,
            BoobombSpray,
            GrabGuns
        }
        private List<int> Attacks;
        private List<int> NextAttacks;
        public int HealthPhase { get; set; } = 1;
        private AIPhase PrevAttackPhase { get; set; }
        private AIPhase PrevAIPhase { get; set; }
        public AIPhase CurrentPhase { get; set; }
        private List<AIPhase> Phase1MovementAttacks = new List<AIPhase> { AIPhase.ShotgunDash, AIPhase.HatThrow, AIPhase.DashChase },
            Phase1StationaryAttacks = new List<AIPhase> { AIPhase.Steward, AIPhase.Boobomb, AIPhase.Stomp },
            Phase2MovementAttacks = new List<AIPhase> { AIPhase.RapidDash2, AIPhase.FlyHigh, AIPhase.SpinCycle },
            Phase2StationaryAttacks = new List<AIPhase> { AIPhase.DoubleSteward, AIPhase.UpwardSpray, AIPhase.BoobombSpray },
            NextMovementAttacks = new List<AIPhase>(),
            NextStationaryAttacks = new List<AIPhase>();
        private bool AttackBool { get; set; }
        private int CloudFrame { get; set; }
        private float CloudAlpha { get; set; }
        private float Speed { get; set; }
        private Vector2 TargetPos, Offset = new Vector2();
        private Projectile SavedProjectile { get; set; }
        private bool Angry { get; set; }
        private bool FrostLegion { get; set; }
        private bool FrostMoon { get; set; }
        private int Tracker { get; set; }

        public Texture2D AnimFrames;
        public Texture2D CloudFront;
        public Texture2D CloudBack;
        public Texture2D GunSpin;
        int FrameDelay { get; set; } = 0;

        public int AttackCounter { get; set; } = 0;
        #endregion

        #region MainMethods
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hypothema");
        }

        public override void SetDefaults()
        {
            //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Test"), Color.White);
            NPC.width = 84;
            NPC.height = 140;
            NPC.damage = 40;
            NPC.defense = 8;
            NPC.aiStyle = -1;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 10f; // Take up open spawn slots, preventing random NPCs from spawning during the fight
            //Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Hypothema");
            PrevAIPhase = CurrentPhase = AIPhase.RainChase;
            HealthPhase = 1;
            FrameDelay = 0;
            CloudFrame = 0;
            CloudAlpha = 0;
            NPC.ai[0] = NPC.ai[1] = NPC.ai[2] = 0;
            Tracker = 0;
            Speed = 2;
            AttackBool = false;
            Attacks = new List<int> { STEWARD, SMASH, DASH, FOLLOW, CLOUDS };
            NextAttacks = new List<int>();
            NextMovementAttacks = new List<AIPhase>();
            NextStationaryAttacks = new List<AIPhase>();
            NextMovementAttacks.AddRange(Phase1MovementAttacks);
            NextMovementAttacks.AddRange(Phase1MovementAttacks);
            NextStationaryAttacks.AddRange(Phase1StationaryAttacks);
            NextStationaryAttacks.AddRange(Phase1StationaryAttacks);
            FrostMoon = Main.snowMoon;
            FrostLegion = Main.invasionType == 2;
            if (FrostLegion)
                NPC.lifeMax += 5000;
            if (FrostMoon)
                NPC.lifeMax += 8000;

            // The following code assigns a music track to the boss in a simple way.
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Hypothema");

                // If you would like to play alternate music when the otherworld soundtrack enabled, use this logic.
                if (!Main.swapMusic == Main.drunkWorld && !Main.remixWorld)
                {
                    Music = MusicID.OtherworldlyBoss1;
                }

                AnimFrames = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace("NewDawn.", "").Replace('.', '/') + "/Hypothema_Frames");
                CloudFront = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace("NewDawn.", "").Replace('.', '/') + "/HypothemaCloud");
                CloudBack = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace("NewDawn.", "").Replace('.', '/') + "/HypothemaCloudOutline");
                GunSpin = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace("NewDawn.", "").Replace('.', '/') + "/GunSpin");
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax += numPlayers * 5000;
        }

        public override void AI()
        {
            HealthEffects();
            Effects();
            CheckDespawnAI();
            SetBaseSpeed();
            RunAI();
        }
        #endregion

        #region Phase Control Methods
        private void PhaseChange()
        {
            Offset = Vector2.Zero;
            NPC.ai[0] = 0;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            AttackBool = false;
            CloudAlpha = 0;
            GetNextPhase();
        }

        private void RunAI()
        {
            switch (CurrentPhase)
            {
                case AIPhase.RainChase:
                    RainChase();
                    break;
                case AIPhase.ShotgunDash:
                    ShotgunDash();
                    break;
                case AIPhase.HatThrow:
                    HatThrow();
                    break;
                case AIPhase.DashChase:
                    DashChase();
                    break;
                case AIPhase.Steward:
                    Steward();
                    break;
                case AIPhase.Boobomb:
                    Boobomb();
                    break;
                case AIPhase.Stomp:
                    Stomp();
                    break;
                case AIPhase.RainChase2:
                    RainChase2();
                    break;
                case AIPhase.Dash2:
                    Dash();
                    break;
                case AIPhase.DirectChase2:
                    DirectChase();
                    break;
                case AIPhase.RapidDash2:
                    RapidDash();
                    break;
                case AIPhase.FlyHigh:
                    FlyHigh();
                    break;
                case AIPhase.SpinCycle:
                    SpinCycle();
                    break;
                case AIPhase.DashIntoAttack:
                    DashIntoAttack();
                    break;
                case AIPhase.DoubleSteward:
                    DoubleSteward();
                    break;
                case AIPhase.UpwardSpray:
                    UpwardSpray();
                    break;
                case AIPhase.BoobombSpray:
                    BoobombSpray();
                    break;
                case AIPhase.GrabGuns:
                    GrabGuns();
                    break;
                default:
                    CheckDespawnAI();
                    break;
            }
        }

        private void GetNextPhase()
        {
            switch (HealthPhase)
            {
                case 1:
                    if (CurrentPhase == AIPhase.RainChase)
                    {
                        if (Phase1MovementAttacks.Contains(PrevAttackPhase))
                        {
                            PrevAIPhase = CurrentPhase;
                            GetStationaryAttack();
                        }
                        else
                        {
                            PrevAIPhase = CurrentPhase;
                            GetMovementAttack();
                        }
                    }
                    else
                    {
                        PrevAIPhase = CurrentPhase;
                        CurrentPhase = AIPhase.RainChase;
                    }
                    break;
                case 2:
                    if (CurrentPhase == AIPhase.RainChase2)
                    {
                        PrevAIPhase = CurrentPhase;
                        CurrentPhase = AIPhase.DirectChase2;
                    }
                    else if (CurrentPhase == AIPhase.DirectChase2)
                    {
                        PrevAIPhase = CurrentPhase;
                        CurrentPhase = AIPhase.Dash2;
                    }
                    else if (CurrentPhase == AIPhase.Dash2)
                    {
                        PrevAIPhase = CurrentPhase;
                        GetMovementAttack();
                    }
                    else if (Phase2MovementAttacks.Contains(CurrentPhase))
                    {
                        PrevAIPhase = CurrentPhase;
                        CurrentPhase = AIPhase.DashIntoAttack;
                    }
                    else if (CurrentPhase == AIPhase.DashIntoAttack)
                    {
                        PrevAIPhase = CurrentPhase;
                        GetStationaryAttack();
                    }
                    else
                    {
                        PrevAIPhase = CurrentPhase;
                        CurrentPhase = AIPhase.RainChase2;
                    }
                    break;
                default:
                    PrevAIPhase = CurrentPhase;
                    CurrentPhase = AIPhase.GrabGuns;
                    break;
            }

            if (Phase1MovementAttacks.Contains(PrevAIPhase) || Phase2MovementAttacks.Contains(PrevAIPhase) ||
                Phase1StationaryAttacks.Contains(PrevAIPhase) || Phase2StationaryAttacks.Contains(PrevAIPhase))
                PrevAttackPhase = PrevAIPhase;
        }

        private void GetMovementAttack()
        {
            if (NextMovementAttacks.Count == 0)
            {
                if (HealthPhase == 1)
                {
                    NextMovementAttacks.AddRange(Phase1MovementAttacks);
                    NextMovementAttacks.AddRange(Phase1MovementAttacks);
                }
                else
                {
                    NextMovementAttacks.AddRange(Phase2MovementAttacks);
                    NextMovementAttacks.AddRange(Phase2MovementAttacks);
                }
            }

            int i = Main.rand.Next(NextMovementAttacks.Count);
            CurrentPhase = NextMovementAttacks[i];
            NextMovementAttacks.RemoveAt(i);
        }

        private void GetStationaryAttack()
        {
            if (NextStationaryAttacks.Count > 0)
            {
                if (HealthPhase == 1)
                {
                    NextStationaryAttacks.AddRange(Phase1StationaryAttacks);
                    NextStationaryAttacks.AddRange(Phase1StationaryAttacks);
                }
                else
                {
                    NextStationaryAttacks.AddRange(Phase2StationaryAttacks);
                    NextStationaryAttacks.AddRange(Phase2StationaryAttacks);
                }
            }

            int i = Main.rand.Next(NextStationaryAttacks.Count);
            CurrentPhase = NextStationaryAttacks[i];
            NextStationaryAttacks.RemoveAt(i);
        }
        #endregion

        #region Phase AIs
        private void RainChase()
        {
            if (Offset == Vector2.Zero)
            {
                if (NPC.Center.X < Main.player[NPC.target].Center.X)
                    Offset = new Vector2(600, -500);
                else
                    Offset = new Vector2(-600, -500);
            }
            if ((TargetPos.X < Main.player[NPC.target].Center.X && NPC.Center.X < TargetPos.X) ||
                (TargetPos.X > Main.player[NPC.target].Center.X && NPC.Center.X > TargetPos.X))
                Offset.X *= -1;
            TargetPos = Main.player[NPC.target].Center + Offset;
            RainSnow();

            MoveTowardsAtSpeed(TargetPos, Speed);
            NPC.ai[0]++;
            if (NPC.ai[0] > 2 * 60)
                PhaseChange();
        }

        private void ShotgunDash()
        {
            if (Offset == Vector2.Zero)
            {
                if (NPC.Center.X < Main.player[NPC.target].Center.X)
                    Offset = new Vector2(500, -200);
                else
                    Offset = new Vector2(-500, -200);
                AttackBool = false; //AttackBool tracks if shotgun is being fired
                NPC.ai[0] = 0;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
            }
            FacePlayer();
            TargetPos = Main.player[NPC.target].Center + Offset;
            MoveTowardsAtSpeed(TargetPos, Speed * 2);

            NPC.ai[0]++;
            if (NPC.ai[0] > 30)
                AttackBool = true;
            if (NPC.ai[0] == 45 + 30)
            {
                Offset.X *= -1;
                var newPos = NPC.Center;
                newPos.X -= 80 * NPC.spriteDirection;
                SoundEngine.PlaySound(SoundID.Item36, NPC.Center);
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(6 * NPC.spriteDirection, -6), ModContent.ProjectileType<ShotgunYeet>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                for (int i = 0; i < 12; i++)
                {
                    var theta = (Math.PI * .67) + (Main.rand.NextDouble() * Math.PI / 2);
                    float mag = 8 + (Main.rand.NextFloat() * 6);
                    Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag * NPC.spriteDirection, (float)Math.Sin(theta) * mag / 2), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                }
            }
            if (NPC.ai[0] > 60 + 30)
            {
                AttackBool = false;
                NPC.ai[0] = 0;
                NPC.ai[1]++;
                if (NPC.ai[1] > 5)
                    PhaseChange();
            }
        }

        private void HatThrow()
        {
            FacePlayer();
            NPC.ai[0]++;
            if (NPC.ai[0] < 4.5f * 60)
            {
                Slow();
                if (NPC.ai[0] >= 18 && !AttackBool)
                {
                    AttackBool = true;
                    var newPos = NPC.Center;
                    newPos.X -= 80 * NPC.spriteDirection;
                    var entitySource = new EntitySource_Parent(this.Entity);
                    NPC.ai[1] = Projectile.NewProjectile(entitySource, newPos, new Vector2(0, 0), ModContent.ProjectileType<HatSpin>(), NPC.damage / 4, 6f, NPC.target, 0, NPC.whoAmI);
                }
            }
            else if (NPC.ai[0] < 7 * 60)
            {
                if (Main.projectile[(int)NPC.ai[1]].type != ModContent.ProjectileType<HatSpin>() || !Main.projectile[(int)NPC.ai[1]].active)
                    PhaseChange();
                else
                {
                    TargetPos = Main.player[NPC.target].Center;// - (Main.projectile[(int)NPC.ai[1]].Center - Main.player[NPC.target].Center);
                    MoveTowards(TargetPos, Speed, MathHelper.ToRadians(2f));
                }
            }
            else
            {
                if (Main.projectile[(int)NPC.ai[1]].type != ModContent.ProjectileType<HatSpin>() || !Main.projectile[(int)NPC.ai[1]].active)
                    PhaseChange();
                else
                {
                    TargetPos = Main.projectile[(int)NPC.ai[1]].Center;
                    MoveTowardsAtSpeed(TargetPos, Speed * 2);
                }
            }
        }

        private void Test()
        {
            TargetPos = Main.player[NPC.target].Center;
            MoveTowards(TargetPos, Speed, MathHelper.ToRadians(2f));
        }

        private void DashChase()
        {
            NPC.ai[0]++;
            NPC.ai[1]--;
            if (NPC.ai[1] < 0)
            {
                NPC.ai[1] = 150;
                TargetPos = Main.player[NPC.target].Center;
            }
            if (NPC.ai[1] < 60)
            {
                TargetPos += NPC.velocity;
                WakeOfSnow();
                MoveTowards(TargetPos, Speed * 3, MathHelper.ToRadians(1f));
            }
            else
            {
                TargetPos = Main.player[NPC.target].Center;
                MoveTowards(TargetPos, Speed, MathHelper.ToRadians(2f));
            }
            if (NPC.ai[0] > 12 * 60)
                PhaseChange();
        }


        private void Steward()
        {
            NPC.ai[0]++;
            Slow();
            var dist = 120;
            var newPos = NPC.Center;
            newPos.Y -= dist;

            if (NPC.ai[0] == 15)
            {
                var entitySource = new EntitySource_Parent(this.Entity);
                NPC.ai[1] = Projectile.NewProjectile(entitySource, newPos, new Vector2(), ModContent.ProjectileType<Steward>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Main.projectile[(int)NPC.ai[1]].scale = 1f / (2 * 60f);
            }
            if (NPC.ai[0] > 15)
                Main.projectile[(int)NPC.ai[1]].scale += 1f / (2 * 60f);
            if (NPC.ai[0] < (1 * 60) + 30)
            {
                var theta = Math.PI * 2 * Main.rand.NextDouble();
                var dustShift = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                int dust = Dust.NewDust(newPos + (dustShift * dist), 0, 0, ModContent.DustType<SnowAccumulateDust>(), 0, 0, 0, default, 1);
                Main.dust[dust].velocity = new Vector2(dustShift.X * -2, dustShift.Y * -2);
            }
            if (NPC.ai[0] > 2 * 60)
            {
                Main.projectile[(int)NPC.ai[1]].scale = 1;
                PhaseChange();
            }
        }


        private void Boobomb()
        {
            TargetPos = Main.player[NPC.target].Center;
            TargetPos.Y -= 200;
            NPC.ai[0]++;
            if (NPC.ai[0] < 90)
                MoveTowardsAtSpeed(TargetPos, Speed * 2);
            else
                Slow();
            if (NPC.ai[0] == 90 + 36)
            {
                var newPos = NPC.Center;
                newPos.Y -= 20;
                newPos.X -= 40;
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(-2 - (Main.rand.NextFloat() * 4), -1 - Main.rand.NextFloat()), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                newPos.X += 80;
                Projectile.NewProjectile(entitySource, newPos, new Vector2(2 + (Main.rand.NextFloat() * 4), -1 - Main.rand.NextFloat()), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
            }
            if (NPC.ai[0] >= (2 * 60) + 30)
                PhaseChange();
        }

        private void Stomp()
        {
            NPC.ai[0]++;
            if (NPC.ai[0] > 2 * 60)
            {
                if (!AttackBool)
                    CloudAlpha += .04f;
                if (CloudAlpha >= 1)
                {
                    CloudAlpha = 1;
                    bool floored = false;
                    NPC.ai[1]++;
                    if (NPC.ai[1] == 1)
                        NPC.velocity.Y = .5f;
                    if (NPC.velocity.Y > 0)
                        NPC.velocity.Y = Math.Min(NPC.velocity.Y + .5f, 16);
                    NPC.noTileCollide = false;
                    NPC.collideX = false;
                    NPC.collideY = true;
                    if (NPC.velocity.Y == 0)
                        floored = true;

                    NPC.velocity.X = 0;

                    if (floored)
                    {
                        if (!AttackBool)
                        {
                            AttackBool = true;
                            for (int k = 0; k < (Main.expertMode ? 20 : 10); k++)
                            {
                                var theta = Main.rand.NextDouble() * Math.PI;
                                float mag = -5 - (Main.rand.NextFloat() * 3);
                                var newPos = NPC.Center;
                                newPos.Y += (NPC.height / 2) - 8;
                                var entitySource = new EntitySource_Parent(this.Entity);
                                Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
                            }
                            TriggerScreenshake(15, 5);
                        }
                        NPC.velocity.Y = 0;
                    }
                }
                else
                    Slow();
                if (AttackBool)
                    CloudAlpha = Math.Max(CloudAlpha - .02f, 0);
            }
            else
            {
                TargetPos = Main.player[NPC.target].Center;
                TargetPos.Y -= 400;
                MoveTowardsAtSpeed(TargetPos, Speed * 2);
            }
            if (NPC.ai[1] > 2 * 60 || NPC.ai[0] > 5 * 60)
            {
                PhaseChange();
                NPC.noTileCollide = true;
                NPC.collideX = false;
                NPC.collideY = false;
            }
        }

        private void RainChase2()
        {
            if (Offset == Vector2.Zero)
            {
                if (NPC.Center.X < Main.player[NPC.target].Center.X)
                    Offset = new Vector2(400, -200);
                else
                    Offset = new Vector2(-400, -200);
                NPC.ai[1] = Main.rand.Next(90) + 90;
            }
            if ((TargetPos.X < Main.player[NPC.target].Center.X && NPC.Center.X < TargetPos.X) ||
                (TargetPos.X > Main.player[NPC.target].Center.X && NPC.Center.X > TargetPos.X))
                Offset.X *= -1;
            TargetPos = Main.player[NPC.target].Center + Offset;
            RainSnow();
            RainSnow();

            MoveTowardsAtSpeed(TargetPos, Speed * 2);
            NPC.ai[0]++;
            if (NPC.ai[0] > NPC.ai[1])
                PhaseChange();
        }

        private void Dash()
        {
            if (NPC.ai[0] == 0)
                TargetPos = Main.player[NPC.target].Center;
            MoveTowardsAtSpeed(TargetPos, Speed * 3);
            TargetPos += NPC.velocity;
            WakeOfSnow();
            NPC.ai[0]++;
            if (NPC.ai[0] > 60)
                PhaseChange();
        }

        private void DirectChase()
        {
            TargetPos = Main.player[NPC.target].Center;

            NPC.ai[0]++;
            MoveTowardsAtSpeed(TargetPos, Speed * 1.25f);
            if (NPC.ai[0] > 150)
                PhaseChange();
        }

        private void RapidDash()
        {
            NPC.ai[0]--;
            if (NPC.ai[0] < 0 || Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > 800)
            {
                NPC.ai[0] = 120;
                NPC.ai[1]++;
                TargetPos = Main.player[NPC.target].Center;
                if (NPC.ai[1] > 5)
                    PhaseChange();
            }
            WakeOfSnow();
            TargetPos += NPC.velocity;
            MoveTowardsAtSpeed(TargetPos, Speed * 3);
        }

        private void FlyHigh()
        {
            TargetPos = Main.player[NPC.target].Center;
            TargetPos.Y -= 1000;
            NPC.ai[0]++;
            if (NPC.ai[0] == 180)
                TriggerScreenshake(15, 3);//TODO: add sfx indicator
            if (NPC.ai[0] < 180)
            {
                RainSnow();
                RainSnow();
                MoveTowardsAtSpeed(TargetPos, Speed * 3);
            }
            else
            {
                if (!AttackBool)
                    CloudAlpha += .04f;
                if (CloudAlpha >= 1)
                {
                    CloudAlpha = 1;
                    bool floored = false;
                    NPC.ai[1]++;
                    if (NPC.ai[1] == 1)
                        NPC.velocity.Y = .5f;
                    if (NPC.velocity.Y > 0)
                        NPC.velocity.Y = Math.Min(NPC.velocity.Y + .5f, 16);
                    NPC.noTileCollide = false;
                    NPC.collideX = false;
                    NPC.collideY = true;
                    if (NPC.velocity.Y == 0)
                        floored = true;

                    NPC.velocity.X = 0;

                    if (floored)
                    {
                        if (!AttackBool)
                        {
                            AttackBool = true;
                            TriggerScreenshake(30, 6);
                            for (int k = 0; k < (Main.expertMode ? 20 : 10); k++)
                            {
                                var theta = Main.rand.NextDouble() * Math.PI;
                                float mag = -5 - (Main.rand.NextFloat() * 3);
                                var newPos = NPC.Center;
                                newPos.Y += (NPC.height / 2) - 8;
                                var entitySource = new EntitySource_Parent(this.Entity);
                                Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
                            }
                            for (int k = 0; k < (Main.expertMode ? 20 : 10); k++)
                            {
                                var theta = Main.rand.NextDouble() * Math.PI;
                                float mag = -5 - (Main.rand.NextFloat() * 3);
                                mag *= 2;
                                var newPos = NPC.Center;
                                newPos.Y += (NPC.height / 2) - 8;
                                var entitySource = new EntitySource_Parent(this.Entity);
                                Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
                            }
                        }
                        NPC.velocity.Y = 0;
                    }
                }
                else
                    Slow();
                if (AttackBool)
                    CloudAlpha = Math.Max(CloudAlpha - .02f, 0);
            }

            if (NPC.ai[1] > 2 * 60 || NPC.ai[0] > 7 * 60)
            {
                PhaseChange();
                NPC.noTileCollide = true;
                NPC.collideX = false;
                NPC.collideY = false;
            }
        }

        private void SpinCycle()
        {
            NPC.ai[2] += 0.025f;
            if (Main.expertMode)
                MoveTowardsAtSpeed(Main.player[NPC.target].Center + new Vector2((float)Math.Cos(NPC.ai[2]) * 600, (float)Math.Sin(NPC.ai[2]) * 600), Speed * 3);
            else
                Slow();

            NPC.ai[0]++;
            if (NPC.ai[0] > 4 && NPC.ai[0] < 8 * 60)
            {
                AttackBool = true;
                if (NPC.ai[0] % 4 == 0 && Main.netMode != 1)
                {
                    var theta = Main.rand.NextDouble() * 2 * Math.PI;
                    float mag = 9 + (Main.rand.NextFloat() * 3);
                    var newPos = NPC.Center;
                    newPos.X -= ((NPC.ai[0] % 4 == 0 ? -1 : 1) * 66 * NPC.spriteDirection) - (50 * (float)Math.Cos(theta));
                    newPos.Y -= 36 - (50 * (float)Math.Sin(theta));

                    var entitySource = new EntitySource_Parent(this.Entity);
                    Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
                }
            }
            else
            {
                NPC.ai[1]++;
                AttackBool = false;
            }
            if (NPC.ai[0] > (8 * 60) + 12)
                PhaseChange();
        }

        private void DashIntoAttack()
        {
            //TODO: Add SFX and VFX to indicate dash is coming
            NPC.ai[0]++;
            if (NPC.ai[0] == 60)
                TargetPos = Main.player[NPC.target].Center;
            if (NPC.ai[0] > 60)
            {
                TargetPos += NPC.velocity;
                MoveTowardsAtSpeed(TargetPos, Speed * 3);
                WakeOfSnow();
                WakeOfSnow();
            }
            else
                Slow();
            if (NPC.ai[0] > 105)
                PhaseChange();
        }

        private void DoubleSteward()
        {
            NPC.ai[0]++;
            Slow();
            var dist = 120;
            var newPos = NPC.Center;
            newPos.Y -= dist;
            Offset.X = -50;
            Offset.Y = 0;

            if (NPC.ai[0] == 15)
            {
                var entitySource = new EntitySource_Parent(this.Entity);
                NPC.ai[1] = Projectile.NewProjectile(entitySource, newPos + Offset, Vector2.Zero, ModContent.ProjectileType<Steward>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                NPC.ai[2] = Projectile.NewProjectile(entitySource, newPos - Offset, Vector2.Zero, ModContent.ProjectileType<Steward>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Main.projectile[(int)NPC.ai[1]].scale = 1f / (2 * 60f);
                Main.projectile[(int)NPC.ai[2]].scale = 1f / (2 * 60f);
            }
            if (NPC.ai[0] > 15)
            {
                Main.projectile[(int)NPC.ai[1]].scale += 1f / (2 * 60f);
                Main.projectile[(int)NPC.ai[2]].scale += 1f / (2 * 60f);
            }
            if (NPC.ai[0] < (1 * 60) + 30)
            {
                var theta = Math.PI * 2 * Main.rand.NextDouble();
                var dustShift = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                int dust = Dust.NewDust(newPos + Offset + (dustShift * dist), 0, 0, ModContent.DustType<SnowAccumulateDust>(), 0, 0, 0, default, 1);
                Main.dust[dust].velocity = new Vector2(dustShift.X * -2, dustShift.Y * -2);
                int dust2 = Dust.NewDust(newPos - Offset + (dustShift * dist), 0, 0, ModContent.DustType<SnowAccumulateDust>(), 0, 0, 0, default, 1);
                Main.dust[dust2].velocity = new Vector2(dustShift.X * -2, dustShift.Y * -2);
            }
            if (NPC.ai[0] > 2 * 60)
            {
                Main.projectile[(int)NPC.ai[1]].scale = 1;
                Main.projectile[(int)NPC.ai[1]].velocity.X = -5;
                Main.projectile[(int)NPC.ai[2]].scale = 1;
                Main.projectile[(int)NPC.ai[2]].velocity.X = 5;
                PhaseChange();
            }
        }

        private void UpwardSpray()
        {
            TargetPos = Main.player[NPC.target].Center;
            TargetPos.Y -= 200;
            MoveTowardsAtSpeed(TargetPos, Speed * 2);
            NPC.ai[0]++;
            if (NPC.ai[0] % 3 == 0)
            {
                NPC.ai[1]++;
                switch (NPC.ai[1] % 4)
                {
                    case 1:
                        ShootUp(-NPC.spriteDirection * 28);
                        break;
                    case 2:
                        break;
                    case 3:
                        ShootUp(NPC.spriteDirection * 28);
                        break;
                    default:
                        break;
                }
            }
            if (NPC.ai[0] > 4 * 60)
                PhaseChange();
        }

        private void BoobombSpray()
        {
            TargetPos = Main.player[NPC.target].Center;
            TargetPos.Y -= 200;
            NPC.ai[0]++;
            if (NPC.ai[0] < 90)
                MoveTowardsAtSpeed(TargetPos, Speed * 2);
            else
                Slow();
            if (NPC.ai[0] == 90)
            {
                Offset.X = -30;
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, NPC.Center + Offset, new Vector2(6 * NPC.spriteDirection, -6), ModContent.ProjectileType<ShotgunYeet>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Projectile.NewProjectile(entitySource, NPC.Center - Offset, new Vector2(-6 * NPC.spriteDirection, -6), ModContent.ProjectileType<ShotgunYeet>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
            }
            if (NPC.ai[0] == 90 + 36)
            {
                var newPos = NPC.Center;
                newPos.Y -= 20;
                newPos.X -= 40;
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(0, -4), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(-3, -3), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(-4, -2), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                newPos.X += 80;
                Projectile.NewProjectile(entitySource, newPos, new Vector2(0, -4), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(3, -3), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(4, -2), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
            }
            if (NPC.ai[0] >= (2 * 60) + 30)
            {
                NPC.ai[0] = 0;
                CurrentPhase = AIPhase.GrabGuns;
            }
        }

        private void GrabGuns()
        {
            Slow();
            NPC.ai[0]++;
        }
        #endregion

        #region SpawnProjectiles
        private void WakeOfSnow()
        {
            if (Main.rand.Next(6) == 0 && Main.netMode != 1)
            {
                var dir = NPC.velocity.ToRotation();
                var theta = (Main.rand.NextDouble() * 2) + dir - 1 + Math.PI;
                float mag = 6 + (Main.rand.NextFloat() * 3);
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, NPC.Center, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
            }
        }

        private void RainSnow()
        {
            if (Main.rand.Next(12) == 0 && Main.netMode != 1)
            {
                var entitySource = new EntitySource_Parent(this.Entity);
                var randOffset = new Vector2(Main.rand.NextFloat(-80, 80), Main.rand.NextFloat(40, 50));
                Projectile.NewProjectile(entitySource, NPC.Center + randOffset, new Vector2(0, 8), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
            }
        }

        private void SpawnCloud()
        {
            if (Main.netMode != 1)
            {
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, new Vector2(NPC.position.X, NPC.Center.Y + 80), new Vector2(0, -.5f), ModContent.ProjectileType<SnowCloud>(), NPC.damage / 4, 8, NPC.target, 0, NPC.whoAmI);
            }
        }
        private void ShootUp(float xShift)
        {
            var theta = (Math.PI * 1.333) + (Main.rand.NextDouble() * Math.PI / 3);
            float mag = 6 + (Main.rand.NextFloat() * 3);
            var newPos = NPC.Center;
            newPos.Y -= 110;
            newPos.X += xShift;
            SoundEngine.PlaySound(SoundID.Item11, NPC.Center);
            var entitySource = new EntitySource_Parent(this.Entity);
            Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
        }
        #endregion

        #region OLDAttackAIs
        /*
        private void GunTwirlAI()
        {
            Slow();
            NPC.ai[0]++;
            if (NPC.ai[0] > 4 && NPC.ai[0] < 2 * 60)
            {
                AttackBool = true;
                if (NPC.ai[0] % 2 == 0 && Main.netMode != 1)
                {
                    var theta = Main.rand.NextDouble() * 2 * Math.PI;
                    float mag = 9 + (Main.rand.NextFloat() * 3);
                    var newPos = NPC.Center;
                    newPos.X -= ((NPC.ai[0] % 4 == 0 ? -1 : 1) * 66 * NPC.spriteDirection) - (50 * (float)Math.Cos(theta));
                    newPos.Y -= 36 - (50 * (float)Math.Sin(theta));

                    var entitySource = new EntitySource_Parent(this.Entity);
                    Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
                }
            }
            else
            {
                NPC.ai[1]++;
                AttackBool = false;
            }
            if (NPC.ai[0] > (2 * 60) + 12)
                Transition();
        }

        private void HatThrowAI()
        {
            Slow();
            NPC.ai[0]++;
            if (NPC.ai[0] == 3 * 6)
            {
                var newPos = NPC.Center;
                newPos.X -= 80 * NPC.spriteDirection;
                var entitySource = new EntitySource_Parent(this.Entity);
                NPC.ai[1] = Projectile.NewProjectile(entitySource, newPos, new Vector2(0, 0), ModContent.ProjectileType<HatSpin>(), NPC.damage / 4, 6f, NPC.target, 0, NPC.whoAmI);
            }
            if (NPC.ai[0] > 3 * 60 && (Main.projectile[(int)NPC.ai[1]].type != ModContent.ProjectileType<HatSpin>() || !Main.projectile[(int)NPC.ai[1]].active))
                Transition();
        }

        private void DashAI()
        {
            NPC.ai[0]++;
            if (NPC.ai[0] == 1)
                AttackBool = Main.rand.NextBool();
            if (NPC.ai[0] < 90)
            {
                MoveTowardsAtSpeed(TargetPos, Speed * 2);
                TargetPos = Main.player[NPC.target].Center;
                TargetPos.X += 600 * (AttackBool ? 1 : -1);
            }
            else if (NPC.ai[0] < 105)
                Slow();
            else if (NPC.ai[0] == 105)
            {
                AttackBool = NPC.Center.X < Main.player[NPC.target].Center.X;
                NPC.velocity.Y = 0;
            }
            if (NPC.ai[0] >= 105)
                NPC.velocity.X = AttackBool ? Speed * 4 : -Speed * 4;
            if (NPC.ai[0] >= 105 && Main.rand.Next(4) == 0 && Main.netMode != 1)
            {
                var theta = (Main.rand.NextDouble() * Math.PI) + Math.PI;
                float mag = 6 + (Main.rand.NextFloat() * 3);
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, NPC.Center, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
            }
            if (NPC.ai[0] > 3 * 60)
                Transition();
        }

        private void UpshootAI()
        {
            TargetPos = Main.player[NPC.target].Center;
            TargetPos.Y -= 200;
            MoveTowardsAtSpeed(TargetPos, Speed);
            NPC.ai[0]++;
            if (NPC.ai[0] % 3 == 0)
            {
                NPC.ai[1]++;
                switch (NPC.ai[1] % 4)
                {
                    case 1:
                        ShootUp(-NPC.spriteDirection * 28);
                        break;
                    case 2:
                        break;
                    case 3:
                        ShootUp(NPC.spriteDirection * 28);
                        break;
                    default:
                        break;
                }
            }
            if (NPC.ai[0] > 3 * 60)
                Transition();
        }


        private void SmashAI()
        {
            NPC.ai[0]++;
            if (NPC.ai[0] > 2 * 60)
            {
                CloudAlpha += .04f;
                if (CloudAlpha >= 1)
                {
                    CloudAlpha = 1;
                    bool floored = false;
                    NPC.ai[1]++;
                    if (NPC.ai[1] == 1)
                        NPC.velocity.Y = .5f;
                    if (NPC.velocity.Y > 0)
                        NPC.velocity.Y = Math.Min(NPC.velocity.Y + .5f, 16);
                    NPC.noTileCollide = false;
                    NPC.collideX = false;
                    NPC.collideY = true;
                    if (NPC.velocity.Y == 0)
                        floored = true;

                    NPC.velocity.X = 0;

                    if (floored)
                    {
                        if (!AttackBool)
                        {
                            AttackBool = true;
                            for (int k = 0; k < (Main.expertMode ? 20 : 10); k++)
                            {
                                var theta = Main.rand.NextDouble() * Math.PI;
                                float mag = -5 - (Main.rand.NextFloat() * 3);
                                var newPos = NPC.Center;
                                newPos.Y += (NPC.height / 2) - 8;
                                var entitySource = new EntitySource_Parent(this.Entity);
                                Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
                            }
                        }
                        NPC.velocity.Y = 0;
                    }
                }
                else
                    Slow();
            }
            else
            {
                TargetPos = Main.player[NPC.target].Center;
                TargetPos.Y -= 400;
                MoveTowardsAtSpeed(TargetPos, Speed * 2);
            }
            if (NPC.ai[1] > 2 * 60 || NPC.ai[0] > 5 * 60)
            {
                Transition();
                NPC.noTileCollide = true;
                NPC.collideX = false;
                NPC.collideY = false;
            }
        }

        private void ShotgunAI()
        {
            TargetPos = Main.player[NPC.target].Center;
            MoveTowardsAtSpeed(TargetPos, Speed);
            NPC.ai[0]++;
            if (NPC.ai[0] == 45)
            {
                var newPos = NPC.Center;
                newPos.X -= 80 * NPC.spriteDirection;
                SoundEngine.PlaySound(SoundID.Item36, NPC.Center);
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(6 * NPC.spriteDirection, -6), ModContent.ProjectileType<ShotgunYeet>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                for (int i = 0; i < 12; i++)
                {
                    var theta = (Math.PI * .67) + (Main.rand.NextDouble() * Math.PI / 2);
                    float mag = 8 + (Main.rand.NextFloat() * 6);
                    Projectile.NewProjectile(entitySource, newPos, new Vector2((float)Math.Cos(theta) * mag * NPC.spriteDirection, (float)Math.Sin(theta) * mag / 2), ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                }
            }
            if (NPC.ai[0] > 60)
                Transition();
        }

        private void SnowbombAI()
        {
            TargetPos = Main.player[NPC.target].Center;
            TargetPos.Y -= 200;
            MoveTowardsAtSpeed(TargetPos, Speed);
            NPC.ai[0]++;
            if (NPC.ai[0] == 45)
            {
                var newPos = NPC.Center;
                newPos.Y -= 20;
                newPos.X -= 40;
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(-2 + (Main.rand.NextFloat() * 4), -1 - Main.rand.NextFloat()), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                newPos.X += 80;
                Projectile.NewProjectile(entitySource, newPos, new Vector2(-2 + (Main.rand.NextFloat() * 4), -1 - Main.rand.NextFloat()), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
            }
            if (NPC.ai[0] > 72)
                Transition();
        }

        private void StewardAI()
        {
            NPC.ai[0]++;
            Slow();
            var dist = 120;
            var newPos = NPC.Center;
            newPos.Y -= dist;

            if (NPC.ai[0] == 15)
            {
                var entitySource = new EntitySource_Parent(this.Entity);
                Tracker = Projectile.NewProjectile(entitySource, newPos, new Vector2(), ModContent.ProjectileType<Steward>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Main.projectile[Tracker].scale = 1f / (2 * 60f);
            }
            if (NPC.ai[0] > 15)
                Main.projectile[Tracker].scale += 1f / (2 * 60f);
            if (NPC.ai[0] < (1 * 60) + 30)
            {
                var theta = Math.PI * 2 * Main.rand.NextDouble();
                var dustShift = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                int dust = Dust.NewDust(newPos + (dustShift * dist), 0, 0, ModContent.DustType<SnowAccumulateDust>(), 0, 0, 0, default, 1);
                Main.dust[dust].velocity = new Vector2(dustShift.X * -2, dustShift.Y * -2);
            }
            if (NPC.ai[0] > 2 * 60)
            {
                Main.projectile[Tracker].scale = 1;
                Transition();
            }
        }
        private void Transition()
        {
            PrevAttackPhase = CurrentPhase;
            //AIPhase = CurrentPhase.GrabGuns;
            NPC.ai[0] = NPC.ai[1] = 0;
            AttackBool = false;
        }
        #endregion

        #region OLDMovementAIs

        //private int GetAnger()
        //{
        //    float threshold = Main.expertMode ? 0.5f : 0.4f;
        //    int anger = NPC.life < NPC.lifeMax * threshold ? 1 : 0;
        //    anger += NPC.life < NPC.lifeMax * threshold / 2f ? 1 : 0;
        //    return anger;
        //}

        private void FollowAI()
        {
            NPC.ai[0]++;
            TargetPos = Main.player[NPC.target].Center;
            MoveTowardsAtSpeed(TargetPos, Speed);
            if (NPC.ai[0] > 2 * 60)
                Transition();
        }

        private void TransitionAI()
        {
            TargetPos = Main.player[NPC.target].Center;
            if (Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > 300)
                TargetPos.Y -= 200;
            MoveTowardsAtSpeed(TargetPos, Speed);
            NPC.ai[0]++;
            if (CloudAlpha > 0)
                CloudAlpha = Math.Max(0, CloudAlpha - .04f);
            if ((NPC.ai[0] > 2 * 60 && Main.rand.Next(60) == 0) || NPC.ai[0] > 4 * 60)
            {
                NPC.ai[0] = 0;
                CloudAlpha = 0;
                FrameDelay = 0;
                //AIPhase = GetNextAI();
            }
        }

        private int GetNextAI()
        {
            return FOLLOW;
            int nextAttack;
            if (Vector2.Distance(NPC.Center, Main.player[NPC.target].Center) > 1200)
                return STEWARD;
            if (NextAttacks.Count == 0)
            {
                for (int i = 0; i < Attacks.Count; i++)
                    NextAttacks.Add(Attacks[i]);
                if (Angry)
                {
                    NextAttacks.Add(UPSHOOT);
                    NextAttacks.Add(GUNTWIRL);
                }
                else
                {
                    NextAttacks.Add(SNOWBOMB);
                    NextAttacks.Add(SHOTGUN);
                    NextAttacks.Add(HATTHROW);
                }
            }
            int pos = Main.rand.Next(NextAttacks.Count);
            //if (NextAttacks[pos] == PrevAIPhase && NextAttacks.Count > 1)
            {
                pos++;
                if (pos >= NextAttacks.Count)
                    pos = 0;
            }
            if (NextAttacks.Contains(CLOUDS))
                pos = NextAttacks.IndexOf(CLOUDS);
            if (NextAttacks.Count > 0)
            {
                nextAttack = NextAttacks[pos];
                NextAttacks.RemoveAt(pos);
                return nextAttack;
            }
            return STEWARD;
        }

        private void SpawnClouds()
        {
            if (Main.netMode != 1)
            {
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, new Vector2(NPC.position.X - 2000, NPC.Center.Y + 80), new Vector2(0, -.5f), ModContent.ProjectileType<SnowCloud>(), NPC.damage / 4, 8, NPC.target, 0, NPC.whoAmI);
                Projectile.NewProjectile(entitySource, new Vector2(NPC.position.X + 2000, NPC.Center.Y + 80), new Vector2(0, -.5f), ModContent.ProjectileType<SnowCloud>(), NPC.damage / 4, 8, NPC.target, 0, NPC.whoAmI);
                Projectile.NewProjectile(entitySource, new Vector2(NPC.position.X, NPC.Center.Y + 80), new Vector2(0, -.5f), ModContent.ProjectileType<SnowCloud>(), NPC.damage / 4, 8, NPC.target, 0, NPC.whoAmI);
            }
        }*/
        #endregion

        private void SetBaseSpeed()
        {
            Speed = 2 + (Main.expertMode ? 1 : 0) + HealthPhase + (Main.player[NPC.target].ZoneSnow ? 0 : 2);
        }

        private void MoveTowardsAtSpeed(Vector2 targetPos, float mag)
        {
            Vector2 newVel = Vector2.Normalize(targetPos - NPC.Center);
            newVel *= Math.Min(Vector2.Distance(NPC.Center, targetPos) / 4, mag);
            NPC.velocity = newVel;
        }

        private void MoveTowards(Vector2 targetPos, float speed, float turnPower)
        {
            speed = MathHelper.Max(0f, speed);
            turnPower = MathHelper.Clamp(turnPower, 0f, MathHelper.Pi);

            if (speed == 0f)
            {
                NPC.velocity = Vector2.Zero;
                return;
            }

            Vector2 toTarget = targetPos - NPC.Center;

            if (toTarget.LengthSquared() < 0.0001f)
            {
                NPC.velocity = NPC.velocity.SafeNormalize(Vector2.UnitX) * speed;
                return;
            }

            if (NPC.velocity.LengthSquared() < 0.0001f)
            {
                NPC.velocity = toTarget.SafeNormalize(Vector2.UnitX) * speed;
                return;
            }

            float currentAngle = NPC.velocity.ToRotation();
            float targetAngle = toTarget.ToRotation();

            // Wrap so it takes the shortest turn across the +/- PI boundary.
            float angleDifference = MathHelper.WrapAngle(targetAngle - currentAngle);
            float turnAmount = MathHelper.Clamp(
                angleDifference,
                -turnPower,
                turnPower
            );

            NPC.velocity =
                NPC.velocity.SafeNormalize(Vector2.UnitX)
                .RotatedBy(turnAmount) * speed;
        }

        private void FacePlayer()
        {
            NPC.spriteDirection = NPC.Center.X < Main.player[NPC.target].Center.X ? -1 : 1;
        }
        //private bool MoveTowardsAtSpeed(Vector2 targetPos, float mag)
        //{
        //    //Vector2 newVel = targetPos - NPC.Center;
        //    //float angle = newVel.ToRotation();
        //    //float curAngle = NPC.velocity.ToRotation();
        //    //float newAngle = curAngle.AngleTowards(angle, .2f / Math.Max(mag, 0.2f));
        //    NPC.velocity = Vector2.Lerp(NPC.velocity, (targetPos - NPC.Center).SafeNormalize(Vector2.Zero) * mag, .1f);
        //    //NPC.velocity = newAngle.ToRotationVector2() * Math.Min(Vector2.Distance(NPC.Center, targetPos) / 25, mag/*Math.Min(mag, NPC.velocity.Length() + .2f)*/);
        //    return Vector2.Distance(NPC.Center, targetPos) < 4;
        //}

        private void Slow()
        {
            NPC.velocity *= .95f;
        }

        #region MiscMethods
        private void TriggerScreenshake(float strength, int time)
        {
            PunchCameraModifier modifier = new PunchCameraModifier(NPC.Center, (Main.rand.NextFloat() * ((float)Math.PI * 2f)).ToRotationVector2(), strength, 6f, time);
            Main.instance.CameraModifiers.Add(modifier);
        }

        private void InitializeAI()
        {
            NPC.TargetClosest(false);
            TargetPos = Main.player[NPC.target].Center;
            CurrentPhase = AIPhase.RainChase;
            NPC.ai[0] = 0;
        }

        private void HealthEffects()
        {
            if (NPC.life < NPC.lifeMax / 2 && HealthPhase == 1)
            {
                FrameDelay = 0;
                HealthPhase = 0;
                CurrentPhase = AIPhase.GrabGuns;
                NPC.ai[0] = 0;
                NPC.ai[0] = 1;
                NextAttacks.Clear();
            }
        }

        private void Effects()
        {
            if (NPC.velocity.X < 0)
                NPC.spriteDirection = 1;
            if (NPC.velocity.X > 0)
                NPC.spriteDirection = -1;
            if (NPC.velocity.X == 0)
                NPC.spriteDirection *= -1;
            NPC.netUpdate = true;
        }

        private void CheckDespawnAI()
        {
            if (Main.player[NPC.target].statLife < 1 || Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 4000)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].statLife < 1 || Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 4000)
                {
                    if (despawn == 0)
                        despawn++;
                }
                else
                    despawn = 0;
            }
            if (despawn >= 1)
            {
                despawn++;
                NPC.noTileCollide = true;
                NPC.velocity.Y = 8f;
                if (despawn >= 300)
                    NPC.active = false;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            //if (NewDawnWorld.downedEtheria)
            //    target.AddBuff(ModContent.BuffType<Frostbite>(), 4 * 60, true);
            if (Main.expertMode)
            {
                target.AddBuff(BuffID.Frostburn, (2 * 60) + Main.rand.Next(60), true);
                target.AddBuff(BuffID.Chilled, 60, true);
            }
        }
        #endregion

        #region Frames
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            GetFrame();
            var spriteFX = SpriteEffects.None;
            if (NPC.spriteDirection == -1)
                spriteFX = SpriteEffects.FlipHorizontally;
            Color color = Lighting.GetColor((int)((double)NPC.position.X + ((double)NPC.width * 0.5)) / 16, (int)(((double)NPC.position.Y + ((double)NPC.height * 0.5)) / 16.0));
            Vector2 npcDimensions = new Vector2((float)(TextureAssets.Npc[NPC.type].Value.Width / 2), (float)(TextureAssets.Npc[NPC.type].Value.Height / Main.npcFrameCount[NPC.type] / 2));
            Vector2 drawPos = new Vector2(NPC.position.X - Main.screenPosition.X + (float)(NPC.width / 2) - ((float)NPC_WIDTH * NPC.scale / 2f) + (npcDimensions.X * NPC.scale), NPC.position.Y - Main.screenPosition.Y + (float)NPC.height - ((float)NPC_WIDTH * NPC.scale / (float)Main.npcFrameCount[NPC.type]) + 4f + (npcDimensions.Y * NPC.scale) + NPC.gfxOffY);

            DrawCloud(spriteBatch, color, npcDimensions, drawPos, true);
            spriteBatch.Draw(AnimFrames, drawPos, new Microsoft.Xna.Framework.Rectangle?(NPC.frame), NPC.GetAlpha(color), NPC.rotation, npcDimensions, NPC.scale, spriteFX, 0f);
            DrawCloud(spriteBatch, color, npcDimensions, drawPos, false);
            if (CurrentPhase == AIPhase.SpinCycle && AttackBool)
                DrawGunSpin(spriteBatch, color, npcDimensions, drawPos);
            return false;
        }

        private void DrawGunSpin(SpriteBatch spriteBatch, Color color, Vector2 dimensions, Vector2 drawPos)
        {
            int frameCount = 4;
            float animRate = 3;
            Rectangle rect = new Rectangle(0, 112 * (int)Math.Min(Math.Floor(NPC.ai[0] % (frameCount * animRate) / animRate), frameCount - 1), 110, 112);
            var spriteFX = SpriteEffects.None;
            var texture = GunSpin;
            drawPos.Y += 24;
            drawPos.X -= 20;
            spriteBatch.Draw(texture, drawPos, rect, color, NPC.rotation, dimensions, NPC.scale, spriteFX, 0f);
            drawPos.X += 120;
            spriteFX = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, drawPos, rect, color, NPC.rotation, dimensions, NPC.scale, spriteFX, 0f);
        }

        private void DrawCloud(SpriteBatch spriteBatch, Color color, Vector2 dimensions, Vector2 drawPos, bool back)
        {
            int frameCount = 8;
            float animRate = 16;
            CloudFrame++;
            while (CloudFrame >= frameCount * animRate)
                CloudFrame -= (int)(frameCount * animRate);
            Rectangle rect = new Rectangle(0, CLOUD_HEIGHT * (int)Math.Min(Math.Floor(CloudFrame / animRate), frameCount - 1), NPC_WIDTH, CLOUD_HEIGHT);
            var spriteFX = SpriteEffects.None;
            if (NPC.spriteDirection == -1)
                spriteFX = SpriteEffects.FlipHorizontally;
            drawPos.X += 0;
            drawPos.Y += 138;
            var texture = CloudFront;
            if (back)
                texture = CloudBack;
            var newColor = new Color((color.R / 255f) - CloudAlpha, (color.G / 255f) - CloudAlpha, (color.B / 255f) - CloudAlpha, (color.A / 255f) - CloudAlpha);
            //var newColor = new Color(1 - CloudAlpha, 1 - CloudAlpha, 1 - CloudAlpha, 1 - CloudAlpha);
            spriteBatch.Draw(texture, drawPos, rect, newColor, NPC.rotation, dimensions, NPC.scale, spriteFX, 0f);
        }

        private void GetFrame()
        {
            NPC.frame.Width = NPC_WIDTH;
            NPC.frame.Height = NPC_WIDTH;
            NPC.frame.X = 0;
            NPC.frame.Y = 0;

            FrameDelay++;

            if (CurrentPhase == AIPhase.GrabGuns && !AttackBool)
            {
                int frameCount = 20;
                float animRate = 6;

                NPC.frame.X = NPC_WIDTH * ((NPC.ai[0] > frameCount * animRate / 2) ? 2 : 1);
                NPC.frame.Y = NPC_WIDTH * (int)Math.Min(Math.Floor((NPC.ai[0] > frameCount * animRate / 2) ? (NPC.ai[0] - (frameCount * animRate / 2)) / animRate : NPC.ai[0] / animRate), 9);

                if (NPC.ai[0] > frameCount * animRate)
                {
                    //NPC.ai[0] -= (int)(frameCount * animRate);
                    HealthPhase = 2;
                    AttackBool = true;
                    NextMovementAttacks.Clear();
                    NextStationaryAttacks.Clear();
                    PhaseChange();
                }
                return;
            }

            if (AttackFrames())
                return;

            if (HealthPhase == 1)
            {
                int frameCount = 10;
                float animRate = 6;
                while (FrameDelay > frameCount * animRate)
                    FrameDelay -= (int)(frameCount * animRate);

                NPC.frame.X = 0;
                NPC.frame.Y = NPC_WIDTH * (int)Math.Min(Math.Floor(FrameDelay / animRate), frameCount - 1);
            }

            if (HealthPhase == 2)
            {
                int frameCount = 10;
                float animRate = 6;
                while (FrameDelay > frameCount * animRate)
                    FrameDelay -= (int)(frameCount * animRate);

                NPC.frame.X = NPC_WIDTH * 3;
                NPC.frame.Y = NPC_WIDTH * (int)Math.Min(Math.Floor(FrameDelay / animRate), frameCount - 1);
            }
        }

        private bool AttackFrames()
        {
            if (CurrentPhase == AIPhase.SpinCycle)
            {
                int frameCount = 5;
                float animRate = 4;

                NPC.frame.X = NPC_WIDTH * 6;
                NPC.frame.Y = NPC_WIDTH * (5 + (int)Math.Min(Math.Floor(NPC.ai[1] / animRate), frameCount - 1));

                return true;
            }
            if (CurrentPhase == AIPhase.HatThrow)
            {
                int frameCount = 7;
                float animRate = 6;

                NPC.frame.X = NPC_WIDTH * 7;
                NPC.frame.Y = NPC_WIDTH * (int)Math.Min(Math.Floor(NPC.ai[0] / animRate), frameCount - 1);

                return true;
            }
            if (CurrentPhase == AIPhase.UpwardSpray)
            {
                int frameCount = 4;

                NPC.frame.X = NPC_WIDTH * 6;
                NPC.frame.Y = NPC_WIDTH * (int)Math.Min(NPC.ai[1] % 4, frameCount - 1);

                return true;
            }
            if (CurrentPhase == AIPhase.ShotgunDash && AttackBool)
            {
                int frameCount = 6;
                float animRate = 6;

                NPC.frame.X = NPC_WIDTH * 4;
                NPC.frame.Y = NPC_WIDTH * (int)Math.Min(Math.Floor(((int)NPC.ai[0] - 30) / animRate), frameCount - 1);

                if (NPC.ai[0] >= 30 && NPC.ai[0] < 45 + 30)
                    return true;
                return false;
            }
            if ((CurrentPhase == AIPhase.Boobomb || CurrentPhase == AIPhase.BoobombSpray) && NPC.ai[0] > 90)
            {
                int frameCount = 10;
                float animRate = 6;

                float frame = NPC.ai[0] - 90;
                //if (frame > animRate * 5)
                //{
                //    frame = animRate * 5;
                //    //if (NPC.ai[0] > animRate * 7)
                //    //    frame = NPC.ai[0] - (animRate * 2);
                //}
                NPC.frame.X = NPC_WIDTH * 5;
                NPC.frame.Y = NPC_WIDTH * (int)Math.Min(Math.Floor(frame / animRate), frameCount - 1);

                if (frame < 60)
                    return true;
                return false;
            }
            return false;
        }
        #endregion

        #region Loot
        public override void OnKill()
        {
            if (NewDawnWorld.downedEtheria)
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<global::NewDawn.Items.Loot.EtherialFrost>());

            if (Main.rand.NextBool(10))
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<global::NewDawn.Items.Placeable.HypothemaTrophy>());

            NewDawnWorld.downedHypothema = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }
        #endregion
    }
}
