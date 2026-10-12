using NewDawn.Items.Loot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Ranged;
using NewDawn.Items.Accessories;

namespace NewDawn.Items.Weapons.Range
{
    public class Slynger : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slynger");
            // Tooltip.SetDefault("Converts normal snowballs into Clusterballs");
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.crit = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useTime = 20;
            Item.useAnimation = 4 * Item.useTime;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ModContent.ProjectileType<SnowRocket>();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return player.itemAnimation >= Item.useAnimation - 3 * Item.useTime;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 20f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            if (player.itemAnimation >= Item.useAnimation - 3 * Item.useTime)
            {
                SoundEngine.PlaySound(SoundID.Item11, player.Center);
                if(type == ProjectileID.SnowBallFriendly)
                    type = ModContent.ProjectileType<ClusterballProjectile>();
                for (int i = 0; i < 3; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                    float scale = 1f - (Main.rand.NextFloat() * .3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
                }
            }
            return false;
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, -8);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Stynger, 1);
            recipe.AddIngredient(ItemID.EyeoftheGolem, 1);
            recipe.AddIngredient(ModContent.ItemType<Slinger>(), 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}