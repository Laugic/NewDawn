using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Materials
{
    public class Crystilla : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("An ancient gemstone");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(silver: 3);
            Item.rare = ItemRarityID.Blue;
        }
    }
}