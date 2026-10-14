using Terraria.ID;
using Terraria;

namespace NewDawn.Items.Materials
{
	public class ChilledBar : NewDawnItem
    {
        public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useAnimation = 1;
			Item.useTime = 15;
			Item.useStyle = 1;
            Item.value = Item.sellPrice(silver: 24);
			Item.rare = ItemRarityID.Blue;
        }
	}
}