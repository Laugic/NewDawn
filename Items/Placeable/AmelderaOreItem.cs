using NewDawn.Items.Materials;
using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Placeable
{
    public class AmelderaOreItem : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ExtractinatorMode[Item.type] = Item.type;
}

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.useStyle = 1;
            Item.consumable = true;
            Item.value = 0;
            Item.rare = ItemRarityID.LightPurple;
            Item.createTile = ModContent.TileType<ObsidiumOreBlock>();
        }



        public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
        {
            int category = Main.rand.Next(3);
            resultStack = 1;
            switch (category)
            {
                case 0:
                    GetPearls(ref resultType, ref resultStack);
                    break;
                default:
                    GetJunk(ref resultType, ref resultStack);
                    break;
            }
        }

        private void GetJunk(ref int resultType, ref int resultStack)
        {
            int type = Main.rand.Next(6);
            switch (type)
            {
                case 0:
                    resultType = ModContent.ItemType<Soot>();
                    resultStack += Main.rand.Next(0, 3);
                    break;
                case 1:
                    resultType = ModContent.ItemType<ElderockItem>();
                    resultStack += Main.rand.Next(0, 2);
                    break;
                case 2:
                    resultType = ModContent.ItemType<ElderlilyItem>();
                    resultStack += Main.rand.Next(0, 2);
                    break;
                case 3:
                    resultType = ModContent.ItemType<ElderootItem>();
                    break;
                default:
                    resultType = ModContent.ItemType<ObsidiumRock>();
                    resultStack += Main.rand.Next(0, 3);
                    break;
            }
        }

        private void GetPearls(ref int resultType, ref int resultStack)
        {
            resultType = ModContent.ItemType<ElderPearl>();
            if (Main.rand.NextBool(6))
                resultStack++;
        }
    }
}