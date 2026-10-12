using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Materials
{
    public class Shadowflame : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Accursed Heat");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 12);
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.ShadowFlameKnife);
            recipe.AddIngredient(this, 4);
            recipe.AddIngredient(ItemID.MagicDagger);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = Recipe.Create(ItemID.ShadowFlameBow);
            recipe.AddIngredient(this, 4);
            recipe.AddIngredient(ItemID.HellwingBow);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = Recipe.Create(ItemID.ShadowFlameHexDoll);
            recipe.AddIngredient(this, 4);
            recipe.AddIngredient(ItemID.BookofSkulls);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = Recipe.Create(ItemID.ShadowflameHadesDye);
            recipe.AddIngredient(this, 1);
            recipe.AddTile(TileID.DyeVat);
            recipe.Register();
        }
    }
}