using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Materials
{
	public class GalacticFragment : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'The secrets of the universe swirl around this fragment'");
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
		{
			Item.width = 20;
            Item.height = 20;
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Cyan;
			Item.useAnimation = 1;
			Item.useTime = 15;
			Item.useStyle = 1;
            Item.value = Item.buyPrice(silver: 20) * 5;
		}
        
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient(3457, 1); //Nebula Fragment
            recipe.AddIngredient(3459, 1); //Stardust Fragment
            recipe.AddTile(412);
            recipe.Register();
        }
	}
}