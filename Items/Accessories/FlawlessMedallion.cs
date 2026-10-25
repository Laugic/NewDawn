using System.Collections.Generic;
using NewDawn.Abstracts;
using NewDawn.Items.Alchemistry;
using NewDawn.Players;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Accessories
{
    public class FlawlessMedallion : AbstractStorageItem
    {
        public override Item[] Items { get; set; } = new Item[3];
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            Item.useAnimation = Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            if (Items[0] == null)
                AbstractStorage.GenerateClearStorage(this);
        }


        public override void RightClick(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
                AbstractStorage.TriggerUI(this);

            Item.stack++;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemAnimation == player.itemAnimationMax && Main.mouseItem.type != ModContent.ItemType<GemBelt>())
                AbstractStorage.TriggerUI(this);

            return null;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string storedSetBonus = Main.LocalPlayer.GetModPlayer<FlawlessMedallionPlayer>().StoredSetBonusText;
            if (!string.IsNullOrWhiteSpace(storedSetBonus))
                tooltips.Add(new TooltipLine(Mod, "StoredArmorSetBonus", storedSetBonus));
        }

        public override bool FilterCheck(Item item, int slot)
        {
            //Check that the same item type isn't in the inventory, unless it's in the slot you're trying to click
            switch (slot)
            {
                case 1:
                    return item.bodySlot > 0;
                case 2:
                    return item.legSlot > 0;
                default:
                    return item.headSlot > 0;
            }
        }
    }
}
