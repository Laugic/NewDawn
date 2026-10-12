using NewDawn.Items.Materials;
using NewDawn.Items.Placeable;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Loot
{
    public class ObsidiumChunk : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Pulsing with heat\nRight Click to open");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item9;
            Item.consumable = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }


        public override void RightClick(Player player)
        {
            var source = new EntitySource_Parent(player);
            if (Main.hardMode)
            {
                if (Main.rand.Next(8) != 0)
                {
                    int rand = Main.rand.Next(10);
                    switch (rand)
                    {
                        case 1:
                            player.QuickSpawnItem(source, ItemID.CobaltOre, Main.rand.Next(4, 12));
                            break;
                        case 2:
                            player.QuickSpawnItem(source, ItemID.CobaltBar, Main.rand.Next(2, 7));
                            break;
                        case 3:
                            player.QuickSpawnItem(source, ItemID.MythrilOre, Main.rand.Next(4, 12));
                            break;
                        case 4:
                            player.QuickSpawnItem(source, ItemID.MythrilBar, Main.rand.Next(2, 7));
                            break;
                        case 5:
                            player.QuickSpawnItem(source, ItemID.PalladiumOre, Main.rand.Next(4, 12));
                            break;
                        case 6:
                            player.QuickSpawnItem(source, ItemID.PalladiumBar, Main.rand.Next(2, 7));
                            break;
                        case 7:
                            player.QuickSpawnItem(source, ItemID.OrichalcumOre, Main.rand.Next(4, 12));
                            break;
                        case 8:
                            player.QuickSpawnItem(source, ItemID.OrichalcumBar, Main.rand.Next(2, 7));
                            break;
                        default:
                            player.QuickSpawnItem(source, ModContent.ItemType<ObsidiumBar>(), Main.rand.Next(8, 15));
                            break;
                    }
                    if (Main.rand.Next(1, 4) == 0) player.QuickSpawnItem(source, 173, Main.rand.Next(4, 12));
                }
                else
                {
                    if (Main.rand.Next(1, 4) == 1) player.QuickSpawnItem(source, 175, Main.rand.Next(6, 13));
                    else player.QuickSpawnItem(source, ModContent.ItemType<ObsidiumBar>(), Main.rand.Next(6, 13));
                }

                int ran = Main.rand.Next(1, 8);
                if (ran == 1) player.QuickSpawnItem(source, 182, Main.rand.Next(2, 4));
                if (ran == 2) player.QuickSpawnItem(source, 178, Main.rand.Next(2, 4));
                if (ran == 3) player.QuickSpawnItem(source, 179, Main.rand.Next(2, 4));
                if (ran == 4) player.QuickSpawnItem(source, 177, Main.rand.Next(2, 4));
                if (ran == 5) player.QuickSpawnItem(source, 180, Main.rand.Next(2, 4));
                if (ran == 6) player.QuickSpawnItem(source, 181, Main.rand.Next(2, 4));
                if (ran == 7) player.QuickSpawnItem(source, ModContent.ItemType<LavaGemItem>(), Main.rand.Next(2, 4));

            }
            else
            {
                if (Main.rand.Next(1, 9) != 1)
                {
                    if (Main.rand.Next(1, 4) == 1) player.QuickSpawnItem(source, 173, Main.rand.Next(2, 6));
                    if (Main.rand.Next(1, 4) == 2) player.QuickSpawnItem(source, 174, Main.rand.Next(3, 5));
                    if (Main.rand.Next(1, 4) == 3) player.QuickSpawnItem(source, ModContent.ItemType<ObsidiumOre>(), Main.rand.Next(2, 6));
                }
                else
                {
                    if (Main.rand.Next(1, 4) == 1) player.QuickSpawnItem(source, 175, Main.rand.Next(1, 4));
                    else player.QuickSpawnItem(source, ModContent.ItemType<ObsidiumBar>(), Main.rand.Next(1, 4));
                }

                int ran = Main.rand.Next(1, 8);
                if (ran == 1) player.QuickSpawnItem(source, 182, Main.rand.Next(1, 3));
                if (ran == 2) player.QuickSpawnItem(source, 178, Main.rand.Next(1, 3));
                if (ran == 3) player.QuickSpawnItem(source, 179, Main.rand.Next(1, 3));
                if (ran == 4) player.QuickSpawnItem(source, 177, Main.rand.Next(1, 3));
                if (ran == 5) player.QuickSpawnItem(source, 180, Main.rand.Next(1, 3));
                if (ran == 6) player.QuickSpawnItem(source, 181, Main.rand.Next(1, 3));
                if (ran == 7) player.QuickSpawnItem(source, ModContent.ItemType<LavaGemItem>(), Main.rand.Next(1, 3));

                if (Main.rand.Next(1, 4) == 1) player.QuickSpawnItem(source, 2701, Main.rand.Next(6, 13));
            }
        }

    }
}