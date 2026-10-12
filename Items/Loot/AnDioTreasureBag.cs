using Terraria;
using Terraria.ID;

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
        //TODO: Add loot to this bag
        //public override void ModifyItemLoot(ItemLoot itemLoot)
        //{
        //    itemLoot.Add(ModContent.ItemType<DioritusCore>(), 2, 4);
        //    itemLoot.Add(ModContent.ItemType<AndesiaCore>(), 2, 4);
        //    itemLoot.Add(ItemID.Marble, 10, 31);
        //    itemLoot.Add(ItemID.Granite, 10, 31);
        //    itemLoot.Add(ModContent.ItemType<ZaWarudoWatch>(), 1);
        //    itemLoot.Add(ItemID.HealingPotion, 10, 15);

        //    //TODO: Move this to a boss that is pre-WoF
        //    int anDioItem = 0;
        //    int rand = Main.rand.Next(6);
        //    switch (rand)
        //    {
        //        case 0:
        //            anDioItem = ItemID.DarkLance;
        //            break;
        //        case 1:
        //            anDioItem = ItemID.Flamelash;
        //            break;
        //        case 2:
        //            anDioItem = ItemID.FlowerofFire;
        //            break;
        //        case 3:
        //            anDioItem = ItemID.Sunfury;
        //            break;
        //        case 4:
        //            anDioItem = ItemID.HellwingBow;
        //            break;
        //        default:
        //            anDioItem = ItemID.ImpStaff;
        //            break;
        //    }
        //    itemLoot.Add(anDioItem, 1);
        //}
    }
}