using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Ammo
{
    public class YellowSnowball : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yellow Snowball");
            // Tooltip.SetDefault("Homes in on enemies as it falls");
        }

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
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.YellowSnowball>();
            Item.shootSpeed = 8f;
            Item.ammo = AmmoID.Snowball;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.Snowball, 50);
            recipe.AddIngredient(ItemID.ChlorophyteBar);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }
    }
}
