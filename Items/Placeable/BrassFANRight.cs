using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class BrassFANRight : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<Tiles.BrassFANRight>();
        }

        public override void UpdateInventory(Player player)
        {
            if (player.direction == 1)
                Item.createTile = ModContent.TileType<Tiles.BrassFANRight>();
            else
                Item.createTile = ModContent.TileType<Tiles.BrassFAN>();
        }
        /*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(mod, nameof(SteamBar), 4);
            recipe.AddIngredient(mod, nameof(Gear), 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}