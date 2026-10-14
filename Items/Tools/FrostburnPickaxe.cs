using NewDawn.Items.Loot;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Tools
{
	public class FrostburnPickaxe : NewDawnItem
	{

		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 10;
			Item.useAnimation = 20;
			Item.pick = 160;
			Item.useStyle = 1;
			Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrigidPickaxe>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ObsidiumPickaxe>(), 1);
            recipe.AddIngredient(Mod, nameof(SoulOfSought), 8);
            recipe.AddIngredient(Mod, nameof(SoulOfHaught), 8);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}