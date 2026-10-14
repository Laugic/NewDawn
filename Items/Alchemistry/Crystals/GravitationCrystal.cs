using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class GravitationCrystal : PotionCrystal
    {

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<GravitationTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.gravControl = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GravitationPotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<FluxEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<HermusEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();
        }
    }
}
