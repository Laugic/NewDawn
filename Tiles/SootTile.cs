using NewDawn.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class SootTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Soot");
            AddMapEntry(new Color(60, 50, 60), name);
            MineResist = .5f;
            MinPick = 0;
            RegisterItemDrop(ModContent.ItemType<Soot>());
            DustType = 1;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}