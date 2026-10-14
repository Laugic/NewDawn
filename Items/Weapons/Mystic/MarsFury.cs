using NewDawn.Control;
using NewDawn.Items.Loot;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Weapons.Mystic
{
	public class MarsFury : MysticItem
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mars' Fury");
            // Tooltip.SetDefault("A Furious War\nIllusion inflicts 'Furious', which deals damage over time and\nmakes enemies shoot fireballs when hit\nFires different projectiles based on Mysticism");
        }

		public override void SetMysticDefaults()
		{
            Item.damage = 40;
            Item.width = 66;
			Item.height = 74;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.noMelee = false;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
            Item.scale = 1f;
		}
        
        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Destruction)
            {
                int numberProjectiles = Main.rand.Next(2, 6);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));

                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockBack, player.whoAmI);
                }
            }
            return true;
        }      

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.scale = 1.25f;
            Item.damage = 44;
            Item.useAnimation = Item.useTime = 20;
            Item.knockBack = 8;
            Item.shootSpeed = 16f;
            Item.shoot = ModContent.ProjectileType<MarsDestruction>();
            LuxCost = 6;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.scale = 1f;
            Item.damage = 32;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.knockBack = 4;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<MarsIllusion>();
            VisCost = 4;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.scale = 1f;
            Item.damage = 32;
            Item.useTime = 38;
            Item.useAnimation = 38;
            Item.knockBack = 2;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<MarsConjuration>();
            MundusCost = 20;
        }
        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HadesJudgement>(), 1);
            recipe.AddRecipeGroup(RecipeManager.TitaniumBars, 8);
            recipe.AddIngredient(ModContent.ItemType<SoulOfHaught>(), 8);
            recipe.AddIngredient(ModContent.ItemType<MagmaticCluster>());
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
            
        }
    }
}