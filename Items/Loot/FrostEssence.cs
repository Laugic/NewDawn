using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class FrostEssence : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            modPlayer.Frost = true;
            modPlayer.SnowDamage += .25f;
        }
    }
}