using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class Shadowshot : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.ShadowshotProjectile>();
            Item.shootSpeed = 14f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(90);
            recipe.AddIngredient(ModContent.ItemType<PurpleIceBall>(), 90);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.Register();
        }
    }
}
