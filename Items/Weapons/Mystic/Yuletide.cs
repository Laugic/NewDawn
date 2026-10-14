using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;
using NewDawn.Items.Loot;
using NewDawn.Items.Materials;

namespace NewDawn.Items.Weapons.Mystic
{
	public class Yuletide : MysticItem
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yuletide");
            // Tooltip.SetDefault("'Holiday Cheer'\nIllusion inflicts 'Frostburn'\nFires different projectiles based on Mysticism");
			Item.staff[Item.type] = true;
		}

		public override void SetMysticDefaults()
		{
			Item.damage = 36;
            Item.width = 48;
			Item.height = 48;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Nothing>();
			Item.shootSpeed = 6f;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (Main.LocalPlayer.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    return "Shoots a stream of icicles that follow your cursor";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Shoots a bouncing snowflake that inflicts 'Brittle', \nwhich makes enemies take more damage based on the speed of what hits them";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Spawns a snowflake that shoots out icicles";
                default:
                    return "";
            }
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 30;
            Item.useAnimation = Item.useTime = 12;
            Item.knockBack = 2;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<YuleDestruction>();
            LuxCost = 4;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 40;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.knockBack = 1;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<YuleIllusion>();
            VisCost = 8;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 36;
            Item.useAnimation = Item.useTime = 30;
            Item.knockBack = 5;
            Item.shootSpeed = 2f;
            Item.shoot = ModContent.ProjectileType<YuleConjuration1>();
            MundusCost = 12;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostShard>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChilledBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<Vitasilk>(), 4);
            recipe.AddTile(16);
			recipe.Register();
		}
    }
}