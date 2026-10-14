using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Equipables
{
    [AutoloadEquip(EquipType.Face)]
    public class ObsidiumLily : NewDawnItem
    {

        public override void SetDefaults()
        {
            //item.CloneDefaults(ItemID.NaturesGift);
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = player.GetModPlayer<NewDawnPlayer>();
            modPlayer.Lily = true;
        }
    }
}