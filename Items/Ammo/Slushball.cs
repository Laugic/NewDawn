using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class Slushball : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slushball");
            // Tooltip.SetDefault("Inflicts 'Slushie', which makes enemies take more damage from snowballs that aren't Slushballs");
        }

        public override void SetDefaults()
        {
            Item.damage = 6;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.SlushballProjectile>();
            Item.shootSpeed = 14f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.SlushBlock, 1);
            recipe.Register();
        }
    }
}
