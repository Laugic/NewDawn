using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class ElderootItem : NewDawnItem
    {

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
            Item.rare = ItemRarityID.Blue;
            Item.createTile = ModContent.TileType<Tiles.Radiata>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(TileID.Bottles);
            recipe.AddIngredient(ModContent.ItemType<ElderlilyItem>(), 2);
            recipe.Register();
        }

    }
}