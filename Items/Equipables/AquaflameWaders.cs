using NewDawn.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class AquaflameWaders : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Dominion over liquids and fire\nLeave a trail of True Fire as you move");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[24] = true;
            player.gills = true;
            player.ignoreWater = true;
            player.accFlipper = true;
            if (!hideVisual)
                player.GetModPlayer<NewDawnPlayer>().TrueFireTrail = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FlareburstWaders>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MagmaHeart>(), 1);
            recipe.AddIngredient(ItemID.DivingGear, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}