using NewDawn.Biomes;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Obsidium
{
    public class ReaverNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaver");
        }
        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 48;
            NPC.damage = 50;
            NPC.defense = 30;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            Main.npcFrameCount[NPC.type] = 4;
            NPC.value = 2000f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.InModBiome<ObsidiumBiome>() && Main.hardMode && NewDawnWorld.downedRagnar)
                return .075f;
            else return 0f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 300, true);
        }

        public override void AI()
        {
            if (NPC.life < NPC.lifeMax / 2 && NPC.ai[0] != 1)
            {
                NPC.ai[0] = 1;

                int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<ReaverHeadNPC>());
                Main.npc[n].ai[0] = NPC.whoAmI;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1;
            if (NPC.frameCounter > 30)
            {
                NPC.frame.Y = NPC.frame.Y + frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y > frameHeight * 3)
            {
                NPC.frame.Y = 0;
            }
        }

        public override void OnKill()
        {
            //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MagmaticCrystal>());
        }
    }
}
