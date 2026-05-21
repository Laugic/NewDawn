using NewDawn.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using NewDawn.Tiles;
using System.IO;
using NewDawn.Items.Placeable;

namespace NewDawn.TileEntities
{
    public class SmelterTileEntity : AbstractStorageTile
    {
        public override Item[] Items { get; set; } = new Item[1];
        public Item item;

        public SmelterTileEntity()
        {
            item = new Item();
        }

        public override bool IsTileValidForEntity(int x, int y)
        {
            return Main.tile[x, y].HasTile && Main.tile[x, y].TileType == ModContent.TileType<Smelter>() && Main.tile[x, y].TileFrameY == 0 && Main.tile[x, y].TileFrameX % 54 == 0;
        }

        public void DropItem()
        {
            if (Main.netMode != 1)
                Item.NewItem(new EntitySource_TileBreak(Position.X * 16, Position.Y * 16), Position.X * 16, Position.Y * 16, 3 * 16, 16, item.netID, 1, false, item.prefix);
            item = new Item();
        }
    }
}
