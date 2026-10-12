using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class Lycoris : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Plants of hell'");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = ModContent.TileType<Tiles.Lycoris>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddTile(TileID.Bottles);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumPlant>());
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<Lycoris>(), 4);
            recipe.AddTile(TileID.Hellforge);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumRock>(), 4);
            recipe.Register();
        }
    }
}