using NewDawn.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Tile_Entities;
using Terraria.ModLoader;
using Terraria.ID;

namespace NewDawn.Abstracts
{
    public abstract class NewDawnTile : ModTile
    {
        public int GetLeft(int x, int y)
        {
            if (x == 0)
                return x;

            int xOff = 0;
            while (x - xOff > 0 && Main.tile[x - xOff, y].TileType == ModContent.TileType<Smelter>())
                xOff++;

            if (xOff % 2 == 0)
                return x - 1;

            return x;
        }

        public int GetTop(int x, int y)
        {
            if (y == 0)
                return y;

            int yOff = 0;
            while (y - yOff > 0 && Main.tile[x, y - yOff].TileType == ModContent.TileType<Smelter>())
                yOff++;

            if (yOff % 2 == 0)
                return y - 1;

            return y;
        }

        public static void TryChangeItemFrameItem(Item item, TEItemFrame frame, int x, int y)
        {
            if (Main.myPlayer != Main.LocalPlayer.whoAmI)
                return;

            if (item.stack > 0)
            {
                Main.LocalPlayer.GamepadEnableGrappleCooldown();
                frame.item.stack = 0;
                TEItemFrame.TryPlacing(x, y, item, 1);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.ItemFrameTryPlacing, -1, -1, null, x, y, Main.LocalPlayer.selectedItem, Main.LocalPlayer.whoAmI, 1);
                Recipe.FindRecipes();
                return;
            }
        }

    }
}
