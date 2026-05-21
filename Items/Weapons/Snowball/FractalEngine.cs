using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Ranged;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class FractalEngine : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fractal Engine");
            // Tooltip.SetDefault("66% chance to not consume ammo\n'Compounding'");
        }

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useAnimation = Item.useTime = 9;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item92;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= .66f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 50f;
            muzzleOffset.Y -= 6;
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            Projectile.NewProjectile(source, position + muzzleOffset, perturbedSpeed, ModContent.ProjectileType<BigSnowball>(), damage, knockback, player.whoAmI, type);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-24, -8);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FragmentVortex, 18);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}