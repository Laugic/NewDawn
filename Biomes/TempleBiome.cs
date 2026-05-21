using Microsoft.Xna.Framework;
using NewDawn.Players;
using NewDawn.Tiles.Temple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Capture;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Biomes
{
    public class TempleBiome : ModBiome
    {
        // Select all the scenery
        //public override ModWaterStyle WaterStyle => ModContent.Find<ModWaterStyle>("ExampleMod/ExampleWaterStyle"); // Sets a water style for when inside this biome
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<TempleBackgroundStyle>();
        public override CaptureBiome.TileColorStyle TileColorStyle => CaptureBiome.TileColorStyle.Jungle;

        // Select Music
        public override int Music => MusicID.Underground;// MusicLoader.GetMusicSlot(Mod, "Sounds/Music/TempleRuins");

        // Sets how the Scene Effect associated with this biome will be displayed with respect to vanilla Scene Effects. For more information see SceneEffectPriority & its values.
        public override SceneEffectPriority Priority => (SceneEffectPriority)5; // We have set the SceneEffectPriority to be BiomeLow for purpose of example, however default behavour is BiomeLow.

        // Populate the Bestiary Filter
        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => base.BackgroundColor;

        // Use SetStaticDefaults to assign the display name
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Temple");
        }

        // Calculate when the biome is active.
        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<SpiritPlayer>().ShowSpirit;
        }
    }
    public class TempleBackgroundStyle : ModSurfaceBackgroundStyle
	{
		// Use this to keep far Backgrounds like the mountains.
		public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }
        /*
        public override int ChooseFarTexture()
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Biomes/ExampleBiomeSurfaceFar.png");
        }*/

        private static int SurfaceFrameCounter;
        private static int SurfaceFrame;
        public override int ChooseMiddleTexture()
        {
            if (++SurfaceFrameCounter > 12)
            {
                SurfaceFrame = (SurfaceFrame + 1) % 4;
                SurfaceFrameCounter = 0;
            }
            switch (SurfaceFrame)
            {
                case 0:
                    return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Biomes/ExampleBiomeSurfaceMid0");
                case 1:
                    return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Biomes/ExampleBiomeSurfaceMid1");
                case 2:
                    return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Biomes/ExampleBiomeSurfaceMid2");
                case 3:
                    return BackgroundTextureLoader.GetBackgroundSlot("NewDawn/Biomes/ExampleBiomeSurfaceMid3");
                default:
                    return -1;
            }
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return BackgroundTextureLoader.GetBackgroundSlot(Mod, "Biomes/ExampleBiomeSurfaceClose");
        }
    }

    /*
    public class TempleTileCount : ModSystem
    {
        public int templeTileCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            templeTileCount = tileCounts[ModContent.TileType<TempleBrick>()];
        }
    }*/
}
