using NewDawn.Items.Alchemistry.Crystals;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class LebenCrystal : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.lifeRegen = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeMagnet = true;
            player.statLifeMax2 += ((player.statLifeMax + player.statLifeMax2) / 5 / 20 * 20) - (player.statLifeMax / 5 / 20 * 20);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<LifeforceCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<HeartreachCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RegenerationCrystal>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.CrystalineInfuser>());
            recipe.Register();
        }
    }
}