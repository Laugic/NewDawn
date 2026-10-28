using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class RedIceBall : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.RedIceBallProjectile>();
            Item.shootSpeed = 8f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.RedIceBlock, 1);
            recipe.Register();
        }
    }
}
