using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Dusts;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    [AutoloadBossHead]
    public class Hypothema : ModNPC
    {
        #region Initializations
        public int despawn = 0;
        static int NPC_WIDTH = 194, CLOUD_HEIGHT = 66;
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
            ShootChase,
            FlyHigh,
            SpinCycle,
            DashIntoAttack,
            DoubleSteward,
            UpwardSpray,
            BoobombSpray,
            GrabGuns
        }
        //private List<int> Attacks;
        //private List<int> NextAttacks;
        public int HealthPhase { get; set; } = 1;
        private AIPhase PrevAttackPhase { get; set; }
        private AIPhase PrevAIPhase { get; set; }
        public AIPhase CurrentPhase { get; set; }
        private List<AIPhase> Phase1MovementAttacks = new List<AIPhase> { AIPhase.ShotgunDash, AIPhase.HatThrow, AIPhase.DashChase },
            Phase1StationaryAttacks = new List<AIPhase> { AIPhase.Steward, AIPhase.Boobomb, AIPhase.Stomp },
            Phase2MovementAttacks = new List<AIPhase> { AIPhase.ShootChase, AIPhase.FlyHigh, AIPhase.SpinCycle },
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
            NPC.npcSlots = 10f;
            PrevAIPhase = CurrentPhase = AIPhase.RainChase;
            HealthPhase = 1;
            FrameDelay = 0;
            CloudFrame = 0;
            CloudAlpha = 0;
            NPC.ai[0] = NPC.ai[1] = NPC.ai[2] = 0;
            Tracker = 0;
            Speed = 2;
            AttackBool = false;
            //Attacks = new List<int> { STEWARD, SMASH, DASH, FOLLOW, CLOUDS };
            //NextAttacks = new List<int>();
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

                AnimFrames = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/Hypothema_Frames");
                CloudFront = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/HypothemaCloud");
                CloudBack = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/HypothemaCloudOutline");
                GunSpin = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/GunSpin");
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
        private void PhaseChange(bool skipPhaseSelect = false)
        {
            Offset = Vector2.Zero;
            NPC.ai[0] = 0;
            NPC.ai[1] = 0;
            NPC.ai[2] = 0;
            AttackBool = false;
            CloudAlpha = 0;
            if (!skipPhaseSelect)
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
                case AIPhase.ShootChase:
                    ShootChase();
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
                        GetMovementAttack();
                    }
                    else if (Phase2MovementAttacks.Contains(CurrentPhase))
                    {
                        PrevAIPhase = CurrentPhase;
                        GetStationaryAttack();
                    }
                    else if (Phase2StationaryAttacks.Contains(CurrentPhase))
                    {
                        PrevAIPhase = CurrentPhase;
                        CurrentPhase = AIPhase.RainChase2;
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
            if (NextStationaryAttacks.Count == 0)
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
            CurrentPhase = NextStationaryAttacks[i]; // out of range??
            NextStationaryAttacks.RemoveAt(i);
        }
        #endregion

        #region Phase AIs
        private void RainChase()
        {
            if (Offset == Vector2.Zero)
            {
                if (NPC.Center.X < Main.player[NPC.target].Center.X)
                    Offset = new Vector2(600, -400);
                else
                    Offset = new Vector2(-600, -400);
            }
            if ((TargetPos.X < Main.player[NPC.target].Center.X && NPC.Center.X < TargetPos.X) ||
                (TargetPos.X > Main.player[NPC.target].Center.X && NPC.Center.X > TargetPos.X))
            {
                Offset.X *= -1;
                NPC.ai[1]++;
                NPC.ai[0] = 0;
            }
            TargetPos = Main.player[NPC.target].Center + Offset;
            RainSnow();
            NPC.ai[0]++;
            if (NPC.ai[2] < NPC.ai[0] && NPC.ai[2] < 3 * 60)
                NPC.ai[2]++;
            else
                NPC.ai[2]--;

            var tempVector = new Vector2((int)NPC.ai[2], 0);
            if (Offset.X < 0)
                tempVector.X *= -1;
            var mult = Math.Min(NPC.ai[2] / 60f, 3);
            MoveTowards(TargetPos + tempVector, Speed * mult, 0.3f);
            if (NPC.ai[1] > 1)
                PhaseChange();
        }

        private void ShotgunDash()
        {
            if (Offset == Vector2.Zero)
            {
                if (NPC.Center.X < Main.player[NPC.target].Center.X)
                    Offset = new Vector2(400, -200);
                else
                    Offset = new Vector2(-400, -200);
                AttackBool = false; //AttackBool tracks if shotgun is being fired
                NPC.ai[0] = 0;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
            }
            FacePlayer();

            if (NPC.ai[0] > 0)
            {
                Slow();
                NPC.ai[0]++;
            }
            else
            {
                TargetPos = Main.player[NPC.target].Center + Offset;
                MoveTowards(TargetPos, Speed * 3, 0.3f);

                if ((TargetPos.X < Main.player[NPC.target].Center.X && NPC.Center.X < TargetPos.X) ||
                    (TargetPos.X > Main.player[NPC.target].Center.X && NPC.Center.X > TargetPos.X))
                    NPC.ai[0]++;
            }


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
                if (NPC.ai[1] > 3)
                    PhaseChange();
            }
            if (Main.expertMode)
                RainSnow();
        }

        private void HatThrow()
        {
            FacePlayer();

            NPC.ai[0]++;
            if (NPC.ai[0] < 3f * 60)
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
            else if (NPC.ai[0] < 8 * 60)
            {
                if (Main.projectile[(int)NPC.ai[1]].type != ModContent.ProjectileType<HatSpin>() || !Main.projectile[(int)NPC.ai[1]].active)
                    PhaseChange();
                else
                {
                    Offset = new Vector2(Main.player[NPC.target].velocity.X * 30, -400);
                    TargetPos = Main.player[NPC.target].Center + Offset;
                    MoveTowardsAtSpeed(TargetPos, Speed * 3);
                    RainSnow();
                    FacePlayer();
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
            TargetPos = Main.player[NPC.target].Center;
            if (Main.expertMode)
                TargetPos += Main.player[NPC.target].velocity * 10;

            if ((NPC.Center + NPC.velocity - TargetPos).Length() > (NPC.Center - NPC.velocity - TargetPos).Length() && (NPC.Center - TargetPos).Length() > 200)
                NPC.ai[1] -= 0.1f;
            else
                NPC.ai[1] += 0.025f;
            NPC.ai[1] = MathHelper.Clamp(NPC.ai[1], 2, 5);
            MoveTowards(TargetPos, Speed * NPC.ai[1], MathHelper.ToRadians((NPC.Center - TargetPos).Length() / 120 / NPC.ai[1]));
            RainSnow();
            if (Main.expertMode)
                RainSnow();

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
            if ((NPC.Center - TargetPos).Length() > 30 && NPC.ai[0] == 0)
            {
                MoveTowardsAtSpeed(TargetPos, Speed * 3f);
            }
            else
            {
                Slow();
                NPC.ai[0]++;
            }
            if (NPC.ai[0] == 30 + 36)
            {
                var newPos = NPC.Center;
                newPos.Y -= 20;
                newPos.X -= 40;
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, newPos, new Vector2(-2 - (Main.rand.NextFloat() * 4), -1 - Main.rand.NextFloat()), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                newPos.X += 80;
                Projectile.NewProjectile(entitySource, newPos, new Vector2(2 + (Main.rand.NextFloat() * 4), -1 - Main.rand.NextFloat()), ModContent.ProjectileType<Snowbomb>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
            }
            if (NPC.ai[0] >= (1 * 60) + 30)
                PhaseChange();
            RainSnow();
        }

        private void Stomp()
        {
            if (NPC.ai[0] == 0)
            {
                Offset = new Vector2(0, -400);
                MoveTowardsAtSpeed(Main.player[NPC.target].Center + Offset, Speed * 3);
            }
            if ((NPC.Center - TargetPos).Length() < 20 || NPC.ai[0] > 0)
            {
                NPC.ai[0]++;
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
                MoveTowardsAtSpeed(TargetPos, Speed * 3);
            }
            if (AttackBool)
                NPC.ai[2]++;
            if (NPC.ai[2] > 60)
            {
                PhaseChange();
                NPC.noTileCollide = true;
                NPC.collideX = false;
                NPC.collideY = false;
            }

            if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 6)
                NPC.spriteDirection = -1;
            else
                FacePlayer();
        }

        private void RainChase2()
        {
            if (Offset == Vector2.Zero)
            {
                if (NPC.Center.X < Main.player[NPC.target].Center.X)
                    Offset = new Vector2(600, -400);
                else
                    Offset = new Vector2(-600, -400);
                Tracker = 0;
            }
            if ((TargetPos.X < Main.player[NPC.target].Center.X && NPC.Center.X < TargetPos.X) ||
                (TargetPos.X > Main.player[NPC.target].Center.X && NPC.Center.X > TargetPos.X))
            {
                Offset.X *= -1;
                NPC.ai[1]++;
                NPC.ai[0] = 0;
            }
            TargetPos = Main.player[NPC.target].Center + Offset;
            //RainSnow();
            RainSnow();
            NPC.ai[0]++;
            Tracker++;
            if (NPC.ai[2] < NPC.ai[0] && NPC.ai[2] < 3 * 60)
                NPC.ai[2]++;
            else
                NPC.ai[2]--;

            if (NPC.ai[0] % 3 == 0 && Tracker < 3 * 60)
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
            var tempVector = new Vector2((int)NPC.ai[2], 0);
            if (Offset.X < 0)
                tempVector.X *= -1;
            var mult = Math.Min(NPC.ai[2] / 50f, 4);
            MoveTowards(TargetPos + tempVector, Speed * mult, 0.3f);
            if (NPC.ai[1] > 1 || Tracker > 4 * 60)
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

        private void ShootChase()
        {
            if (Offset == Vector2.Zero)
            {
                if (NPC.Center.Y < Main.player[NPC.target].Center.Y)
                    Offset = new Vector2(0, -200);
                else
                    Offset = new Vector2(0, 200);
            }
            NPC.ai[0]++;
            if (NPC.ai[0] % 120 == 0)
            {
                Offset.Y *= -1;
            }
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
            TargetPos = Main.player[NPC.target].Center + Offset;
            //RainSnow();
            if (Main.expertMode)
                RainSnow();
            MoveTowards(TargetPos, Speed * 1.5f, .03f);
            if (NPC.ai[0] > 8 * 60)
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

            if ((NPC.Center - TargetPos).Length() > 20 && NPC.ai[0] == 0)
            {
                RainSnow();
                RainSnow();
                MoveTowardsAtSpeed(TargetPos, Speed * 3);
            }
            else if (NPC.ai[0] == 0)
            {
                TriggerScreenshake(15, 5);
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                NPC.ai[0]++;
            }

            if (NPC.ai[0] > 0)
            {
                NPC.ai[0]++;
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
                {
                    Slow();
                    NPC.spriteDirection = 1;
                }
                if (AttackBool)
                    CloudAlpha = Math.Max(CloudAlpha - .02f, 0);
            }

            if (AttackBool)
                NPC.ai[2]++;

            if (NPC.ai[2] > 60)
            {
                PhaseChange();
                NPC.noTileCollide = true;
                NPC.collideX = false;
                NPC.collideY = false;
            }


            if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) < 6)
                NPC.spriteDirection = -1;
            else
                FacePlayer();
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
                if (NPC.ai[0] % 6 == 0 && Main.netMode != 1)
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
            if (NPC.ai[0] == 0)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
                TriggerScreenshake(15, 5);
            }
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
            Slow();
            var dist = 160;
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
            if (Offset == Vector2.Zero)
            {
                if (NPC.Center.X < Main.player[NPC.target].Center.X)
                    Offset = new Vector2(400, -300);
                else
                    Offset = new Vector2(-400, -300);
                SoundEngine.PlaySound(SoundID.NPCDeath15, NPC.Center);
                var entitySource = new EntitySource_Parent(this.Entity);
                NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MiniHypo>());
            }

            NPC.ai[0]++;
            if (!AttackBool)
            {
                TargetPos = Main.player[NPC.target].Center + Offset;
                TargetPos.Y -= 100;
                MoveTowardsAtSpeed(TargetPos, Speed * 2);
            }
            //else
            //{
            //    var yOffsetGoal = Main.player[NPC.target].Center.Y - 300;
            //    if (NPC.Center.Y > yOffsetGoal)
            //        TargetPos = new Vector2(Main.player[NPC.target].Center.X, yOffsetGoal);
            //    else
            //        TargetPos = Main.player[NPC.target].Center + Offset;
            //    TargetPos.X = NPC.Center.X;
            //    MoveTowardsAtSpeed(TargetPos, Speed * 2);
            //    FacePlayer();
            //}

            if ((NPC.Center - TargetPos).Length() < 16 && !AttackBool)
            {
                AttackBool = true;
                SoundEngine.PlaySound(SoundID.NPCDeath15, NPC.Center);
                var entitySource = new EntitySource_Parent(this.Entity);
                NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<MiniHypo>());
            }
            //Slow();
            if (NPC.ai[0] % 3 == 0)
            {
                NPC.ai[1]++;
                switch (NPC.ai[1] % 4)
                {
                    case 1:
                        ShootUp(-NPC.spriteDirection * 28, true);
                        break;
                    case 2:
                        break;
                    case 3:
                        ShootUp(NPC.spriteDirection * 28, true);
                        break;
                    default:
                        break;
                }
            }

            RainSnow();
            if (Main.expertMode)
                RainSnow();

            if (NPC.ai[0] > 8 * 60)
                PhaseChange();
        }

        private void BoobombSpray()
        {
            TargetPos = Main.player[NPC.target].Center;
            TargetPos.Y -= 250;
            if ((NPC.Center - TargetPos).Length() > 30 && NPC.ai[0] == 0)
            {
                MoveTowardsAtSpeed(TargetPos, Speed * 2.5f);
            }
            else
            {
                Slow();
                NPC.ai[0]++;
            }
            if (NPC.ai[0] == 30)
            {
                Offset.X = -30;
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, NPC.Center + Offset, new Vector2(6 * NPC.spriteDirection, -6), ModContent.ProjectileType<ShotgunYeet>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
                Projectile.NewProjectile(entitySource, NPC.Center - Offset, new Vector2(-6 * NPC.spriteDirection, -6), ModContent.ProjectileType<ShotgunYeet>(), NPC.damage / 4, 8f, 0, NPC.whoAmI);
            }
            if (NPC.ai[0] == 30 + 36)
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
            if (NPC.ai[0] >= (1 * 60) + 30)
            {
                NPC.ai[0] = 0;
                CurrentPhase = AIPhase.GrabGuns;
                PhaseChange(true);
            }
            RainSnow();
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
        private void ShootUp(float xShift, bool seeking = false)
        {
            var newPos = NPC.Center;
            newPos.Y -= 110;
            newPos.X += xShift;
            SoundEngine.PlaySound(SoundID.Item11, NPC.Center);
            var entitySource = new EntitySource_Parent(this.Entity);
            if (seeking)
            {
                var player = Main.player[NPC.target];
                var toPlayer = player.Center - newPos;

                var theta = (Math.PI * 1.25) + (Main.rand.NextDouble() * Math.PI / 4);
                float mag = 6 + (Main.rand.NextFloat() * 3);
                mag /= 3f;
                var shootVel = new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag);
                shootVel.Y -= 4;
                shootVel.X -= toPlayer.X / (2 * shootVel.Y / SnowballNoSlow.Gravity);
                Projectile.NewProjectile(entitySource, newPos, shootVel, ModContent.ProjectileType<SnowballNoSlow>(), NPC.damage / 4, 7, NPC.target);
            }
            else
            {
                var theta = (Math.PI * 1.333) + (Main.rand.NextDouble() * Math.PI / 3);
                float mag = 6 + (Main.rand.NextFloat() * 3);
                var randomVel = new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag);
                Projectile.NewProjectile(entitySource, newPos, randomVel, ModContent.ProjectileType<Snowball>(), NPC.damage / 4, 7);
            }
        }
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
            if (Math.Abs(NPC.velocity.X) < .001f)
                NPC.velocity.X = 0.001f * Math.Sign(NPC.velocity.X);
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
            NPC.spriteDirection = (NPC.Center.X < Main.player[NPC.target].Center.X - 6) ? -1 : 1;
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
            FacePlayer();
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
                PhaseChange(true);
                NPC.ai[0] = 0;
                NPC.ai[0] = 1;
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
            if (CurrentPhase == AIPhase.UpwardSpray || CurrentPhase == AIPhase.ShootChase)
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
            if ((CurrentPhase == AIPhase.Boobomb || CurrentPhase == AIPhase.BoobombSpray) && NPC.ai[0] > 30)
            {
                int frameCount = 10;
                float animRate = 6;

                float frame = NPC.ai[0] - 30;
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
