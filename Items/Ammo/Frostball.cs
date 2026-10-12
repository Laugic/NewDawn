using NewDawn.Items.Loot;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class Frostball : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frostball");
            // Tooltip.SetDefault("Bounces off of enemies\nInflicts 'Frostburn'");
        }

        public override void SetDefaults()
        {
            Item.damage = 13;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<FrostballProjectile>();
            Item.shootSpeed = 14f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(499);
            recipe.AddIngredient(ItemID.FrostCore, 1);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSought>(), 5);
            recipe.Register();
        }
    }
}
