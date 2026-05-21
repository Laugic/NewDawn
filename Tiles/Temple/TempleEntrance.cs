using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using NewDawn.Temple;
using NewDawn.Items.Temple.Consumables;

namespace NewDawn.Tiles.Temple
{
    public class TempleEntrance : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Width = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Temple Entrance");
            AddMapEntry(new Color(150, 150, 150), name);
        }

        public override bool RightClick(int i, int j)
        {
            foreach(Item item in Main.LocalPlayer.inventory)
            {
                if(item.type == ModContent.ItemType<AncientTempleKey>())
                {
                    item.stack--;
                    TempleMain.EnterFloor();
                    return true;
                }
            }
            Main.NewText("You need an Ancient Temple Key to enter!");
            return true;
        }
    }
}
