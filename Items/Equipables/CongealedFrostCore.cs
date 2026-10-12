using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class CongealedFrostCore : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Congealed Frost Core");
            // Tooltip.SetDefault("+10% Melee and Ranged damage\n+50% Snowball damage\n'Sometimes, an Ice Golem's Heart is extra cold'");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += .1f;
            player.GetDamage(DamageClass.Ranged) += .1f;
            NewDawnPlayer.Get(player).SnowDamage += .5f;
        }
    }
}