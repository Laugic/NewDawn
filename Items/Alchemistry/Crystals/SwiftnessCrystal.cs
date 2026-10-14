using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class SwiftnessCrystal : PotionCrystal
    {

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<SwiftnessTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .25f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SwiftnessPotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<HermusEssence>(), 2);
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();
        }
    }
}
