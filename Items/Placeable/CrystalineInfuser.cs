using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class CrystalineInfuser : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.createTile = ModContent.TileType<Tiles.CrystalineInfuser>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddTile(ModContent.TileType<Tiles.LaugicalWorkbench>());
            recipe.AddIngredient(ModContent.ItemType<ObsidiumBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<ChilledBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 8);
            recipe.AddIngredient(ModContent.ItemType<FrostShard>(), 1);
            recipe.Register();
        }
    }
}