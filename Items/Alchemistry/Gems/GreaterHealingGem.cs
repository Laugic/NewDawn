using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class GreaterHealingGem : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 150;
            player.AddBuff(21, 3600);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(499, 4);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.Register();
        }
    }
}