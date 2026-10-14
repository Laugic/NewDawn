using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class BuilderCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases placement speed and range");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<BuilderTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.tileSpeed += 0.25f;
            player.wallSpeed += 0.25f;
            player.blockRange++;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BuilderPotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<AurumEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<TerronEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();
        }
    }
}
