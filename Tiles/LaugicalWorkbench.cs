using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace NewDawn.Tiles
{
    public class LaugicalWorkbench : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.Width = 7;
            //TileObjectData.newTile.Height = 3;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Laugical Workbench");
            AddMapEntry(new Color(200, 200, 200), name);
            //disableSmartCursor = true;
            //adjTiles = new int[] { TileID.WorkBenches };
            AnimationFrameHeight = 74;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if (frameCounter >= 5)
            {
                frameCounter = 0;
                frame++;
                if (frame > 26)
                {
                    frame = 0;
                }
            }
        }
    }
}