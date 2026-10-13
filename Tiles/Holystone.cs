using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class Holystone : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[TileID.Dirt][ModContent.TileType<Holystone>()] = true;
            Main.tileMerge[ModContent.TileType<Holystone>()][TileID.Dirt] = true;
            Main.tileMerge[TileID.Marble][ModContent.TileType<Holystone>()] = true;
            Main.tileMerge[ModContent.TileType<Holystone>()][TileID.Marble] = true;
            Main.tileMerge[TileID.MarbleBlock][ModContent.TileType<Holystone>()] = true;
            Main.tileMerge[ModContent.TileType<Holystone>()][TileID.MarbleBlock] = true;
            //Main.tileSpelunker[Type] = true;
            Main.tileLighted[Type] = true;
            MineResist = .5f;
            MinPick = 0;
            DustType = 229;
            RegisterItemDrop(ModContent.ItemType<Items.Placeable.Holystone>());
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}