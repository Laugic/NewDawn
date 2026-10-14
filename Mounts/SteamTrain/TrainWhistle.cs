using NewDawn.Items;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Mounts.SteamTrain
{
	public class TrainWhistle : NewDawnItem
	{
		public override void SetStaticDefaults()
		{
			// Tooltip.SetDefault("Calls a Train for you to ride! \n'Not so suspicious'");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 36;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.value = 30000;
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item79;
			Item.noMelee = true;
			Item.mountType = ModContent.MountType<SteamTrainMount>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();

			recipe.AddIngredient(Mod, nameof(SteamBar), 16);
            recipe.AddIngredient(Mod, nameof(Gear), 20);
            recipe.AddIngredient(Mod, nameof(SoulOfWrought), 8);

            recipe.AddTile(TileID.MythrilAnvil);

			recipe.Register();
		}
	}
}