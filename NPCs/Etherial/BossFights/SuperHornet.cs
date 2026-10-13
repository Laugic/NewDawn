using NewDawn.Items.Loot;
using NewDawn.NPCs.Etheria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class SuperHornet : ModNPC
    {
        public bool bitherial = false;
        public bool etherial = true;
        int delay = 0;
        int index = 0;
        Vector2 targetPos;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 4f;
        public float vAccel = .2f;
        public float vMag = 0f;
        float theta = 0;
        int targetType = 0;
        int counter = 0;
        int cooldown = 0;

        public override void SetDefaults()
        {
            counter = 0;
            cooldown = 0;
            targetType = 0;
            vMag = 0f;
            vMax = 14f;
            tVel = 0f;
            index = 0;
            delay = 0;
            NewDawnVars.etherial.Add(NPC.type);
            NPC.width = 18;
            NPC.height = 18;
            NPC.damage = 40;
            NPC.defense = 80;
            NPC.lifeMax = 4000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 0;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void AI()
        {
            Movement(NPC);
            Attack(NPC);
        }

        private void Movement(NPC npc)
        {
            delay++;
            if ((delay > 8 * 60 || Main.rand.Next(8 * 60) == 0) && counter == 0)
            {
                delay = Main.rand.Next(0, 120);
                cooldown = 2 * 60;
                MirrorTeleport(npc, false);
            }
            npc.rotation = 0;
            if (cooldown > 0)
                cooldown--;
            if (cooldown > 1 * 60 + 30)
            {
                npc.velocity.X = 0;
                npc.velocity.Y = 0;
            }
            else
            {
                targetPos = Main.player[npc.target].Center;
                MoveToTarget(npc);
            }
            if (Main.player[npc.target].position.X > npc.position.X)
                npc.direction = 1;
            else
                npc.direction = 0;
        }

        private void Attack(NPC npc)
        {
            counter++;
            if (counter > 4 * 60)
            {
                counter = 0;
                if (Main.netMode != 1)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<EtherialStinger>(), (int)(npc.damage * .7), 3, Main.myPlayer);
            }
        }

        public override void OnKill()
        {
            if (Main.rand.Next(6) == 0) Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<EtherialEssence>(), 1);
        }

        private void MirrorTeleport(NPC npc, bool burst)
        {
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

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 4.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight)
            {
                NPC.frame.Y = 0;
            }
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
