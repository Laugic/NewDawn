using NewDawn.Projectiles.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Magic
{
	public class WarlockStaff : NewDawnItem
    {
        private int reload = 0;
        private int reloadMax = 12;
        public override void SetStaticDefaults()
        {
			Item.staff[Item.type] = true;
}

		public override void SetDefaults()
		{
			Item.damage = 550;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 6;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 2;
			Item.useAnimation = 24;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<WarlockStaff3>();
			Item.shootSpeed = 36f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            reload--;
            Vector2 target = Main.MouseWorld;
            Vector2 vel = player.DirectionTo(target) * Item.shootSpeed;
            if (reload <= 0)
            {
                reload = reloadMax;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), player.Center.X, player.Center.Y, vel.X, vel.Y, ModContent.ProjectileType<WarlockStaff1>(), (int)(Item.damage) * 4, 3, Main.myPlayer);
            }
            return true;
        }/*
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(3467, 16);
            recipe.AddIngredient(3457, 8);
            recipe.AddTile(412);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}