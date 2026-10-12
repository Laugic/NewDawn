using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class SteamTrainTreasureBag : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.maxStack = 20;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }


        //TODO: Add loot to this bag
        //public override void ModifyItemLoot(ItemLoot itemLoot)
        //{
        //    itemLoot.Add(ModContent.ItemType<SteamBar>(), Main.rand.Next(20, 35));
        //    itemLoot.Add(ModContent.ItemType<SoulOfWrought>(), Main.rand.Next(25, 40));
        //    itemLoot.Add(ModContent.ItemType<SteamTank>());

        //    itemLoot.Add(ItemID.GreaterHealingPotion, Main.rand.Next(10, 15));
        //}
    }
}