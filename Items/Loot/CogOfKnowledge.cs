using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class CogOfKnowledge : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.maxMinions += 2;
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(2328, 4);
            recipe.AddTile(ModContent.TileType<Tiles.AlchemicalInfuser>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}