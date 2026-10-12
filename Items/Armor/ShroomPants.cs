using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class ShroomPants : NewDawnItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shroom Stalk");
            // Tooltip.SetDefault("+10% Movement Speed");
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 2;
		}

        public override void UpdateEquip(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.moveSpeed += 0.1f;
        }
        

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(183, 10);
            recipe.AddIngredient(176, 8);
            recipe.AddTile(16);
            recipe.Register();
        }
	}
}