using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class BattleCrystal : PotionCrystal
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Increases enemy spawn rate");
        }

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<BattleTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.enemySpawns = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BattlePotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(null, Crystallizer.name);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<MaliceEssence>(), 2);
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
