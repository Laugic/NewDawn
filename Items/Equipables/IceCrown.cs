using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class IceCrown : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Crown");
            // Tooltip.SetDefault("+20% Melee and Ranged crit\n+50% Snowball damage\n'The authority of the snow'");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 9);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Generic) += 20;
            player.GetCritChance(DamageClass.Ranged) += 20;
            NewDawnPlayer.Get(player).SnowDamage += .5f;
        }
    }
}