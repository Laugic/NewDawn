using NewDawn.Items.Loot;
using NewDawn.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class EtherialBlock : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = ModContent.TileType<EtherialTile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AntitherialBlock>());
            recipe.Register();

            Recipe erecipe = CreateRecipe(20);
            erecipe.AddIngredient(Mod, nameof(EtherialEssence));
            erecipe.Register();
        }
    }
}