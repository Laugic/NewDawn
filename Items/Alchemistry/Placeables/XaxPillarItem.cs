using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Placeables
{
    public class XaxPillarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xax Pillar");
            Tooltip.SetDefault("Accumulates Stardust when placed in Space");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 28;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 150;
            item.createTile = ModContent.TileType<Tiles.Alchemistry.Accumulation.XaxAccumulationPillarTile>();
        }
    }
}
