using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class MineralEnchanter : NewDawnItem
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
            Item.value = Item.buyPrice(silver: 50) * 5;
            Item.createTile = ModContent.TileType<Tiles.MineralEnchanterTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddIngredient(1198, 12);
            recipe.AddIngredient(520, 8);
            recipe.AddIngredient(531, 1); //Spell Tome
            recipe.Register();
        }
    }
}