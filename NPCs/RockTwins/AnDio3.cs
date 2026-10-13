using Microsoft.Xna.Framework;
using NewDawn.Items.Loot;
using NewDawn.Items.Placeable;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace NewDawn.NPCs.RockTwins
{
    [AutoloadBossHead]
    public class AnDio3 : ModNPC
    {
        public bool zImmune = true;
        public static Random rnd = new Random();
        public int spawn = 0;
        public bool stage2 = false;
        public int phase = 0;
        public int dir = 0;
        public int vdir = 0;
        public float accel = 0f;
        public float maxAccel = 20f;
        public float vaccel = 0f;
        public float maxVaccel = 20f;
        public bool boosted = false;
        public int delay = 0;
        public int maxDelay = 60;
        public int range = 2000;
        public int damage = 0;
        public int shoot = 0;
        public int reload = 160;
        public int moveType = 1;
        public int hovDir = 1;
        public int moveDelay = 600;
        public int vDir = 2;
        public bool bitherial = true;
        public int plays = 0;
        public static int laser = 0;
        public int laserCharge = 0;
        public static Vector2 center;
        public static Vector2 position;
        public static Player target;
        public static bool andio = false;
        public static float theta = 0f;
        public float rotSpeed = 1f;
        public static double posX = 0;
        public static double posY = 0;
        public static int dist = 0;
        public static int laserBallNum;
        public int spiralDur = 0;
        public int spiralDelay = 0;
        public float tVel = 0f;
        public float vel = 0f;
        public float distance = 0;
        public bool anim = false;
        public int attacks = 0;
        public int animPhase = 1;
        public int attackDuration = 0;
        public int attackNum = 0;
        public int anDel = 0;
        int _despawn = 0;

        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            NewDawnVars.zNPCs.Add(NPC.type);
            // DisplayName.SetDefault("AnDio");
        }

        public override void SetDefaults()
        {
            _despawn = 0;
            anDel = 0;
            attackNum = 0;
            attackDuration = 0;
            zImmune = true;
            animPhase = 1;
            attacks = 0;
            anim = true;
            distance = 0;
            tVel = 0f;
            vel = 0f;
            spiralDur = 0;
            spiralDelay = 0;
            laserBallNum = 1;
            rotSpeed = 1f;
            theta = 0f;
            laserCharge = 0;
            andio = false;
            laser = 0;
            plays = 1;
            bitherial = true;
            moveDelay = 600;
            hovDir = 1;
            vDir = 1;
            moveType = 1;
            shoot = 0;
            reload = 380;
            phase = 1;
            damage = 0;
            maxDelay = 60;
            range = 200;
            maxAccel = 14f;
            maxVaccel = 20f;
            accel = 0f;
            vaccel = 0f;
            spawn = 0;
            dir = 0;
            vdir = 0;
            delay = reload;
            NPC.width = 140;
            NPC.height = 156;
            NPC.damage = 64;
            NPC.defense = 20;
            NPC.aiStyle = 0;
            NPC.lifeMax = 5000;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath1;
            //Main.npcFrameCount[npc.type] = 11;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 99f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = global::Terraria.ModLoader.MusicLoader.GetMusicSlot(Mod, "Sounds/Music/AnDio");
            damage = 32;
            //npc.scale = 2f;
            //npc.dontTakeDamage = true;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            plays = numPlayers;
            NPC.lifeMax = 10000 + (numPlayers * 2500);
            NPC.damage = 88;
            reload = 300;
            delay = reload;
            damage = 40;
        }

        private void DespawnCheck()
        {
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
                {
                    if (_despawn == 0)
                        _despawn++;
                }
            }
            else
                _despawn = 0;
            if (_despawn >= 1)
            {
                _despawn++;
                NPC.noTileCollide = true;
                NPC.velocity.Y = 8f;
                if (_despawn >= 300)
                    NPC.active = false;
            }
        }

        private void Retarget()
        {
            Player p = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;
        }

        public override void AI()
        {
            DespawnCheck();
            Retarget();
            bitherial = true;
            if (anim == true)
            {
                anDel++;
                if (anDel >= 120)
                    anim = false;
            }
            NPC.spriteDirection = 0;
            center = NPC.Center;
            position = NPC.position;
            bitherial = true;

            //DESPAWN
            int despawn = 0;
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead || Main.player[NPC.target].ZoneRockLayerHeight == false)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead || Main.dayTime)
                {
                    if (despawn == 0)
                        despawn++;
                }
            }
            if (despawn >= 1)
            {
                despawn++;
                //ResetValues();
                NPC.noTileCollide = true;
                NPC.velocity.Y = 8f;
                if (despawn >= 300)
                    NPC.active = false;
            }


            Vector2 delta = Main.player[NPC.target].Center - NPC.Center;
            float magnitude = (float)Math.Sqrt((delta.X * delta.X) + (delta.Y * delta.Y));
            NPC.spriteDirection = 0;
            target = Main.player[NPC.target];

            posX = NPC.Center.X;
            posY = NPC.Center.Y;

            NPC.netUpdate = true;
            //if (anim == false)
            //{
            //Checking which direction to move when spawned
            if (dir == 0)
            {
                if (delta.X < 0) dir = 1;
                else dir = -1;
            }

            //Moving across top of screen
            if (moveType == 1)
            {
                //attacking = false;
                //Horizontal Movement
                NPC.velocity.X = accel;
                if (NPC.position.X < Main.player[NPC.target].position.X - 400 && hovDir == -1)
                {
                    hovDir = 1;
                    /*if (npc.life > npc.lifeMax / 2)
                        shoot = 1;
                    else
                        shoot = 2;*/
                    attacks += 1;
                }
                if (NPC.position.X > Main.player[NPC.target].position.X + 400 && hovDir == 1)
                {
                    hovDir = -1;
                    /*if (npc.life > npc.lifeMax / 2)
                        shoot = 1;
                    else
                        shoot = 2;*/
                    attacks += 1;
                }
                if (Math.Abs(accel) < maxAccel) { accel += (float)hovDir / 4f; }
                else { accel *= .9f; }

                //Vertical Movement
                NPC.velocity.Y = vaccel;
                if (NPC.position.Y - Main.player[NPC.target].position.Y + 280 > 0) { vDir = -1; }
                if (NPC.position.Y - Main.player[NPC.target].position.Y + 280 < 0) { vDir = 1; }
                if (Math.Abs(vaccel) < maxVaccel / 4) { vaccel += (float)vDir / 3f; }
                else { vaccel *= .2f; }
                NPC.velocity.Y = vaccel;
                if (boosted == false)
                {
                    if (Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y + 280) > 20)
                    {
                        if (NPC.position.Y - Main.player[NPC.target].position.Y + 280 < 0)
                        {
                            if (vaccel < maxVaccel) vaccel += .4f;
                        }
                        if (NPC.position.Y - Main.player[NPC.target].position.Y + 280 > 0)
                        {
                            if (vaccel < maxVaccel) vaccel -= .4f;
                        }
                    }
                    else
                    {
                        if (Math.Abs(vaccel) > .01f) vaccel *= .9f;
                        else vaccel = 0f;
                    }
                }
                else vaccel = 0;

                //Attack 2
                if (Math.Abs(delta.X) < 10 && attacks >= 8 && moveType == 1)
                {
                    attacks = 0;
                    moveType = 2;
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                }

            }

            //Dash down
            if (moveType == 2)
            {
                //attacking = true;
                NPC.velocity.Y = 14;
                NPC.velocity.X = 0;
                vaccel = 0;
                accel = 0;
                if (NPC.position.Y - Main.player[NPC.target].position.Y > 280)
                {
                    moveType = 3;
                    delay = reload;
                    //shoot = 2;
                }
            }

            //Floating
            if (moveType == 3)
            {
                moveDelay -= 1;
                if (moveDelay <= 0)
                {
                    moveDelay = 4;
                    moveType = 4;
                }
                //Horizontal Movement
                NPC.velocity.X = accel;
                if (NPC.position.X < Main.player[NPC.target].position.X - 200 && hovDir == -1) { hovDir = 1; }
                if (NPC.position.X > Main.player[NPC.target].position.X + 200 && hovDir == 1) { hovDir = -1; }
                if (Math.Abs(accel) < maxAccel) { accel += (float)hovDir / 3f; }
                else { accel *= .98f; }

                //Vertical Movement
                NPC.velocity.Y = vaccel;
                if (NPC.position.Y - Main.player[NPC.target].position.Y + 70 > 0) { vDir = -1; }
                if (NPC.position.Y - Main.player[NPC.target].position.Y + 70 < 0) { vDir = 1; }
                if (Math.Abs(vaccel) < maxVaccel / 6) { vaccel += (float)vDir / 6f; }
                else { vaccel *= .98f; }
            }

            //Dashing
            if (moveType == 4)
            {
                //Horizontal Movement
                NPC.velocity.X = accel;
                if (dir == 1)
                {
                    if (accel < maxAccel / 2)
                    {
                        accel += .2f;
                    }
                    else
                    {
                        if (boosted == false) //Play boosted sound effect
                        {
                            moveDelay -= 1;
                            boosted = true;
                        }
                        accel = maxAccel;
                    }
                    if (delta.X < -range)
                    {
                        boosted = false;
                        dir = -1;
                    }
                }
                if (dir == -1)
                {
                    if (accel > maxAccel / -2)
                    {
                        accel -= .2f;
                    }
                    else
                    {
                        if (boosted == false) //Play boosted sound effect
                        {
                            moveDelay -= 1;
                            boosted = true;
                        }
                        accel = -maxAccel;
                    }
                    if (delta.X > range)
                    {
                        boosted = false;
                        dir = 1;
                    }
                }

                //Vertical Movement
                NPC.velocity.Y = vaccel;
                if (boosted == false)
                {
                    if (Math.Abs(delta.Y) > 40)
                    {
                        if (delta.Y > 40)
                        {
                            if (vaccel < maxVaccel) vaccel += .4f;
                        }
                        if (delta.Y < -40)
                        {
                            if (vaccel < maxVaccel) vaccel -= .4f;
                        }
                    }
                    else
                    {
                        if (Math.Abs(vaccel) > .01f) vaccel *= .5f;
                        else vaccel = 0f;
                    }
                }
                else vaccel = 0;

                if (moveDelay <= 0)
                {
                    moveDelay = 600;
                    moveType = 1;
                }
            }

            //}
            //Attacks
            delay--;
            if (delay <= 0 && Main.netMode != 1)
            {
                delay = reload;
                if (attackNum >= 4)
                {
                    attackNum = 0;
                    shoot = 1;
                }
                else
                {
                    shoot = Main.rand.Next(2, 5);
                    attackNum++;
                }
            }
            if (shoot == 1 && Main.netMode != 1)//Lazer
            {
                shoot = 0;
                laserCharge = 1;
                rotSpeed = .25f;
                dist = 0;
                if (NPC.CountNPCS(ModContent.NPCType<AnDioLaserBall>()) < 1 && !NewDawnWorld.downedEtheria)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<AnDioLaserBall>());
                    }
                }
            }
            if (laserCharge > 0)
            {
                laserCharge++;
                delay = reload;
                if (laserCharge < 120)
                {
                    dist += 1;
                    rotSpeed += .01f;
                }
                else if (laserCharge > 160)
                {
                    dist -= 3;
                    rotSpeed += .01f;
                }
                if (laserCharge > 240)
                {
                    SoundEngine.PlaySound(NewDawn.CreateSoundStyle("Sounds/zawarudo"));
                    //TODO: Add time stop effect here
                    //TimeManagement.TryAlterTime(new TimeAlterationRequest(NPC, 10 * 60, 0));
                    if (Main.netMode != 1)
                    {
                        //if(!NewDawn.zaWarudo > 0)
                        {
                        }
                        if (NPC.CountNPCS(ModContent.NPCType<AnDioLaserBall>()) > 1)
                            laser = 120;
                        else
                            moveType = 1;
                    }
                    else
                    {
                        delay = 1;
                    }
                    AnDioLaserBall.life = 0;
                    laserCharge = 0;
                }
                //Theta Rotation & Position Setting
                theta += 3.14f / 60 * rotSpeed;

            }
            if (laser > 0)
            {
                delay = reload;
                laser--;
                if (Main.netMode != 1)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AndeLaser3>(), damage / 2, 3, Main.myPlayer);
            }
            if (shoot == 2)//Spiral Orbs
            {
                shoot = 0;
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AndeEnergy>(), damage / 2, 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AnDioSpiral2>(), damage / 2, 3, Main.myPlayer);
                }
                spiralDur++;
            }
            if (spiralDur > 0)
            {
                spiralDur++;
                if (spiralDur > 200)
                    spiralDur = 0;
                spiralDelay++;
                if (spiralDelay >= 50)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AndeEnergy>(), damage / 2, 3f, Main.myPlayer);
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AnDioSpiral2>(), damage / 2, 3, Main.myPlayer);
                    }
                    spiralDelay = 0;
                    delay = reload;
                }
            }
            if (shoot == 3 && Main.netMode != 1)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AnDioEnergy>(), damage / 2, 3f, Main.myPlayer);
                shoot = 0;
                attackDuration = 1;
            }

            if (attackDuration == 90 && Main.netMode != 1)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AnDioEnergy>(), damage / 2, 3f, Main.myPlayer);
            }
            if (attackDuration == 180 && Main.netMode != 1)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AnDioEnergy>(), damage / 2, 3f, Main.myPlayer);
            }
            if (attackDuration == 270 && Main.netMode != 1)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<AnDioEnergy>(), damage / 2, 3f, Main.myPlayer);
            }
            if (attackDuration > 0)
                attackDuration++;
            if (attackDuration > 270)
            {
                attackDuration = 0;
                delay = reload;
            }

            if (shoot == 4 && Main.netMode != 1)//Split Ball
            {
                int rng = 480;
                int yHeight = 480;
                shoot = 0;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<DioShard>(), damage / 2, 3, Main.myPlayer);
                yHeight = -480;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X, Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Main.player[NPC.target].position.X - rng + Main.rand.Next(rng * 2), Main.player[NPC.target].position.Y - yHeight), new Vector2(0, 0), ModContent.ProjectileType<AndeShard>(), damage / 2, 3, Main.myPlayer);
            }

            NPC.netUpdate = true;
        }






        public override void OnKill()
        {
            if (plays == 0)
                plays = 1;
            if (NewDawnWorld.downedEtheria)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<TheWorldOfEtheria>(), 1);
            }
            if (!Main.expertMode)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<DioritusCore>(), 1);
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<AndesiaCore>(), 1);
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Marble, 10);
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Granite, 10);
            }

            if (Main.rand.Next(10) == 0)
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<AnDioTrophy>(), 1);
            NewDawnWorld.downedAnDio = true;
        }
        /*
        public override void FindFrame(int frameHeight)
        {
            int num = 168;
            drawOffsetY = 56f;
            if (anim)
                npc.frameCounter += 1.0;
            if (npc.frameCounter > 1.0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0.0;
            }
            if (npc.frame.Y > frameHeight * 10)
            {
                npc.frame.Y = frameHeight * 10;
                anim = false;
                npc.dontTakeDamage = false;
            }
        }*/


        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

    }
}
