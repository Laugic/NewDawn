using NewDawn.Control;
using NewDawn.Buffs;
using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class MagmaticMask : NewDawnItem
	{

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 22;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Pink;
            Item.defense = 12;
		}
        

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<MagmaticLongcoat>() && legs.type == ModContent.ItemType<MagmaticBoots>();
        }

        public override void UpdateArmorSet(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            player.setBonus = this.GetLocalization("SetBonus").Value;
            modPlayer.Obsidium = true;
            mysticPlayer.Magmatic = true;
            mysticPlayer.MysticObsidiumSwitch = true;
            player.GetCritChance(DamageClass.Throwing) += 15;
            mysticPlayer.MysticSwitchCoolRate += 2;

            if (player.lavaWet)
                player.AddBuff(ModContent.BuffType<Magmatic>(), 60 * 15);
        }


        public override void UpdateEquip(Player player)
        {
        global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            mysticPlayer.MysticDamage += 0.15f;
            player.GetDamage(DamageClass.Throwing) += .15f;
        }
        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidiumHelmet>(), 1);
            recipe.AddRecipeGroup(RecipeManager.TitaniumBars, 12);
            recipe.AddIngredient(ModContent.ItemType<SoulOfHaught>(), 4);
            recipe.AddIngredient(ModContent.ItemType<MagmaticCluster>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();


            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<ObsidiumBand>(), 1);
            recipe2.AddRecipeGroup(RecipeManager.TitaniumBars, 12);
            recipe2.AddIngredient(ModContent.ItemType<SoulOfHaught>(), 4);
            recipe2.AddIngredient(ModContent.ItemType<MagmaticCluster>(), 1);
            recipe2.AddTile(134);
            recipe2.Register();
        }
    }
}