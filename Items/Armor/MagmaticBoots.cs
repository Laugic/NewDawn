using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class MagmaticBoots : NewDawnItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("+100% Mystic Duration\n+50%Throwing Velocity\n+15% Movement Speed\nProvides waterwalking and free movement in liquids");
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 12;
		}

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.MysticDuration += 1f;
            player.ThrownVelocity += .5f;
            player.waterWalk = true;
            player.ignoreWater = true;
        }
        

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidiumPants>(), 1);
            recipe.AddRecipeGroup("TitaniumBars", 10);
            recipe.AddIngredient(ModContent.ItemType<SoulOfHaught>(), 4);
            recipe.AddIngredient(ModContent.ItemType<MagmaticCluster>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}