using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ObjectData;

namespace NewDawn.Tiles
{
    public class Smelter : Abstracts.NewDawnTile
    {
        public static string NameString { get => "Smetler"; }
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault(NameString);
            AddMapEntry(new Color(170, 200, 120), name);
        }

        public override bool RightClick(int i, int j)
        {
            if (Main.myPlayer != Main.LocalPlayer.whoAmI)
                return false;

            return true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}