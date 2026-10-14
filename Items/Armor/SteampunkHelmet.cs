using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SteampunkHelmet : NewDawnItem
	{

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 14;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<SteampunkJacket>() && legs.type == ModContent.ItemType<SteampunkBoots>();
        }


        public override void UpdateEquip(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            mysticPlayer.MysticDamage += .17f;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+200% Mystic Duration\nSwitching Mysticisms unleashes a burst of Steam\nDecreased Mystic Burst cooldown\nAttacks inflict 'Steamy!', which makes enemies take more damage";
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.Steamified = true;
            mysticPlayer.MysticSteamBurst = true;
            mysticPlayer.MysticDuration += 2f;
            mysticPlayer.MysticSwitchCoolRate += 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(SteamBar), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}