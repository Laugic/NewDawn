using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class AnnihilatorTreasureBag : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag");
            // Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
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
        //    itemLoot.Add(ModContent.ItemType<SteamBar>(), 20, 35);
        //    itemLoot.Add(ModContent.ItemType<SoulOfThought>(), 25, 40);
        //    itemLoot.Add(ModContent.ItemType<CogOfKnowledge>(), 1);
        //    itemLoot.Add(499, 10, 15);
        //}
    }
}