using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using NewDawn.Items.Placeable;

namespace NewDawn.Items.Materials
{
    public class ObsidiumBar : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(silver: 36);
            Item.rare = ItemRarityID.Green;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidiumOre>(), 3);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 1);
            recipe.AddTile(TileID.Hellforge);
            recipe.Register();
        }
        
    }
}