using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class Radiata : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Roots of hell'");
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
            Item.createTile = ModContent.TileType<Tiles.Radiata>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(13); //Bottle
            recipe.AddIngredient(ModContent.ItemType<Lycoris>(), 2);
            recipe.Register();
        }
            
    }
}