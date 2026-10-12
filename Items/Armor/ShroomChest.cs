using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class ShroomChest : NewDawnItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shroom Amalgam");
            // Tooltip.SetDefault("+20% Overflow");
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.GlobalOverflow += .2f;
        }
        
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GlowingMushroom, 16);
            recipe.AddIngredient(ItemID.MudBlock, 12);
            recipe.AddIngredient(ModContent.ItemType<ArcaneShard>(), 8);
            recipe.AddTile(16);
            recipe.Register();
        }
	}
}