using System;
using NewDawn.Items.Loot;
using NewDawn.NPCs.Etheria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class Terratome : ModNPC
    {
        public bool bitherial = false;
        public bool etherial = true;
        int delay = 0;
        int shootDelay = 0;
        int index = 0;
        Vector2 targetPos;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 10f;
        public float vAccel = .2f;
        public float vMag = 0f;
        float theta = 0;
        int targetType = 0;
        int moveType = 0;
        int moveCounter = 0;
        int shootCooldown = 2 * 60;
        int moveDelay = 0;

        public override void SetDefaults()
        {
            moveDelay = 0;
            shootCooldown = 2 * 60;
            moveCounter = 0;
            moveType = 0;
            shootDelay = 0;
            targetType = 0;
            vMag = 0f;
            vMax = 14f;
            tVel = 0f;
            index = 0;
            delay = 0;
            NewDawnVars.etherial.Add(NPC.type);
            NPC.width = 78;
            NPC.height = 78;
            NPC.damage = 40;
            NPC.defense = 80;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;
            NPC.scale = 1.5f;
        }

        private bool CheckIfActive()
        {
            return (NPC.CountNPCS(NPCID.Spazmatism) > 0 || NPC.CountNPCS(NPCID.Retinazer) > 0);
        }

        public override void AI()
        {
            UpdateValues();
            CheckMoveType();
            if(moveType == 2)
                CheckTeleport(NPC);
            Shoot(NPC);
            NPC.active = CheckIfActive();
            MoveToTarget(NPC);
        }

        private void UpdateValues()
        {
            theta += (float)Math.PI / 90;
            if (theta > (float)Math.PI * 2)
                theta -= (float)Math.PI * 2;
            if (moveType == 0)
                NPC.rotation = 0;
            else
                NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) - 1.57f;
        }

        private void CheckMoveType()
        {
            moveCounter++;
            if (moveCounter > 9 * 60 + 2)
            {
                moveCounter = 0;
                moveType++;
            }
            if (moveType == 3)
                moveType = 0;
            GetMovement();
        }

        private void GetMovement()
        {
            if(moveType == 0)
            {
                targetPos = Main.player[NPC.target].Center;
                targetPos.Y -= 320 + (float)Math.Sin(theta) * 60;
                shootCooldown = 2 * 60;
                moveDelay = 0;
                vMax = 32f;
            }
            if (moveType == 1)
            {
                moveDelay--;
                if (moveDelay <= 0)
                {
                    targetPos = Main.player[NPC.target].Center;
                    moveDelay = 90;
                }
                shootCooldown = 3 * 60;
                vMax = 16f;
            }
            if (moveType == 2)
            {
                targetPos = Main.player[NPC.target].Center;
                shootCooldown = 2 * 60;
                vMax = 12f;
            }
        }

        private void CheckTeleport(NPC npc)
        {
            delay++;
            if (delay > 480)
            {
                delay = Main.rand.Next(1, 120);
                MirrorTeleport(npc, true);
            }
        }

        private void Shoot(NPC npc)
        {
            shootDelay++;
            if (shootDelay > shootCooldown)
            {
                shootDelay = Main.rand.Next(0, 60);
                if (Main.netMode != 1)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<EtherialYeet>(), (int)(npc.damage / 2), 3, Main.myPlayer);
            }
        }

        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<EtherialEssence>(), 5);
        }

        private void MirrorTeleport(NPC npc, bool burst)
        {
            SoundEngine.PlaySound(NewDawn.CreateSoundStyle("Sounds/EtherialChange"));
            if (burst && Main.player[npc.target].statLife > 1)
            {
                for (int i = 0; i < 8; i++)
                {

                    if (Main.netMode != 1)
                    {
                        int N = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<EtherialSpiralShot>());
                        Main.npc[N].ai[0] = npc.whoAmI;
                        Main.npc[N].ai[1] = i;
                    }
                }
            }
            npc.position.X = Main.player[npc.target].position.X - (npc.position.X - Main.player[npc.target].position.X);
            npc.position.Y = Main.player[npc.target].position.Y - (npc.position.Y - Main.player[npc.target].position.Y);
            npc.velocity.X = -npc.velocity.X;
            npc.velocity.Y = -npc.velocity.Y;
        }

        private void MoveToTarget(NPC npc)
        {
            float dist = Vector2.Distance(targetPos, npc.Center);
            tVel = dist / 15;
            if (vMag < vMax && vMag < tVel)
            {
                vMag += vAccel;
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
                npc.velocity = npc.DirectionTo(targetPos) * vMag;
            }
        }
    }
}
