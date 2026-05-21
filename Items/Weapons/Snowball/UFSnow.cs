using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class UFSnow : ModItem
    {
        public int numShots = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("U.F.Snow");
            // Tooltip.SetDefault("Converts normal snowballs into dark iceballs\nDoesn't consume ammo if you're wearing all Meteorite armor");
        }
        public override void SetDefaults()
        {
            Item.damage = 17;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 60;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 2);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item157;
            Item.autoReuse = true;
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;

            
            Item.useTime = Item.useAnimation = 17;
            //Item.reuseDelay = 15;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.armor[0].type == ItemID.MeteorHelmet && player.armor[1].type == ItemID.MeteorSuit && player.armor[2].type == ItemID.MeteorLeggings )
                return false;
            return base.CanConsumeAmmo(ammo, player);
        }


        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 38f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
                position += muzzleOffset;
            position.Y -= 5;


            if (type == ProjectileID.SnowBallFriendly)
                type = ModContent.ProjectileType<DarkIceball>();
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);

            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MeteoriteBar, 20);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}