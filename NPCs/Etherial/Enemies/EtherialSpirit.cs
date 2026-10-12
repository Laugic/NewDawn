using System;
using NewDawn.Items.Loot;
using NewDawn.NPCs.Etheria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etherial.Enemies
{
    public class EtherialSpirit : ModNPC
    {
        public bool bitherial = false;
        public bool etherial = true;
        public override void SetDefaults()
        {
            NewDawnVars.etherial.Add(NPC.type);
            NPC.width = 68;
            NPC.height = 74;
            NPC.damage = 60;
            NPC.defense = 80;
            NPC.lifeMax = 400;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Main.npcFrameCount[NPC.type] = 5;
            NPC.scale *= 2f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            bool canSpawn = true;
            foreach(NPC npc in Main.npc)
            {
                if (npc.boss)
                    canSpawn = false;
            }
            if (NewDawnWorld.downedEtheria && canSpawn && NPC.CountNPCS(ModContent.NPCType<EtherialSpirit>()) < 2)
                return .035f;
            else return 0f;
        }

        public override void AI()
        {
            NPC.rotation = 0.02f;
            if (NPC.localAI[0] == 0f)
            {
                AdjustMagnitude(ref NPC.velocity);
                NPC.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 1400f;
            bool target = false;
            for (int k = 0; k < 8; k++)
            {
                if (Main.player[k].active)
                {
                    Vector2 newMove = Main.player[k].Center - NPC.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                NPC.velocity = (12 * NPC.velocity + move) / 11f;
                AdjustMagnitude(ref NPC.velocity);
            }


            if (Main.netMode != 1 && Main.rand.Next(280) == 0)
            {
                Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, NPC.Center.Y, 0, 0, ModContent.ProjectileType<EtherialYeet>(), (int)(NPC.damage / 6), 3, Main.myPlayer);
            }
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= 4f / magnitude;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 4.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 4)
            {
                NPC.frame.Y = 0;
            }

        }
        
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<EtherialEssence>(), Main.rand.Next(3, 5));            
        }
    }
}
