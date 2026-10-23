using Microsoft.Xna.Framework;
using NewDawn.Items.Materials;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class Hailmaker : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 26;
            Item.useAnimation = Item.useTime = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item13;
            Item.autoReuse = true;
            Item.shootSpeed = 13f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Hailball>(), damage, knockback, player.whoAmI, type, Main.rand.Next(5));
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.AquaScepter);
            recipe.AddIngredient(ModContent.ItemType<ChilledBar>(), 12);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}