using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class SnowperIceful : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 220;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useAnimation = Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = true;
            Item.shootSpeed = 30f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;


            if (type == ProjectileID.SnowBallFriendly)
                type = ModContent.ProjectileType<Snipeball>();
            Projectile.NewProjectile(source,new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), type, damage, knockback, player.whoAmI);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SniperRifle, 1);
            recipe.AddIngredient(ItemID.Ectoplasm, 12);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}