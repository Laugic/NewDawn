using System;
using NewDawn.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Range
{
	public class StarHunter : NewDawnItem
	{

        private int reload = 0;
        private int reloadMax = 8;
        private int reload2 = 0;
        private int reloadMax2 = 32;
        private float theta = 0f;
        private float rotSp = 3.14f / 30;

		public override void SetDefaults()
        {
            Item.scale *= 1.2f;
            Item.damage = 550;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 44;
			Item.height = 86;
			Item.useTime = 1;
			Item.useAnimation = 32;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item5;
			Item.autoReuse = true;
            Item.channel = true;
            Item.shoot = 10; 
			Item.shootSpeed = 22f;
			Item.useAmmo = 40;
		}
        /*
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(3467, 16);
            recipe.AddIngredient(3456, 8);
            recipe.AddTile(412);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
            
        public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .50f;
		}

        public override void HoldItem(Player player)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if(reload > 0)
                reload--;
            if (reload2 > 0)
                reload2--;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 50f;
            //Super shot
            if(reload2 <= 0)
            {
                reload2 = reloadMax2;

                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 14, 0, ModContent.ProjectileType<Luminarrow>(), (int)(Item.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, -14, 0, ModContent.ProjectileType<Luminarrow>(), (int)(Item.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 0, 14, ModContent.ProjectileType<Luminarrow>(), (int)(Item.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 0, -14, ModContent.ProjectileType<Luminarrow>(), (int)(Item.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 10, 10, ModContent.ProjectileType<Luminarrow>(), (int)(Item.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, 10, -10, ModContent.ProjectileType<Luminarrow>(), (int)(Item.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, -10, -10, ModContent.ProjectileType<Luminarrow>(), (int)(Item.damage / 1.2f), 3, Main.myPlayer);
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, -10, 10, ModContent.ProjectileType<Luminarrow>(), (int)(Item.damage / 1.2f), 3, Main.myPlayer);
            }
            //Normal shot
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            float mag = 12f;
            theta += rotSp;
            if (theta >= 3.14158265f * 2)
                theta -= 3.14158265f * 2;
            Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, (float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag, ModContent.ProjectileType<LuminarrowHead>(), (int)(Item.damage) / 2, 3, Main.myPlayer);

            //Normal shot
            if (reload <= 0)
            {
                reload = reloadMax;
                int numberProjectiles = Main.rand.Next(2, 4);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10)); 
                                                                                                                
                    float scale = 1f - (Main.rand.NextFloat() * .2f);
                    perturbedSpeed = perturbedSpeed * scale;
                    if(Main.player[Main.myPlayer] == player)
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), 638, damage, knockback, player.whoAmI);
                }
            }


            return false; 
        }
    }
}
