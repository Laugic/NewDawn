using NewDawn.Items.Equipables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class NewDawnGlobalTile : GlobalTile
    {
        public int miscVar = 0;

        public override void Drop(int i, int j, int type)/* tModPorter Suggestion: Use CanDrop to decide if items can drop, use this method to drop additional items. See documentation. */
        {
            Tile tile = Main.tile[i, j];

            if (tile == null)
                return base.Drop(i, j, type);

            if (tile.TileType == TileID.ShadowOrbs && Main.rand.Next(6) == 0)
            {
                if(i % 2 == 0 && j % 2 == 0)
                {
                    if(!WorldGen.crimson)
                        Item.NewItem(i * 16, j * 16, 8, 8, ModContent.ItemType<DarkfootBoots>(), 1);
                    else
                        Item.NewItem(i * 16, j * 16, 8, 8, ModContent.ItemType<BloodfootBoots>(), 1);
                }
                return false;
            }

            return base.Drop(i, j, type);
        }
    }
}