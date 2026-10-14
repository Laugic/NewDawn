using NewDawn.Items.Alchemistry.Crystals;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Gems
{
    public class PanzerCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tank Crystal");
            // Tooltip.SetDefault("+10% Damage reduction \n Attackers take damage");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.defense = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.10f;
            if (player.thorns < 1f)
            {
                player.thorns = 0.5f;
            }

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IronskinCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EnduranceCrystal>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ThornsCrystal>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.CrystalineInfuser>());
            recipe.Register();
        }
    }
}