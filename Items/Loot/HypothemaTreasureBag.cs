using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class HypothemaTreasureBag : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Green;
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
        //    itemLoot.Add(ModContent.ItemType<FrostShard>(), Main.rand.Next(2, 4));
        //    itemLoot.Add(ModContent.ItemType<FrostEssence>(), 1);
        //    itemLoot.Add(ModContent.ItemType<ChilledBar>(), Main.rand.Next(22, 36));
        //    int ran = Main.rand.Next(1, 7);
        //    if (ran == 1) itemLoot.Add(ItemID.IceBoomerang, 1);
        //    if (ran == 2) itemLoot.Add(ItemID.IceBlade, 1);
        //    if (ran == 3) itemLoot.Add(ItemID.IceSkates, 1);
        //    if (ran == 4) itemLoot.Add(ItemID.SnowballCannon, 1);
        //    if (ran == 5) itemLoot.Add(987, 1);
        //    if (ran == 6) itemLoot.Add(ItemID.FlurryBoots, 1);


        //    itemLoot.Add(188, Main.rand.Next(10, 15));
        //    itemLoot.Add(ItemID.SnowBlock, Main.rand.Next(40, 75));
        //    itemLoot.Add(ItemID.IceBlock, Main.rand.Next(40, 75));

        //}
    }
}