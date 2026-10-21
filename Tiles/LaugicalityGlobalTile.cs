using NewDawn.Items.Equipables;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class NewDawnGlobalTile : GlobalTile
    {

        public override bool CanDrop(int i, int j, int type)
        {
            var suppressShadowOrbDrop = type == TileID.ShadowOrbs && Main.rand.Next(6) == 0;
            if (suppressShadowOrbDrop)
            {
                int itemType = WorldGen.crimson ? ModContent.ItemType<BloodfootBoots>() : ModContent.ItemType<DarkfootBoots>();
                Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 8, 8, itemType, 1);
                return false;
            }
            return !suppressShadowOrbDrop;
        }
    }
}