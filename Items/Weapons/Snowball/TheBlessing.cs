using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class TheBlessing : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Blessing");
            // Tooltip.SetDefault("Converts some snowballs into blessedballs");
        }
        public override void SetDefaults()
        {
            Item.damage = 32;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 26;
            Item.useTime = Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item28;
            Item.autoReuse = true;
            Item.shootSpeed = 8f;
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

            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed *= scale;
                if (type == ProjectileID.SnowBallFriendly)
                    type = ModContent.ProjectileType<Blessedball>();
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, -5);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrystalShard, 12);
            recipe.AddIngredient(ItemID.CobaltBar, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CrystalShard, 12);
            recipe.AddIngredient(ItemID.PalladiumBar, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}