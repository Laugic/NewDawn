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
    public class Snowtillery : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snowtillery");
            // Tooltip.SetDefault("Shoots snow rockets that push the player as well");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useTime = 14;
            Item.useAnimation = 6 * Item.useTime;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 12;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shootSpeed = .01f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ModContent.ProjectileType<SnowRocket>();
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return player.itemAnimation >= Item.useAnimation - 4 * Item.useTime;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 20f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            if (player.itemAnimation >= Item.useAnimation - 4 * Item.useTime)
            {
                SoundEngine.PlaySound(SoundID.Item11, player.Center);
                type = ModContent.ProjectileType<SnowRocket>();
                Projectile.NewProjectile(new EntitySource_Parent(Entity), position.X, position.Y, speedX * .25f, speedY * .25f, type, damage, knockback, player.whoAmI);
            }
            return false;
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 8);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SnowballCannon, 1);
            recipe.AddIngredient(ItemID.IllegalGunParts, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}