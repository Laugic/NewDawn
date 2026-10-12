using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class EyeOfEtheria : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eye of Etheria");
            // Tooltip.SetDefault("Allows you to see all creatures, no matter which dimension you are in.");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.EtherVision = true;
        }
        
    }
}