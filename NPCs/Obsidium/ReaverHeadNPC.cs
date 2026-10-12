using System;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Obsidium
{
    public class ReaverHeadNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaver Head");
        }
        int _phase = 0;
        Vector2 targetPos;
        public override void SetDefaults()
        {
            NPC.width = 20;
            NPC.height = 20;
            NPC.damage = 50;
            NPC.defense = 12;
            NPC.lifeMax = 500;
            NPC.value = 0f;
            NPC.knockBackResist = 0f;
            Main.npcFrameCount[NPC.type] = 4;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
            NPC.dontTakeDamage = true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 5 * 60, true);
        }

        public override void AI()
        {
            if(Main.rand.Next(4) == 0)
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, ModContent.DustType<Magma>(), 0f, 0f);

            SetTarget();
            PhaseCheck();
            Movement();
            DespawnCheck();

        }

        private void SetTarget()
        {
            if (_phase == 0)
                targetPos = Main.player[Main.npc[(int)NPC.ai[0]].target].Center;// * 2 - Main.npc[(int)npc.ai[0]].Center;
            else
                targetPos = Main.npc[(int)NPC.ai[0]].Center;
        }

        private void PhaseCheck()
        {
            if(Vector2.Distance(NPC.Center, targetPos) < 60 + Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y))
            {
                if (_phase == 0)
                    _phase = 1;
                else
                    _phase = 0;
            }
        }

        private void Movement()
        {
            if (NPC.position.X < targetPos.X)
            {
                NPC.velocity.X += .1f;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X *= .9f;
            }
            if (NPC.position.X > targetPos.X)
            {
                NPC.velocity.X -= .1f;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X *= .9f;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += .04f;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y *= .9f;
            }
            if (NPC.Center.Y > targetPos.Y)
            {
                NPC.velocity.Y -= .04f;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y *= .9f;
            }
        }

        private void DespawnCheck()
        {
            if (Vector2.Distance(NPC.Center, Main.npc[(int)NPC.ai[0]].Center) > 2400 || Main.npc[(int)NPC.ai[0]].life < 1 || !Main.npc[(int)NPC.ai[0]].active)
            {
                NPC.active = false;
                NPC.life = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (NPC.frameCounter > 15)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }
    }
}
