using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Equipables
{
    public class Ragnashia : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.defense = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<NewDawnPlayer>().CritBoost(5);
            player.GetModPlayer<NewDawnPlayer>().Blaze = true;
        }
    }
}