using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Equipables
{
    public class CrystalizedMagma : NewDawnItem
    {

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