using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class CalmingCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Decreases enemy spawn rate");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<CalmingTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.calmed = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CalmingPotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(null, Crystallizer.name);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<GraceEssence>(), 2);
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();

            //recipe = Recipe.Create(ModContent.ItemType<SoulEssence>());
            //recipe.AddIngredient(this);
            //recipe.AddCondition(Demystify);
            //recipe.Register();
        }
    }
}
