using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Loot
{
    public class RagnarTreasureBag : NewDawnItem
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


        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            //TODO: Add item loot for Ragnar Treasure Bag
            //itemLoot.Add(ModContent.ItemType<DarkShard>(), Main.rand.Next(2, 4));
            //itemLoot.Add(ModContent.ItemType<MoltenCore>(), 1);
            //itemLoot.Add(ModContent.ItemType<ObsidiumChunk>(), Main.rand.Next(3, 5));

            //int obsidiumItem = 0;
            //int rand = Main.rand.Next(7);
            //switch (rand)
            //{
            //    case 0:
            //        obsidiumItem = ItemID.LavaCharm;
            //        break;
            //    case 1:
            //        obsidiumItem = ModContent.ItemType<ObsidiumLily>();
            //        break;
            //    case 2:
            //        obsidiumItem = ModContent.ItemType<FireDust>();
            //        break;
            //    case 3:
            //        obsidiumItem = ModContent.ItemType<Eruption>();
            //        break;
            //    case 4:
            //        obsidiumItem = ModContent.ItemType<CrystalizedMagma>();
            //        break;
            //    case 5:
            //        obsidiumItem = ModContent.ItemType<Ragnashia>();
            //        break;
            //    default:
            //        obsidiumItem = ModContent.ItemType<MagmaHeart>();
            //        break;
            //}
            //itemLoot.Add(obsidiumItem, 1);
            //if (Main.rand.Next(4) == 0)
            //    itemLoot.Add(ModContent.ItemType<BlackIce>(), 1);

            //itemLoot.Add(188, Main.rand.Next(10, 15));
        }

    }
}