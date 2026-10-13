using NewDawn.Items.Loot;
using NewDawn.NPCs.Etheria;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Etherial.BossFights
{
    public class SuperCorruptor : ModNPC
    {
        public bool bitherial = false;
        public bool etherial = true;
        private int _delay = 0;
        private int _shootDelay = 0;
        private int _index = 0;
        private Vector2 _targetPos;
        public float tVel = 0f;
        public float vel = 0f;
        public float vMax = 10f;
        public float vAccel = .2f;
        public float vMag = 0f;
        private float _theta = 0;
        private int _targetType = 0;

        public override void SetDefaults()
        {
            _shootDelay = 0;
            _targetType = 0;
            vMag = 0f;
            vMax = 14f;
            tVel = 0f;
            _index = 0;
            _delay = 0;
            NewDawnVars.etherial.Add(NPC.type);
            NPC.width = 36;
            NPC.height = 36;
            NPC.damage = 40;
            NPC.defense = 80;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 5;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Main.npcFrameCount[NPC.type] = 3;
        }
        
        public override void AI()
        {
            MovementType(NPC);
            Shoot(NPC);
            NPC.active = CheckActive();
        }
        
        private void MovementType(NPC npc)
        {
            _delay++;
            if (_delay > 480)
            {
                _delay = Main.rand.Next(1, 120);
                MirrorTeleport(npc, false);
                
            }
        }

        private void Shoot(NPC npc)
        {
            _shootDelay++;
            if (_shootDelay > 480)
            {
                _shootDelay = Main.rand.Next(1, 120);
                if (Main.netMode != 1)
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), NPC.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<EtherialYeet>(), (int)(npc.damage / 4), 3, Main.myPlayer);
            }
        }
        
        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<EtherialEssence>(), 1);
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
                        int n = NPC.NewNPC(npc.GetSource_FromAI(), (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<EtherialSpiralShot>());
                        Main.npc[n].ai[0] = npc.whoAmI;
                        Main.npc[n].ai[1] = i;
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
            if (NPC.frameCounter > 24.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 2)
            {
                NPC.frame.Y = 0;
            }
        }
    }
}
