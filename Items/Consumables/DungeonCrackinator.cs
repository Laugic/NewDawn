using NewDawn.Control;
using NewDawn.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables
{
    public class DungeonCrackinator : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Expert;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    if ((Main.tile[x, y] != null && Main.tile[x, y].TileType == TileID.CrackedBlueDungeonBrick) || Main.tile[x, y].TileType == TileID.CrackedGreenDungeonBrick || Main.tile[x, y].TileType == TileID.CrackedPinkDungeonBrick)
                    {
                        WorldGen.KillTile(x, y);
                    }
                }
            }
            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeManager.DungeonBricks, 100);
            recipe.AddIngredient(ModContent.ItemType<Fissionite>(), 100);
            recipe.Register();
        }
    }
}
