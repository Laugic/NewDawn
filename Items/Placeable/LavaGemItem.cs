using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class LavaGemItem : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lava Gem");
            // Tooltip.SetDefault("Grows on Obsidium Rock\n'Molten Candy'");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = ModContent.TileType<Tiles.LavaGem>();
            Item.bait = 20;
        }
    }
}