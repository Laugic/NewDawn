using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Materials
{
    public class FrigidCrystal : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 999;
        }
    }
}
