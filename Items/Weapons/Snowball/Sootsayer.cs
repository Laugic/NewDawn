using NewDawn.Items.Ammo;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Range
{
	public class Sootsayer : NewDawnItem
	{
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soot Sprayer");
            // Tooltip.SetDefault("Converts snowballs into sootballs\nShoots more snowballs as you consume Obsidium Hearts");
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
			Item.width = 46;
			Item.height = 26;
			Item.useTime = 38;
			Item.useAnimation = 30;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 8;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item34;
			Item.autoReuse = true;
			Item.shootSpeed = 14f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            int numberProjectiles = 3 + NewDawnPlayer.Get(player).ObsidiumHeart / 2;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type==ProjectileID.SnowBallFriendly?ModContent.ProjectileType<SootballProjectile>():type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ObsidiumBar>(), 16);
            recipe.AddTile(16);
			recipe.Register();
		}
	}
}