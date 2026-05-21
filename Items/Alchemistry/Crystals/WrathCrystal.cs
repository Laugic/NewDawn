using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using NewDawn.Items.Materials;
using Terraria.ModLoader;
using NewDawn.Tiles.Alchemistry;
using NewDawn.Items.Alchemistry.Essences;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class WrathCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases damage by 10%");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<WrathTile>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.WrathPotion, 4);
            recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(null, Crystallizer.name);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<FireEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<ForceEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();
        }
    }
}
