using NewDawn.Items.Consumables;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Items.Placeable;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Body)]
	public class ObsidiumLongcoat : NewDawnItem
	{

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
			Item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[24] = true;
        }
        

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidiumBar>(), 20);
            recipe.AddIngredient(ModContent.ItemType<LavaGemItem>(), 8);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumHeart>(), 1);
            recipe.AddTile(16);
			recipe.Register();
		}
	}
}