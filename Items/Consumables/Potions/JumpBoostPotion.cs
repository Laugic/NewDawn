using NewDawn.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Consumables.Potions
{
	public class JumpBoostPotion : NewDawnItem
    {
        public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.maxStack = 30;
			Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = 2;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
            Item.value = Item.sellPrice(silver: 2);
        }
        

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.AddBuff(ModContent.BuffType<JumpBoost>(), 5*60*60, true);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.Feather, 1);
            recipe.AddIngredient(ItemID.PinkGel, 1);
            recipe.AddIngredient(ItemID.Emerald, 1);
            recipe.AddTile(13);
            recipe.Register();
        }
    }
}