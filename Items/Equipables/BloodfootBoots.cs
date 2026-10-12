using NewDawn.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    public class BloodfootBoots : BootItem
    {
        public override void SetStaticDefaults()
        {
            NewDawnVars.DashBoots.Add(Item.type);
            // DisplayName.SetDefault("Bloodfoot Boots");
            // Tooltip.SetDefault("Allows the wearer to dash");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void SetBootVars()
        {
            DashCooldownMax = 45;
            DashSpeed = 11;

            DustType = ModContent.DustType<Black>();
            TrailLength = 15;
        }
    }
}