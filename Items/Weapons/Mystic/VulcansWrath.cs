using NewDawn.Items.Loot;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Illusion;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewDawn.Items.Weapons.Mystic
{
	public class VulcansWrath : MysticItem
    {
		public override void SetStaticDefaults()
        {
            Tooltip.AddLine("'Unleash his fury'");
            Tooltip.AddLine("Illusion inflicts 'Steamy'");
            Tooltip.AddLine("Fires different projectiles based on Mysticism");
}

		public override void SetMysticDefaults()
		{
			Item.damage = 48;
            Item.width = 48;
			Item.height = 48;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 1;
			Item.noMelee = false; 
			Item.knockBack = 2;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 6f;
		}

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.GetModPlayer<global::NewDawn.Players.MysticPlayer>().CurrentMysticMode == global::NewDawn.Players.MysticPlayer.MysticMode.Destruction)
            {
                int numberProjectiles = Main.rand.Next(2, 5);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10)); 

                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), 242, damage, knockBack, player.whoAmI);
                }

            }
            return true;
        }

        public override void Destruction(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 54;
            Item.useTime = 32;
            Item.useAnimation = (int)(Item.useTime / 2);
            Item.knockBack = 6;
            Item.shootSpeed = 14f;
            Item.shoot = 242;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1.5f;
            LuxCost = 8;
        }

        public override void Illusion(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 48;
            Item.useTime = 10;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 5;
            Item.shootSpeed = 18f;
            Item.shoot = ModContent.ProjectileType<VulcanIllusion>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1f;
            VisCost = 4;
        }

        public override void Conjuration(global::NewDawn.Players.MysticPlayer modPlayer)
        {
            Item.damage = 48;
            Item.useTime = 30;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 2;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<VulcanConjuration>();
            Item.noUseGraphic = false;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1f;
            MundusCost = 12;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, nameof(SteamBar), 16);
            recipe.AddIngredient(Mod, nameof(SoulOfWrought), 8);
            recipe.AddIngredient(ModContent.ItemType<SoulOfFraught>(), 8);
            recipe.AddIngredient(Mod, nameof(Gear), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}