using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Items.Placeable;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class ObsidiumPants : NewDawnItem
	{

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
			Item.defense = 7;
		}

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.waterWalk = true;
            player.ignoreWater = true;
        }
        

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidiumBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<Placeable.ObsidiumRock>(), 20);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumChunk>(), 2);
            recipe.AddTile(16);
			recipe.Register();
		}
	}
}