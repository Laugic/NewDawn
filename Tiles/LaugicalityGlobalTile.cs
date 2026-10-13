using NewDawn.Items.Equipables;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class NewDawnGlobalTile : GlobalTile
    {
        private int pendingShadowOrbX = -1;
        private int pendingShadowOrbY = -1;
        private bool suppressShadowOrbDrop;
        private bool dropShadowOrbBoots;

        public override bool CanDrop(int i, int j, int type)
        {
            suppressShadowOrbDrop = type == TileID.ShadowOrbs && Main.rand.Next(6) == 0;
            dropShadowOrbBoots = suppressShadowOrbDrop && i % 2 == 0 && j % 2 == 0;
            pendingShadowOrbX = i;
            pendingShadowOrbY = j;
            return !suppressShadowOrbDrop;
        }

        public override void Drop(int i, int j, int type)
        {
            if (!suppressShadowOrbDrop || i != pendingShadowOrbX || j != pendingShadowOrbY || type != TileID.ShadowOrbs)
                return;

            suppressShadowOrbDrop = false;
            if (!dropShadowOrbBoots)
                return;

            dropShadowOrbBoots = false;
            int itemType = WorldGen.crimson
                ? ModContent.ItemType<BloodfootBoots>()
                : ModContent.ItemType<DarkfootBoots>();
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 8, 8, itemType, 1);
        }
    }
}