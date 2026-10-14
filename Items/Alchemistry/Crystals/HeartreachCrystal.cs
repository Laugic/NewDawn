using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class HeartreachCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases pickup range for life hearts");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<HeartreachTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.lifeMagnet = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HeartreachPotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<NeptusEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<VitaEssence>());
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();
        }
    }
}
