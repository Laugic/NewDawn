using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class Blizzard : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("50% chance not to consume ammo");
        }

        public override void SetDefaults()
        {
            Item.damage = 66;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 72;
            Item.height = 36;
            Item.useTime = Item.useAnimation = 4;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
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
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 40f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;

            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            float scale = 1f - (Main.rand.NextFloat() * .3f);
            perturbedSpeed *= scale;

            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, -5);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<SnowBlower>());
            recipe.AddIngredient(ModContent.ItemType<Gravitron9000>());
            recipe.AddIngredient(ModContent.ItemType<SnowperIceful>());
            recipe.AddIngredient(ItemID.StaffoftheFrostHydra);
            recipe.AddIngredient(ItemID.BlizzardStaff);
            recipe.AddIngredient(ItemID.NorthPole);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}