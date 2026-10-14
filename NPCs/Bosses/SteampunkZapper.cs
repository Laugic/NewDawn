using System;
using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Bosses
{
    public class SteampunkZapper : ModNPC
    {
        int Delay { get; set; } = 0;
        public override void SetStaticDefaults()
        {
            NewDawnVars.eNPCs.Add(NPC.type);
            // DisplayName.SetDefault("Steampunk Zapper");
        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 34;
            NPC.damage = 50;
            NPC.defense = 10;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[ModContent.BuffType<Steamy>()] = true;
            Delay = 0;
        }

        public override void AI()
        {
            Move();
            Attack();
            CheckDespawn();
            Visuals();
        }

        private void Visuals()
        {
            Lighting.AddLight(NPC.position, .5f, .6f, .8f);
        }

        private void Attack()
        {
            Delay++;
            if(Delay > 6 * 60 || Main.rand.Next(6 * 60) == 0)
            {
                Delay = 0;
                if(Main.netMode != 1)
                {
                    int sign = (NPC.ai[1] % 2 == 0) ? 1 : -1;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center, new Vector2(sign * 12, 0), ModContent.ProjectileType<KnowledgeBolt>(), NPC.damage / 4, 3);
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        private void Move()
        {
            NPC.position = Main.npc[(int)NPC.ai[0]].Center;
            NPC.position.X += ((NPC.ai[1] % 2 == 0)?1:-1) * 1000;
            NPC.position.Y += (float)Math.Sin(Main.npc[(int)NPC.ai[0]].ai[1] + 2 * Math.PI / (double)NPC.CountNPCS(ModContent.NPCType<SteampunkDefender>()) * NPC.ai[1]) * Main.npc[(int)NPC.ai[0]].height * 3f;
        }

        private void CheckDespawn()
        {
            if (!Main.npc[(int)NPC.ai[0]].active || Main.npc[(int)NPC.ai[0]].life < 1 || Main.npc[(int)NPC.ai[0]].type != ModContent.NPCType<TheAnnihilator>())
                NPC.active = false;
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
            float alpha = 1.25f * ( 1f - (float)Main.npc[(int)NPC.ai[0]].life / (float)Main.npc[(int)NPC.ai[0]].lifeMax);
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

        public override void OnKill()
        {
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].type == ModContent.NPCType<SteampunkZapper>() && Main.npc[i].ai[1] > NPC.ai[1])
                    Main.npc[i].ai[1]--;
            }
        }
    }
}
