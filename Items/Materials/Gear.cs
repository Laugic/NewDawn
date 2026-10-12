using Terraria.ID;

namespace NewDawn.Items.Materials
{
	public class Gear : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Like a Cog, but better.'");
        }
        public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 38;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 1;
			Item.useTime = 15;
			Item.useStyle = 1;
            Item.value = 1000;
		}
        
	}
}