using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using NewDawn.Items.Materials;
using Terraria.ModLoader;
using NewDawn.Tiles.Alchemistry;
using NewDawn.Items.Alchemistry.Essences;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class ShineCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Emits an aura of light");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<ShineTile>();
        }
        
        public override void UpdateEquip(Player player)
        {
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ShinePotion, 4);
            recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(null, Crystallizer.name);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<LightEssence>(), 2);
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();
        }
    }
}
