using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class FlareburstWaders : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Flamespark Waders");
            // Tooltip.SetDefault("Provides the ability to walk on water and lava\nGrants immunity to fire blocks and 10 seconds of immunity to lava\nLeave a trail of True Fire as you move");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 10 * 60;
            if (!hideVisual)
                player.GetModPlayer<NewDawnPlayer>().TrueFireTrail = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LavaWaders, 1);
            recipe.AddIngredient(ModContent.ItemType<CrystalizedMagma>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FireDust>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();

            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.LavaWaders, 1);
            recipe.AddIngredient(ModContent.ItemType<CrystalizedMagma>(), 1);
            recipe.AddIngredient(ItemID.MagmaStone, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}