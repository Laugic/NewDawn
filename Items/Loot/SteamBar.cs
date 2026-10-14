using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class SteamBar : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Pink;
        }
    }
}