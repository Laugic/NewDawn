using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Materials
{
    public class ArcaneShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'A shard of magic'\nHeld by monsters in the dungeon");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(silver: 5);
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
        }
    }
}
