using NewDawn.Items.Equipables;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class Soot : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ExtractinatorMode[Item.type] = Item.type;
}

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.createTile = ModContent.TileType<SootTile>();
        }

        public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
        {
            int category = Main.rand.Next(4);
            resultStack = 1;
            switch (category)
            {
                case 0:
                    GetMoney(ref resultType, ref resultStack);
                    break;
                case 1:
                    GetOre(ref resultType, ref resultStack);
                    break;
                case 2:
                    GetGem(ref resultType, ref resultStack);
                    break;
                default:
                    GetMisc(ref resultType, ref resultStack);
                    break;
            }
        }

        private void GetMoney(ref int resultType, ref int resultStack)
        {
            if(Main.rand.Next(2) == 0)
            {
                resultType = ItemID.CopperCoin;
                resultStack = Main.rand.Next(50, 99);
            }
            else if (Main.rand.Next(50) <= 48)
            {
                resultType = ItemID.SilverCoin;
                resultStack = Main.rand.Next(5, 10);
            }
            else if(Main.rand.Next(2) == 0)
            {
                resultType = ItemID.GoldCoin;
                resultStack = 1;
            }
            else
            {
                resultType = ItemID.CopperCoin;
                resultStack = Main.rand.Next(50, 99);
            }
            if (Main.rand.NextBool(8))
                resultStack++;
        }
        private void GetOre(ref int resultType, ref int resultStack)
        {
            int type = Main.rand.Next(7);
            switch(type)
            {
                case 0:
                    resultType = ItemID.SilverOre;
                    break;
                case 1:
                    resultType = ItemID.TungstenOre;
                    break;
                case 2:
                    resultType = ItemID.GoldOre;
                    break;
                case 3:
                    resultType = ItemID.PlatinumOre;
                    break;
                default:
                    resultType = ModContent.ItemType<ObsidiumOre>();
                    break;
            }
            if (Main.rand.NextBool(8))
                resultStack++;
        }
        private void GetGem(ref int resultType, ref int resultStack)
        {
            int type = Main.rand.Next(9);
            switch (type)
            {
                case 0:
                    resultType = ItemID.Amethyst;
                    break;
                case 1:
                    resultType = ItemID.Topaz;
                    break;
                case 2:
                    resultType = ItemID.Sapphire;
                    break;
                case 3:
                    resultType = ItemID.Emerald;
                    break;
                case 4:
                    resultType = ItemID.Ruby;
                    break;
                case 5:
                    resultType = ItemID.Diamond;
                    break;
                default:
                    resultType = ModContent.ItemType<LavaGemItem>();
                    break;
            }
            if (Main.rand.NextBool(8))
                resultStack++;
        }
        private void GetMisc(ref int resultType, ref int resultStack)
        {
            int category = Main.rand.Next(100);
            if(category < 48)
                GetOre(ref resultType, ref resultStack);
            else if(category < 95)
                GetGem(ref resultType, ref resultStack);
            else
            {
                int rand = Main.rand.Next(7);
                switch (rand)
                {
                    case 0:
                        resultType = ItemID.LavaCharm;
                        break;
                    case 1:
                        resultType = ModContent.ItemType<ObsidiumLily>();
                        break;
                    case 2:
                        resultType = ModContent.ItemType<FireDust>();
                        break;
                    case 3:
                        resultType = ModContent.ItemType<Eruption>();
                        break;
                    case 4:
                        resultType = ModContent.ItemType<CrystalizedMagma>();
                        break;
                    case 5:
                        resultType = ModContent.ItemType<Ragnashia>();
                        break;
                    default:
                        resultType = ModContent.ItemType<MagmaHeart>();
                        break;
                }
            }
        }
    }
}