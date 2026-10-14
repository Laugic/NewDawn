using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class MagicPowerCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("10% increased magic damage");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<MagicPowerTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.10f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MagicPowerPotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<FluxEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<MaliceEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();
        }
    }
}
