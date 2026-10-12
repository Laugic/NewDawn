using Terraria.ID;
using Terraria;

namespace NewDawn.Items.Materials
{
    public class MagmaSnapper : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Don't get your hand too close.'");
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 58;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(silver: 15);
            Item.rare = ItemRarityID.Blue;
        }
        
    }
}