using Microsoft.Xna.Framework;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Items.Materials;
using NewDawn.Items.Temple.Consumables;
using NewDawn.Tiles.Alchemistry;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ID.ContentSamples.CreativeHelper;

namespace NewDawn.Tiles.Temple
{
    public class KeystoneTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
            Main.tileSolid[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;


            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Demyst");
            AddMapEntry(new Color(80, 165, 208), name);
        }

        public override bool RightClick(int i, int j)
        {
            WorldGen.KillTile(i, j);
            return true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!noItem)
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<Keystone>());
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = .35f;
            g = 0.5f;
            b = .6f;
        }
    }
}