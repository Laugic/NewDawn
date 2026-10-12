using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Equipables
{
    public class CrystalizedMagma : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crystallized Magma");
            // Tooltip.SetDefault("Leave a trail of fire as you run");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.FireTrail = true;
        }
    }
}