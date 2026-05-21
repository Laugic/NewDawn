using Microsoft.Xna.Framework;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class Gravitron9000 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Gravitron 9000");
            // Tooltip.SetDefault("33% chance to not consume ammo\nConverts normal snowballs into Graviballs");
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useAnimation = Item.useTime = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 55f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
                position.Y -= 5;
            }

            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            float scale = 1f - (Main.rand.NextFloat() * .2f);
            perturbedSpeed = perturbedSpeed * scale;
            if (type == ProjectileID.SnowBallFriendly)
                type = ModContent.ProjectileType<Graviball>();

            Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);

            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Main.rand.NextBool(3))
                return false;
            return base.CanConsumeAmmo(ammo, player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -8);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddIngredient(ItemID.Emerald, 6);
            recipe.AddIngredient(ModContent.ItemType<GravitationCrystal>());
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}