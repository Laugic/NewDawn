using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Items.Standard;
using NewDawn.Tiles.Subworlds.MoldyrTemple;

namespace Laugicality.Items.Placeable
{
    public class TestPillarItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hi");
        }

        public override void SetDefaults()
        {
            item.width = 54;
            item.height = 27;
            item.maxStack = 1;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 150;
            item.createTile = ModContent.TileType<TestTriggerTile>();
        }
    }
}