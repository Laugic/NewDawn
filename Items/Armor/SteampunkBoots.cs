using NewDawn.Buffs;
using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Legs)]
	public class SteampunkBoots : NewDawnItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Insulated- Immune to 'Steamy'\n+10% Movement Speed");
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 14;
		}

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.10f;
            player.buffImmune[ModContent.BuffType<Steamy>()] = true;
            player.buffImmune[144] = true;
        }
        

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, nameof(SteamBar), 18);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}