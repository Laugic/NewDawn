using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Weapons.Ammo
{
    public class SnowPile : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Endless Snow Pile");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Snowball);
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Snowball, 3996);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }
    }
}
