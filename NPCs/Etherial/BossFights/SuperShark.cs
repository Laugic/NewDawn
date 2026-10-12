using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class SuperShark : ModNPC
    {
        int movementType = 0;
        int movementCounter = 0;
        bool justSpawned = false;
        private Vector2 targetPos;
        float vMag = 0;
        public override void SetDefaults()
        {
            movementCounter = 0;
            NewDawnVars.etherial.Add(NPC.type);
            NPC.width = 18;
            NPC.height = 18;
            NPC.damage = 80;
            NPC.defense = 80;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath19;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void AI()
        {
            NPC.spriteDirection = (int)NPC.ai[1];
            if (!Main.npc[(int)NPC.ai[0]].active)
                NPC.active = false;
            if (!justSpawned)
            {
                justSpawned = true;
                movementCounter = 45 * (int)NPC.ai[1];
                targetPos = Main.player[NPC.target].Center;
            }
            Movement();
            NPC.rotation = NPC.ai[1] * -(float)(Math.PI / 2) + (float)(Math.PI / 2) + (float)Math.Atan2(targetPos.Y - NPC.Center.Y, targetPos.X - NPC.Center.X);
        }

        private void Movement()
        {
            MovementTypeCheck();
            Move();
            MoveToTarget();
        }

        private void MovementTypeCheck()
        {
            movementCounter++;
            if (movementCounter > 2 * 60 && movementType == 0)
            {
                movementType = 1;
                targetPos.Y += 800;
                movementCounter = 0;
            }
            if (movementCounter > 60 && movementType == 1)
            {
                movementType = 2;
                movementCounter = 0;
            }
            if (movementCounter > 2 * 60 && movementType == 2)
            {
                movementType = 3;
                targetPos.X -= NPC.ai[1] * 800;
                movementCounter = 0;
            }
            if (movementCounter > 60 && movementType == 3)
            {
                movementType = 4;
                movementCounter = 0;
            }
            if (movementCounter > 5 * 60 && movementType == 4)
            {
                movementType = 0;
                movementCounter = 0;
            }
        }

        private void Move()
        {
            if (movementType == 0)
            {
                targetPos = Main.player[NPC.target].Center;
                targetPos.Y -= 300 + 100 * (int)NPC.ai[1];
            }
            if (movementType == 2)
            {
                targetPos = Main.player[NPC.target].Center;
                targetPos.X += NPC.ai[1] * 300;
            }
            if (movementType == 4)
            {
                if (movementCounter % 60 == 0)
                    targetPos = Main.player[NPC.target].Center + (Main.player[NPC.target].Center - NPC.Center) / 2;
            }
        }

        private void MoveToTarget()
        {
            float dist = Vector2.Distance(targetPos, NPC.Center);
            float tVel = dist / 15;
            float vMax = 24;
            if (vMag < vMax && vMag < tVel)
            {
                vMag += .2f;
                vMag = tVel;
            }
            if (vMag > tVel)
            {
                vMag = tVel;
            }
            if (vMag > vMax)
            {
                vMag = vMax;
            }
            if (dist != 0)
            {
                NPC.velocity = NPC.DirectionTo(targetPos) * vMag;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}
