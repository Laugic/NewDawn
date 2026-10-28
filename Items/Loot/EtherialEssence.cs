using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Loot
{
    public class EtherialEssence : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.value = 0;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.TruffleWorm);
            recipe.AddTile(TileID.DemonAltar);
            recipe.AddIngredient(Mod, nameof(EtherialEssence), 10);
            recipe.Register();
        }
    }
}