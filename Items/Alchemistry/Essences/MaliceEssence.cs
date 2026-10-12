using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Essences
{
    public class MaliceEssence : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.value = Item.sellPrice(silver: 25);
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 999;
        }
    }
}
