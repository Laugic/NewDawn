using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewDawn.Items.Weapons.Mystic
{
	public class HermesFlight : MysticItem
    {
		public override void SetStaticDefaults()
        {
			Item.staff[Item.type] = true;
}

		public override void SetMysticDefaults()
		{
			Item.damage = 20;
            Item.width = 28;
			Item.height = 28;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2;
            Item.value = Item.sellPrice(silver: 40);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<HermesDestruction>();
			Item.shootSpeed = 6f;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (Main.LocalPlayer.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode)
            {
                case global::NewDawn.Players.MysticPlayer.MysticMode.Destruction:
                    return "Shoots a burst of feathers";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Illusion:
                    return "Shoots gusts of wind that inflict 'Aerial weakness',\nwhich makes enemies take more damage when you are above them";
                case global::NewDawn.Players.MysticPlayer.MysticMode.Conjuration:
                    return "Shoots feathers that orbit you";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Destruction)
            {
                int numberProjectiles = Main.rand.Next(2, 4);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<HermesDestruction>(), damage, knockBack, player.whoAmI);
                }
            }
            return true;
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 15;
            Item.useTime = 30;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 4;
            Item.shootSpeed = 12;
            Item.shoot = ModContent.ProjectileType<HermesDestruction>();
            LuxCost = 10;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 22;
            Item.useTime = 30;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<HermesIllusion>();
            VisCost = 10;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 15;
            Item.useTime = 15;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 5;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<HermesConjuration>();
            MundusCost = 5;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 12);
            recipe.AddIngredient(ItemID.Feather, 4);
            recipe.AddIngredient(ItemID.FallenStar, 2);
            recipe.AddTile(16);
			recipe.Register();
		}
    }
}