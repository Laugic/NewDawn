using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class ZincBrick : NewDawnItem
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
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = ModContent.TileType<Tiles.ZincBrick>();
        }


        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(17); //Furnace
            recipe.AddIngredient(ModContent.ItemType<ZincOre>());
            recipe.AddIngredient(3); //Stone
            recipe.Register();
        }
    }
}