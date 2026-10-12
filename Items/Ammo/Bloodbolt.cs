using NewDawn.Items.Placeable;
using NewDawn.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class Bloodbolt : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bloodbolt");
            // Tooltip.SetDefault("Heavy, but high damage");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<BloodboltProjectile>();
            Item.shootSpeed = 14f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(90);
            recipe.AddIngredient(ModContent.ItemType<RedIceBall>(), 90);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.Register();
        }
    }
}
