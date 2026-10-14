using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class ZincBrickWall : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.createWall = Mod.Find<ModWall>("ZincBrickWall").Type;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddTile(18); //Workbench
            recipe.AddIngredient(ModContent.ItemType<ZincBrick>());
            recipe.Register();
        }
        
    }
}