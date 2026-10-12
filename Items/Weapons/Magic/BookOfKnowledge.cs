using NewDawn.Items.Loot;
using NewDawn.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Magic
{
	public class BookOfKnowledge : NewDawnItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Book of Knowledge");
            // Tooltip.SetDefault("'Rain Lightning upon them'");
			Item.staff[Item.type] = true; 
		}

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 3;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = 5;
			Item.noMelee = true; 
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item33;
            Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<LightningBall>();
			Item.shootSpeed = 14f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, nameof(SteamBar), 16);
            recipe.AddIngredient(ModContent.ItemType<SoulOfThought>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}