using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class RageCrystal : PotionCrystal
    {

        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<RageTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 10;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.RagePotion, 4);
            //recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(ModContent.TileType<Tiles.Crystallizer>());
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<AurumEssence>());
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
