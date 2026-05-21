using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class Avalanche : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("50% chance not to consume ammo\n'Look out below!'");
        }

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 106;
            Item.height = 58;
            Item.useTime = Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.NextBool(2))
                return false;
            return base.CanConsumeAmmo(ammo, player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 65f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(10));

                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed *= scale;
                if (i % 3 == 0)
                    Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<BigSnowball>(), damage, knockback, player.whoAmI, type);
                else
                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Frostball>(), damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, -15);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FrostThrower>());
            recipe.AddIngredient(ItemID.ShroomiteBar, 16);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}