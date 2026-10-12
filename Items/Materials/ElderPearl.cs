using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Materials
{
    public class ElderPearl : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'It's emitting a calming energy.'");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = 1;
            Item.value = 0;
            Item.rare = ItemRarityID.LightPurple;
        }
        
    }
}