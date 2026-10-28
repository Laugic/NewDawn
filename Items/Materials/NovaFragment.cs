using Terraria.ID;

namespace NewDawn.Items.Materials
{
	public class NovaFragment : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
}
        public override void SetDefaults()
		{
			Item.width = 20;
            Item.height = 20;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.Cyan;
			Item.useAnimation = 1;
			Item.useTime = 15;
			Item.useStyle = 1;
		}
        /*
        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(3458, 1); //Solar Fragment
            recipe.AddIngredient(3456, 1); //Vortex Fragment
            recipe.AddTile(412);
            recipe.SetResult(this, 2);
            recipe.AddRecipe();
        }*/
	}
}