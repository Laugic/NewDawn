using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class AntiIceball : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<AntiIceballProjectile>();
            Item.shootSpeed = 14f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(333);
            recipe.AddIngredient(ModContent.ItemType<IceBall>(), 333);
            recipe.AddIngredient(ItemID.GravitationPotion, 1);
            recipe.Register();
        }
    }
}
