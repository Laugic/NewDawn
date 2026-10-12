using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class Hiveball : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hiveball");
            // Tooltip.SetDefault("'Beeball'");
        }

        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Hiveball>();
            Item.shootSpeed = 8f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddIngredient(ItemID.Snowball, 25);
            recipe.AddIngredient(ItemID.BeeWax);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
