using NewDawn.Tiles.Furniture;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable.Furniture
{
    public class LavaGemLantern : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 1);
            Item.createTile = ModContent.TileType<LavaGemLanternTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumRock>(), 6);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 1);
            recipe.Register();
        }
    }
}