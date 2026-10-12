using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.RockTwins
{
    public class AnDio1 : ModNPC
    {
        public bool spawned = false;
        public bool bitherial = true;
        public override void SetDefaults()
        {
            bitherial = true;
            spawned = false;
            NPC.width = 59;
            NPC.height = 167;
            NPC.damage = 42;
            NPC.defense = 10;
            NPC.aiStyle = 0;
            NPC.lifeMax = 5000;
            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath1;
            Main.npcFrameCount[NPC.type] = 9;
            NPC.npcSlots = 15f;
            NPC.value = 12f;
            NPC.knockBackResist = 99f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.scale = 2f;
            NPC.dontTakeDamage = true;
            NPC.boss = true;
        }
        public override void AI()
        {
            bitherial = true;
            NPC.spriteDirection = 0;
            
            //Disabling if AnDio is on
            if (NPC.CountNPCS(ModContent.NPCType<AnDio3>()) > 0 || NPC.CountNPCS(ModContent.NPCType<AnDio2>()) > 0 || NPC.CountNPCS(ModContent.NPCType<AnDio1>()) > 1)
            {
                NPC.active = false;
                //if(Main.netMode != 1)
                    //NPC.NewNPC((int)npc.Center.X, (int)npc.position.Y + npc.height, ModContent.NPCType<AnDio1>());
            }

        }
        public override void FindFrame(int frameHeight)
        {
            int num = 168;
            DrawOffsetY = 56f;
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter > 1.0)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0.0;
            }
            if (NPC.frame.Y > frameHeight * 8 && !spawned)
            {
                NPC.frame.Y = frameHeight * 10;
                SoundEngine.PlaySound(SoundLoader.customSoundType, -1, -1, Mod.GetSoundSlot(SoundType.Sound, "Sounds/boom"));
                if(Main.netMode != 1)
                    NPC.NewNPC((int)NPC.Center.X, (int)NPC.position.Y + NPC.height, ModContent.NPCType<AnDio3>());
                spawned = true;
                NPC.active = false;
            }

        }
    }
}
