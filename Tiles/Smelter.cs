using Microsoft.Xna.Framework;
using NewDawn.Abstracts;
using NewDawn.Items.Alchemistry;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Items.Placeable;
using NewDawn.TileEntities;
using SubworldLibrary;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Tile_Entities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
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

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 3, 4, ModContent.ItemType<SmelterItem>());
        }
    }
}