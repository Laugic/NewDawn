using NewDawn.Items.Alchemistry.Crystals;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class VerteCrystal : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.maxMinions++;
            if (modPlayer.calm)
                player.calmed = true;
            player.resistCold = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<CalmingCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<WarmthCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SummoningCrystal>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.CrystalineInfuser>());
            recipe.Register();
        }
    }
}