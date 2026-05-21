using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using NewDawn.Items.Materials;
using Terraria.ModLoader;
using NewDawn.Tiles.Alchemistry;
using NewDawn.Items.Alchemistry.Essences;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class AmmoReservationCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("20% chance not to consume ammo");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<AmmoReservationTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.ammoCost80 = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AmmoReservationPotion, 4);
            recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(null, Crystallizer.name);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<LightEssence>());
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
