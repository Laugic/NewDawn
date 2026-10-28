namespace NewDawn.Items.Loot
{
    public class FrigidEssence : NewDawnItem
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
            Item.value = 0;
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(77);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumOre>(), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
        
    }
}