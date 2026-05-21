using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using NewDawn.Temple;
using NewDawn.Items.Temple.Consumables;
using NewDawn.Temple.RoomGeneration;
using System;

namespace NewDawn.Tiles.Temple
{
    public class ExitStairs : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            MineResist = 9999f;
            MinPick = 9999;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style5x4);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 3;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(150, 150, 150), name);
        }

        public override bool RightClick(int i, int j)
        {
            if (!CheckForKeystones())
                return false;

            if (TempleMain.floor % 7 == 0)
            {
                TempleMain.floor++;
                SubworldSystem.Enter<Sanctuary>();
            }
            else
                SubworldSystem.Exit();
            return true;
        }

        private bool CheckForKeystones()
        {
            int numKeystones = 0;
            foreach(Item item in Main.player[Main.myPlayer].inventory)
            {
                if (item.type == ModContent.ItemType<Keystone>())
                    numKeystones += item.stack;
            }
            return numKeystones >=3;
        }
    }
}
