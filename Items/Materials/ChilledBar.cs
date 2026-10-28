using NewDawn.Control;
using Terraria;

namespace NewDawn.Items.Materials
{
    public class ChilledBar : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.useAnimation = 1;
            Item.useTime = 15;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(silver: 24);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.IceMachine);
            recipe.AddRecipeGroup(RecipeManager.EvilBar, 1);
            recipe.AddRecipeGroup(RecipeManager.GoldBars, 1);
            recipe.AddIngredient(ItemID.IceBlock, 4);
            recipe.Register();
        }
    }
}