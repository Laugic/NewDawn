using NewDawn.Tiles;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class TempleEntrance : ModItem
    {

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Temple.TempleEntrance>());
            Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(gold: 1));
        }
    }
}