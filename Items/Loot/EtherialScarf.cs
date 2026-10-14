using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class EtherialScarf : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.2f;
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawnWorld.downedEtheria || modPlayer.Etherable > 0)
                modPlayer.EtherialScarf = true;
        }
    }
}