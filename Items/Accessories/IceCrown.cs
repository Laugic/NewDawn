using NewDawn.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Accessories
{
    public class IceCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ice Crown");
            // Tooltip.SetDefault("7% increased melee and ranged critical strike chance\n7% increased snowball damage\n'The authority of the snow'");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 9);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 7;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Melee) += 7;
            player.GetCritChance(DamageClass.Ranged) += 7;
            player.GetDamage(DamageClass.Melee) += 0.07f;
            player.GetDamage(DamageClass.Ranged) += 0.07f;
            player.GetModPlayer<AccessoryPlayer>().SnowDamage += .07f;
        }
    }
}