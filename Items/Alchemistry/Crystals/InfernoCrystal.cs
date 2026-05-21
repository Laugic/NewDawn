using NewDawn.Items.Alchemistry.Essences;
using NewDawn.Items.Materials;
using NewDawn.Tiles;
using NewDawn.Tiles.Alchemistry;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Crystals
{
    public class InfernoCrystal : PotionCrystal
    {
        public override void CustomDefaults()
        {
            Item.createTile = ModContent.TileType<InfernoTile>();
        }

        public override void UpdateEquip(Player player)
        {
            player.AddBuff(116, 2);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.InfernoPotion, 4);
            recipe.AddIngredient(ModContent.ItemType<Frostpetal>());
            recipe.AddTile(null, Crystallizer.name);
            recipe.Register();

            recipe = Recipe.Create(ModContent.ItemType<FireEssence>(), 2);
            recipe.AddIngredient(this);
            recipe.AddCondition(Demystify);
            recipe.Register();
        }
    }
}
