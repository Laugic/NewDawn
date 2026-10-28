using NewDawn.Tiles;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Placeable
{
    public class ObsidiumOre : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 12);
            Item.createTile = ModContent.TileType<ObsidiumOreBlock>();
            Item.rare = ItemRarityID.Green;
        }
    }
}