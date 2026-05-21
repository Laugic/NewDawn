using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class LaugicalWorkbench : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("For crafting things to craft things. \n'It's completely laugical!'");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 24;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 50);
            Item.createTile = ModContent.TileType<Tiles.LaugicalWorkbench>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 100);
            recipe.AddIngredient(ItemID.Extractinator);
            recipe.AddIngredient(ItemID.Sawmill);
            recipe.AddIngredient(ItemID.Minecart);
            recipe.AddIngredient(ItemID.MinecartTrack, 10);
            recipe.AddIngredient(ItemID.Chain, 10);
            recipe.Register();
        }
    }
}