using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using NewDawn.Projectiles.Mystic.Conjuration;
using NewDawn.Projectiles.Mystic.Destruction;
using NewDawn.Projectiles.Mystic.Illusion;
using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Special;

namespace NewDawn.Items.Weapons.Mystic
{
	public class FriggsPhalanx : MysticItem
    {
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frigg's Phalanx");
            // Tooltip.SetDefault("'The swarm cometh'");
			Item.staff[Item.type] = true;
		}

        public override void SetMysticDefaults()
		{
			Item.damage = 25;
            Item.width = 48;
			Item.height = 48;
			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Nothing>();
			Item.shootSpeed = 6f;
        }

        public override string GetExtraTooltip()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            switch (NewDawnPlayer.MysticMode)
            {
                case 1:
                    return "Shoots a spread of bees and spores";
                case 2:
                    return "Shoots spore clouds that inflict 'Pollinated', \nwhich makes bees deal more damage and be consumed on hit";
                case 3:
                    return "Spawns a hive that shoots bees";
                default:
                    return "";
            }
        }

        public override bool MysticShoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 velocity = new Vector2(speedX, speedY);
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (modPlayer.MysticMode == 1)
            {
                int numberProjectiles = 2 + Main.rand.Next(1, 4);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    if(Main.rand.Next(3) == 0)
                    {
                        if(player.strongBees && Main.rand.Next(3) == 0)
                            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileID.GiantBee, (int)(damage * 1.5), knockBack, player.whoAmI);
                        else
                            Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileID.Bee, damage, knockBack, player.whoAmI);
                    }
                    else
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<FriggDestruction>(), damage, knockBack, player.whoAmI);
                }
            }
            if(modPlayer.MysticMode == 2)
            {
                float theta = (float)Main.rand.NextDouble() * 3.14f * 2;
                float mag = 360;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2((int)(Main.MouseWorld.X) + (int)(mag * Math.Cos(theta)), (int)(Main.MouseWorld.Y) + (int)(mag * Math.Sin(theta))), new Vector2(-4 * (float)Math.Cos(theta), -4 * (float)Math.Sin(theta)), ModContent.ProjectileType<FriggIllusion>(), damage, 3, Main.myPlayer);
            }
            return true;
        }

        public override void Destruction(NewDawnPlayer modPlayer)
        {
            Item.damage = 20;
            Item.useTime = 20;
            Item.useAnimation = Item.useTime;
            Item.knockBack = 1f;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            LuxCost = 8;
        }

        public override void Illusion(NewDawnPlayer modPlayer)
        {
            Item.damage = 25;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.knockBack = 1;
            Item.shootSpeed = 8f;
            Item.shoot = ModContent.ProjectileType<Nothing>();
            VisCost = 5;
        }

        public override void Conjuration(NewDawnPlayer modPlayer)
        {
            Item.damage = 20;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.knockBack = 5;
            Item.shootSpeed = 2f;
            Item.shoot = ModContent.ProjectileType<FriggConjuration>();
            MundusCost = 16;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(331, 10);
            recipe.AddIngredient(2431, 8);
            recipe.AddTile(16);
			recipe.Register();
		}
    }
}