using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable.MusicBoxes
{
    public class AnDioMusicBox : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<Tiles.MusicBoxes.AnDioMusicBox>();
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddIngredient(ItemID.MusicBox, 1);
            recipe.AddIngredient(ItemID.Granite, 20);
            recipe.AddIngredient(ItemID.Marble, 20);
            recipe.Register();

            recipe = Recipe.Create(ItemID.MusicBox);
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 20);
            recipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 5);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 4);
            recipe.Register();
        }
    }
}