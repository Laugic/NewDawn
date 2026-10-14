using NewDawn.Biomes;
using NewDawn.Items.Loot;
using NewDawn.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Obsidium
{
    public class ObsidiumSkull : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 32;
            NPC.damage = 35;
            NPC.defense = 4;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.value = 60f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 10;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[24] = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.InModBiome<ObsidiumBiome>())
                return .015f;
            else return 0f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, 90, true);
        }

        public override void OnKill()
        {
            if (NewDawnWorld.downedRagnar && Main.rand.Next(2) == 0)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<ObsidiumChunk>());
            }
            else
            {
                if (NPC.downedBoss2)
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<ObsidiumOre>(), Main.rand.Next(1, 4));
                else
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, 173, Main.rand.Next(4));
            }
        }
    }
}
