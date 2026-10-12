using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class CogOfEtheria : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Annihilation");
            // Tooltip.SetDefault("Killing an enemy while in the Etherial boosts your damage by 20% for 10 seconds. Killing another enemy in this time resets the timer and stacks the bonus.");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawnWorld.downedEtheria || modPlayer.Etherable > 0)
                modPlayer.EtherCog = true;
        }
    }
}