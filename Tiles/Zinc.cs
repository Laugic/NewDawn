using NewDawn.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class Zinc : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileMerge[0][ModContent.TileType<Zinc>()] = true;
            Main.tileMerge[ModContent.TileType<Zinc>()][0] = true;
            Main.tileMerge[ModContent.TileType<ZincBrick>()][ModContent.TileType<Zinc>()] = true;
            Main.tileMerge[ModContent.TileType<Zinc>()][ModContent.TileType<ZincBrick>()] = true;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Zinc");
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(40, 100, 80), name);
            Main.tileSpelunker[Type] = true;
            MineResist = 1f;
            MinPick = 20;
            RegisterItemDrop(ModContent.ItemType<ZincOre>());
            HitSound = SoundID.Tink;
            DustType = 1;
            //soundStyle = 1;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        
    }
}