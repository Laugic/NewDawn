using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class ArtifactOfRestoration : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.value = 100;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 600;
            player.statManaMax2 += 160;
            player.AddBuff(21, 3600);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<UltraHealingRelic>(), 1);
            recipe.AddIngredient(ModContent.ItemType<UltraManaRelic>(), 1);
            recipe.AddTile(412);
            recipe.Register();
        }
    }
}