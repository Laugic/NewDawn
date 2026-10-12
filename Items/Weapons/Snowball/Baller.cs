using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class Baller : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Baller");
            // Tooltip.SetDefault("Chance to shoot multiple snowballs\n'Looks like it's from the Snowman Mafia'");
        }

        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useAnimation = Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 40f;
            muzzleOffset.Y -= 6;
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(8));
            float scale = 1.1f - (Main.rand.NextFloat() * .2f);
            perturbedSpeed *= scale;
            Projectile.NewProjectile(source, position + muzzleOffset, perturbedSpeed, type, damage, knockback, player.whoAmI);
            if (Main.rand.NextBool(2))
            {
                perturbedSpeed = perturbedSpeed.RotatedByRandom(MathHelper.ToRadians(8));
                perturbedSpeed *= .9f;
                Projectile.NewProjectile(source, position + muzzleOffset, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-24, -8);
        }
    }
}