using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using NewDawn.Items.Materials;
using Terraria.ModLoader;
using NewDawn.Tiles.Alchemistry;
using NewDawn.Items.Alchemistry.Essences;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class TitanCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases knockback by 50%");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<TitanTile>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetKnockback(DamageClass.Generic) += .50f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TitanPotion, 4);
            recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(null, Crystallizer.name);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<EarthEssence>());
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
