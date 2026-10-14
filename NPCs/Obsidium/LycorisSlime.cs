using NewDawn.Biomes;
using NewDawn.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Obsidium
{
    public class LycorisSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = 1;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime;
            NPC.width = 32;
            NPC.height = 28;
            NPC.damage = 30;
            NPC.defense = 4;
            NPC.lifeMax = 60;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(silver: 1);
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.buffImmune[BuffID.OnFire] = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.InModBiome<ObsidiumBiome>())
                return .075f;
            else return 0f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.OnFire, (2 * 60) + Main.rand.Next(60), true);
        }

        public override void OnKill()
        {
            if (Main.rand.Next(3) != 0)
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Lycoris>(), Main.rand.Next(2, 5));
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Gel, Main.rand.Next(1, 4));
        }
    }
}
