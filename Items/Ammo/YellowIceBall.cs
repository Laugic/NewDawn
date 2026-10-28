using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class YellowIceBall : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.YellowIceBall>();
            Item.shootSpeed = 10f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.Snowball, 15);
            recipe.AddIngredient(ItemID.ChlorophyteOre, 1);
            recipe.Register();
        }
    }
}
