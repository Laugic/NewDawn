using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class HolystoneBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[ModContent.TileType<HolystoneBrick>()][ModContent.TileType<Holystone>()] = true;
            Main.tileMerge[ModContent.TileType<Holystone>()][ModContent.TileType<HolystoneBrick>()] = true;
            Main.tileMerge[TileID.Marble][ModContent.TileType<HolystoneBrick>()] = true;
            Main.tileMerge[ModContent.TileType<HolystoneBrick>()][TileID.Marble] = true;
            Main.tileMerge[TileID.MarbleBlock][ModContent.TileType<HolystoneBrick>()] = true;
            Main.tileMerge[ModContent.TileType<HolystoneBrick>()][TileID.MarbleBlock] = true;
            //Main.tileSpelunker[Type] = true;
            Main.tileLighted[Type] = true;
            MineResist = .5f;
            MinPick = 0;
            DustType = 229;
            ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<Items.Placeable.HolystoneBrick>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}