using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class MagmaticCluster : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 22;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
        }
    }
}