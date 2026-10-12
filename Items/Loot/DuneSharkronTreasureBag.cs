using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class DuneSharkronTreasureBag : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 34;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Orange;
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
        //    itemLoot.Add(ModContent.ItemType<AncientShard>(), 2, 4);
        //    itemLoot.Add(ModContent.ItemType<Crystilla>(), 8, 15);
        //    itemLoot.Add(ModContent.ItemType<Pyramind>(), 1);
        //    int ran = Main.rand.Next(1, 8);
        //    if (ran == 1) itemLoot.Add(ItemID.SandstorminaBottle, 1);
        //    if (ran == 2) itemLoot.Add(ItemID.FlyingCarpet, 1);
        //    if (ran == 3) itemLoot.Add(ItemID.BandofRegeneration, 1);
        //    if (ran == 4) itemLoot.Add(ItemID.MagicMirror, 1);
        //    if (ran == 5) itemLoot.Add(ItemID.CloudinaBottle, 1);
        //    if (ran == 6) itemLoot.Add(ItemID.HermesBoots, 1);
        //    if (ran == 7) itemLoot.Add(ItemID.EnchantedBoomerang, 1);

        //    itemLoot.Add(188, 10, 15);
        //}
    }
}