using NewDawn.Items.Loot;
using NewDawn.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Magic
{
	public class Stationator : NewDawnItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("'Here they come'");
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 150;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 12;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 32;
			Item.useAnimation = 32;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<TrainScytheProjectile>();
			Item.shootSpeed = 14f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod, nameof(SteamBar), 16);
            recipe.AddIngredient(Mod, nameof(SoulOfWrought), 8);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}