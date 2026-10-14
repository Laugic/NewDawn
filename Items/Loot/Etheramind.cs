using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Loot
{
    [AutoloadEquip(EquipType.Wings)]
    public class Etheramind : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawnWorld.downedEtheria || modPlayer.Etherable > 0)
                player.wingTimeMax = 210;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
    ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawnWorld.downedEtheria || modPlayer.Etherable > 0)
            {
                ascentWhenFalling = 0.85f;
                ascentWhenRising = 0.185f;
                maxCanAscendMultiplier = 2.5f;
                maxAscentMultiplier = 4f;
                constantAscend = 0.15f;
            }
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (NewDawnWorld.downedEtheria || modPlayer.Etherable > 0)
            {
                speed = 15f;
                acceleration *= 4f;
            }
        }
    }
}