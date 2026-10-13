using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Items.Loot;
using NewDawn.Items.Useables;

namespace NewDawn.Items.Loot
{
    public class AnDioTreasureBag : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 46;
            Item.maxStack = 20;
            Item.consumable = true;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DioritusCore>(), 1, 2, 3));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AndesiaCore>(), 1, 2, 3));
            itemLoot.Add(ItemDropRule.Common(ItemID.Marble, 1, 10, 30));
            itemLoot.Add(ItemDropRule.Common(ItemID.Granite, 1, 10, 30));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ZaWarudoWatch>()));
            itemLoot.Add(ItemDropRule.Common(ItemID.HealingPotion, 1, 10, 14));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(
                1,
                ItemID.DarkLance,
                ItemID.Flamelash,
                ItemID.FlowerofFire,
                ItemID.Sunfury,
                ItemID.HellwingBow,
                ItemID.ImpStaff));
        }
    }
}