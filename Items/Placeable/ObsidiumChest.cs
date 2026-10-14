using NewDawn.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class ObsidiumChest : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.sellPrice(silver: 1);
            Item.createTile = ModContent.TileType<Tiles.ObsidiumChest>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(ObsidiumBar), 8);
            recipe.AddIngredient(22, 2);
            recipe.AddTile(16);
            recipe.Register();
        }
    }
}