using NewDawn.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class Barrier : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = ModContent.TileType<BarrierTile>();
        }

        public override void HoldItem(Player player)
        {
            player.GetModPlayer<NewDawnPlayer>().HoldingBarrier = true;
            base.HoldItem(player);
        }
    }
}