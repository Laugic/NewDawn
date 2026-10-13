using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class GodRay : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[TileID.Cloud][ModContent.TileType<GodRay>()] = true;
            Main.tileMerge[ModContent.TileType<GodRay>()][TileID.Cloud] = true;
            Main.tileMerge[TileID.RainCloud][ModContent.TileType<GodRay>()] = true;
            Main.tileMerge[ModContent.TileType<GodRay>()][TileID.RainCloud] = true;
            //Main.tileSpelunker[Type] = true;
            Main.tileLighted[Type] = true;
            MineResist = .5f;
            MinPick = 0;
            DustType = 19;
            RegisterItemDrop(ModContent.ItemType<Items.Placeable.GodRay>());
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}