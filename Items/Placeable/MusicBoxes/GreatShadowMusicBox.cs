using Terraria.ModLoader;

namespace NewDawn.Items.Placeable.MusicBoxes
{
    public class GreatShadowMusicBox : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Surface Obsidium)");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 150;
            Item.createTile = ModContent.TileType<Tiles.MusicBoxes.GreatShadowMusicBox>();
            Item.accessory = true;
        }
    }
}