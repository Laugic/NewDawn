using NewDawn.Control;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class CrystallizerItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crystallizer");
            // Tooltip.SetDefault("Crystallize magical liquids");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 32;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.createTile = ModContent.TileType<Tiles.Crystallizer>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 50);
            recipe.AddIngredient(ItemID.IceMachine, 1);
            recipe.AddRecipeGroup(RecipeManager.LargeGems, 1);
            recipe.Register();
        }
    }
}