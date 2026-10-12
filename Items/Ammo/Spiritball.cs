using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Ammo
{
    public class Spiritball : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spiritball");
            // Tooltip.SetDefault("Flies back towards you");
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 7f;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Spiritball>();
            Item.shootSpeed = 12f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.Ectoplasm);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}
