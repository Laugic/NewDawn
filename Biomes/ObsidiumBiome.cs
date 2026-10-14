using NewDawn.Tiles;
using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Biomes
{
    public class ObsidiumBiome : ModBiome
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Obsidium");

        public override bool IsBiomeActive(Player player)
        {
            return ModContent.GetInstance<BiomeTileCounts>().BasaltBlockCount >= 100;
        }
    }
    public class BiomeTileCounts : ModSystem
    {
        public int BasaltBlockCount { get; private set; }

        public override void TileCountsAvailable(
            ReadOnlySpan<int> tileCounts)
        {
            BasaltBlockCount = tileCounts[ModContent.TileType<ObsidiumRock>()];
        }
    }
}
