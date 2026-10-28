using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class AncientShard : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 26;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(silver: 25);
            Item.rare = ItemRarityID.Blue;
        }
    }
}