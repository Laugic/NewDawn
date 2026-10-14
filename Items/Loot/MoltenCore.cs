using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class MoltenCore : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 44;
            Item.value = Item.sellPrice(gold:4);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 50;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.MoltenCore = 2;
        }
    }
}