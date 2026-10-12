using NewDawn.Tiles;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class DarkShard : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magma Shard");
            // Tooltip.SetDefault("Grows Obsidium Hearts over time when placed");
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 26;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<MagmaVeins>();
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Orange;
        }
    }
}