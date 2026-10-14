using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SteampunkHeadgear : NewDawnItem
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
            player.GetDamage(DamageClass.Throwing) += .2f;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+33% Throwing Velocity, \nAttacks inflict 'Steamy!' ";
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.Steamified = true;
            player.ThrownVelocity += .33f;
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