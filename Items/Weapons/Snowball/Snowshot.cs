using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Snowball
{
    public class Snowshot : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Snowshot");
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 30;
            Item.useAnimation = Item.useTime = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = false;
            Item.shootSpeed = 4.5f;
            Item.useAmmo = AmmoID.Snowball;
            Item.shoot = ProjectileID.SnowBallFriendly;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -2);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BorealWood, 12);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}