using System;
using NewDawn.Items.Loot;
using NewDawn.NPCs.Etheria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class SuperSlime : ModNPC
    {
        public bool bitherial = false;
        public bool etherial = true;
        int _delay = 0;
        int _index = 0;
        Vector2 _targetPos;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 10f;
        public float vAccel = .2f;
        public float vMag = 0f;
        float _theta = 0;
        int _targetType = 0;

        public override void SetDefaults()
        {
            _targetType = 0;
            vMag = 0f;
            vMax = 14f;
            tVel = 0f;
            _index = 0;
            _delay = 0;
            NewDawnVars.etherial.Add(NPC.type);
            NPC.width = 54;
            NPC.height = 40;
            NPC.damage = 40;
            NPC.defense = 80;
            NPC.lifeMax = 400;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Main.npcFrameCount[NPC.type] = 2;
            NPC.scale *= 1.5f;
        }
        
        public override void AI()
        {
            MovementType(NPC);
            MoveToTarget(NPC);
            Shoot(NPC);
        }
        
        private void MovementType(NPC npc)
        {
            npc.rotation = 0f;
            _theta += 3.14f / 30;
            if (Main.npc[(int)npc.ai[0]].active && Main.player[Main.npc[(int)npc.ai[0]].target].statLife > 0)
            {
                if (_targetType == 0)
                {
                    float mag = 64;
                    Vector2 rot;
                    rot.X = (float)Math.Cos(_theta + 3.14f * npc.ai[1] / 4) * (mag + 32 * npc.ai[1]);
                    rot.Y = (float)Math.Sin(_theta + 3.14f * npc.ai[1] / 4) * (mag + 32 * npc.ai[1]);
                    _targetPos = Main.npc[(int)npc.ai[0]].Center + rot;
                }
                if (_targetType == 1)
                {
                    _targetPos = Main.player[Main.npc[(int)npc.ai[0]].target].Center;
                }
            }
            else
                npc.active = false;
        }

        private void MoveToTarget(NPC npc)
        {
            float dist = Vector2.Distance(_targetPos, npc.Center);
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
                npc.velocity = npc.DirectionTo(_targetPos) * vMag;
            }
        }

        private void Shoot(NPC npc)
        {
            _delay++;
            if (_delay > 480)
            {
                _delay = Main.rand.Next(0, 120);
                if (Main.netMode != 1 && _targetType == 0)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<EtherialYeet>(), (int)(npc.damage / 4), 3, Main.myPlayer);
                }
                _targetType = 1 - _targetType;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 20.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 1)
            {
                NPC.frame.Y = 0;
            }
        }
        
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<EtherialEssence>(), 1);
        }
    }
}
