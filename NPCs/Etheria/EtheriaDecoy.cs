using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etheria
{
    [AutoloadBossHead]
    public class EtheriaDecoy : ModNPC
    {
        public int phase = 0;
        public int delay = 0;
        public bool bitherial = true;
        public int plays = 0;
        public int attack = 0;
        public int attackRel = -1;
        public int attackRelMax = 0;
        public int attackDel = 0;
        public int attackDelMax = 0;
        public int moveType = 1;
        public int hovDir = 1;
        public int moveDelay = 600;
        public int vDir = 2;
        public int dir = 0;
        public int vdir = 0;
        public float accel = 0f;
        public float maxAccel = 20f;
        public float vaccel = 0f;
        public float maxVaccel = 20f;
        public int damage = 0;
        public int eattack = 0;
        public int eattackDel = 0;
        public int eattackDelMax = 0;
        public static bool despawn = false;
        int _despawnT = 0;

        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            delay = 0;
            // DisplayName.SetDefault("Etherial Shadow");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            _despawnT = 0;
            despawn = false;
            phase = 0;
            eattack = 0;
            eattackDelMax = 280;
            eattackDel = eattackDelMax;
            damage = 40;
            moveDelay = 600;
            maxAccel = 22f;
            maxVaccel = 20f;
            accel = 0f;
            vaccel = 0f;
            hovDir = 1;
            vDir = 1;
            moveType = 1;
            attackDelMax = 260;
            attackDel = attackDelMax;
            attackRel = -1;
            attackRelMax = 0;
            attack = 0;
            bitherial = true;
            NPC.width = 190;
            NPC.height = 240;
            NPC.damage = 80;
            NPC.defense = 48;
            NPC.aiStyle = 0;
            NPC.lifeMax = 8000;
            NPC.HitSound = SoundID.NPCHit21;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Etheria");
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            plays = numPlayers;
            NPC.lifeMax = 10000 + numPlayers * 1950;
            NPC.damage = 80;
            attackDelMax = 220;
            attackDel = attackDelMax;
            damage = 30;
            eattack = 0;
            eattackDelMax = 200;
            eattackDel = eattackDelMax;
            if (NewDawnWorld.downedEtheria)
            {
                maxAccel = 30f;
            }
        }
        

        public override void AI()
        {
            //Retarget (borrowed from Dan <3)
            Player player = Main.player[NPC.target];
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            NPC.netUpdate = true;

            //DESPAWN
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
                {
                    if (_despawnT == 0)
                        _despawnT++;
                }
            }
            else if (!Main.dayTime || NewDawnWorld.downedEtheria)
                _despawnT = 0;
            if (Main.dayTime && _despawnT == 0 && !NewDawnWorld.downedEtheria)
                _despawnT++;
            if (_despawnT >= 1)
            {
                _despawnT++;
                NPC.noTileCollide = true;
                if (_despawnT >= 300)
                    NPC.active = false;
            }
            if(despawn)
                NPC.active = false;
            NPC.spriteDirection = 0;
            bitherial = true;
            NPC.rotation = 0;
            if (phase > 2)
                NPC.scale = 1f + (float)(phase) / 10f;
            else
                NPC.scale = 1f;
            NPC.height = (int)(NPC.scale * 190);
            NPC.width = (int)(NPC.scale * 240);
            phase = Etheria.phase;

            //Movement
            Vector2 delta = Main.player[NPC.target].Center - NPC.Center;
            float magnitude = (float)Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y);

            //Hovering
            if (moveType == 1)
            {
                moveDelay -= 1;
                if (moveDelay <= 0)
                {
                    moveDelay = 900;
                    moveType = 2;
                }
                //Horizontal Movement
                NPC.velocity.X = accel;
                if (delta.X > 0) { hovDir = 1; }
                if (delta.X < 0) { hovDir = -1; }
                if (Math.Abs(accel) < maxAccel / 2) { accel += (float)hovDir / 5f; }
                else { accel *= .98f; }

                //Vertical Movement
                NPC.velocity.Y = vaccel;
                if (NPC.position.Y - Main.player[NPC.target].position.Y + 70 > 0) { vDir = -1; }
                if (NPC.position.Y - Main.player[NPC.target].position.Y + 70 < 0) { vDir = 1; }
                if (Math.Abs(vaccel) < maxVaccel / 4) { vaccel += (float)vDir / 3f; }
                else { vaccel *= .98f; }
            }

            //Floating
            if (moveType == 2)
            {
                moveDelay -= 1;
                if (moveDelay <= 0)
                {
                    moveDelay = 750;
                    moveType = 1;
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

            float projMod = 2f;
            if (phase <= 4)
                projMod = 2.5f;
            //Attack Delay
            attackDel--;
            if(attackDel <= 0)
            {
                attackDel = attackDelMax;
                attack = Main.rand.Next(1,4);
            }

            //Attacks
            if(attack == 1 && Main.netMode != 1)
            {
                float dir = (float)Math.Atan2(NPC.DirectionTo(Main.player[NPC.target].Center).Y, NPC.DirectionTo(Main.player[NPC.target].Center).X);
                /*if(phase <= 4)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, npc.Center.Y, 8 * (float)Math.Cos(dir), 8 * (float)Math.Sin(dir), ModContent.ProjectileType<QuadroBurst>(), (int)(npc.damage / projMod), 3, Main.myPlayer);
                else
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, npc.Center.Y, 8 * (float)Math.Cos(dir), 8 * (float)Math.Sin(dir), ModContent.ProjectileType<TrueQuadroBurst>(), (int)(npc.damage / projMod), 3, Main.myPlayer);
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, npc.Center.Y, 8 * (float)Math.Cos(dir + 3.14), 8 * (float)Math.Sin(dir + 3.14), ModContent.ProjectileType<TrueQuadroBurst>(), (int)(npc.damage / projMod), 3, Main.myPlayer);
                }*/
                attack = 0;
            }
            if(attack == 2 && Main.netMode != 1)
            {
                if (phase <= 4)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<EtherialYeet>(), (int)(NPC.damage / projMod), 3, Main.myPlayer);
                else
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<TrueEtherialYeet>(), (int)(NPC.damage / projMod), 3, Main.myPlayer);
                }
                attack = 0;
            }
            if (attack == 3 && Main.netMode != 1)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Sound, "Sounds/EtherialChange"));
                NPC.position.X = Main.player[NPC.target].position.X - (NPC.position.X - Main.player[NPC.target].position.X) / 2;
                NPC.position.Y = Main.player[NPC.target].position.Y - (NPC.position.Y - Main.player[NPC.target].position.Y) / 2;
                NPC.velocity.X = -NPC.velocity.X;
                NPC.velocity.Y = -NPC.velocity.Y;
                if (Main.rand.Next(4) == 0)
                {
                    if (phase <= 4)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            int n = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<EtherialSpiralShot>());
                            Main.npc[n].ai[0] = NPC.whoAmI;
                            Main.npc[n].ai[1] = i;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            int n = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<TrueEtherialSpiralShot>());
                            Main.npc[n].ai[0] = NPC.whoAmI;
                            Main.npc[n].ai[1] = i;
                        }
                    }
                }
                attack = 0;
            }

        }

        public override Color? GetAlpha(Color drawColor)
        {
            int b = 125;
            int b2 = 225;
            int b3 = 255;
            if (phase > 4 && NewDawnWorld.downedEtheria)
            {
                b = 225;
                b2 = 125;
                b3 = 125;
            }
            if (drawColor.R != (byte)b)
            {
                drawColor.R = (byte)b;
            }
            if (drawColor.G < (byte)b2)
            {
                drawColor.G = (byte)b2;
            }
            if (drawColor.B < (byte)b3)
            {
                drawColor.B = (byte)b3;
            }
            return drawColor;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {

        }
        
        public override void FindFrame(int frameHeight)
        {
                NPC.frame.Y = frameHeight * phase;
        }
        
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 0f;
            return null;
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
            float num212 = 1f - (float)NPC.life / (float)NPC.lifeMax;
            num212 *= num212;
            alpha15.R = (byte)((float)alpha15.R * num212);
            alpha15.G = (byte)((float)alpha15.G * num212);
            alpha15.B = (byte)((float)alpha15.B * num212);
            alpha15.A = (byte)((float)alpha15.A * num212);
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
