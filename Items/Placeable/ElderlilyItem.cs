using Terraria.ID;

namespace NewDawn.Items.Placeable
{
    public class ElderlilyItem : NewDawnItem
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
            Item.rare = ItemRarityID.Blue;
            Item.createTile = Terraria.ModLoader.ModContent.TileType<Tiles.Lycoris>();
        }
    }
}