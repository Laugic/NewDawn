using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class FishingCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("15% increased fishing power");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<FishingTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.fishingSkill += 15;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FishingPotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(null, Crystallizer.name);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<NeptusEssence>(), 2);
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();

            //recipe = Recipe.Create(ModContent.ItemType<ForceEssence>());
            //recipe.AddIngredient(this);
            //recipe.AddCondition(Demystify);
            //recipe.Register();
        }
    }
}
