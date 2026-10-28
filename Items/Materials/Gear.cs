using Terraria.ID;

namespace NewDawn.Items.Materials
{
	public class Gear : NewDawnItem
    {
        public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 38;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 1;
			Item.useTime = 15;
			Item.useStyle = 1;
            Item.value = 1000;
		}
        
	}
}