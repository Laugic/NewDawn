using NewDawn.Items.Alchemistry.Crystals;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class WasserCrystal : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.gills = true;
            player.ignoreWater = true;
            player.accFlipper = true;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GillsCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FlipperCrystal>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.CrystalineInfuser>());
            recipe.Register();
        }
    }
}