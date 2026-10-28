using NewDawn.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Materials
{
    public class ArcaneShard : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(5);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddIngredient(ModContent.ItemType<Fissionite>(), 10);
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();
        }
    }
}
