using NewDawn.Items.Materials;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Loot
{
    public class HypothemaTreasureBag : NewDawnItem
    {
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


        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostShard>(), 1, 3, 4));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FrostEssence>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ChilledBar>(), 1, 12, 24));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(
                1,
                ItemID.IceBoomerang,
                ItemID.IceBlade,
                ItemID.IceSkates,
                ItemID.SnowballCannon,
                ItemID.BlizzardinaBottle,
                ItemID.FlurryBoots));
            itemLoot.Add(ItemDropRule.Common(ItemID.HealingPotion, 1, 10, 14));
            itemLoot.Add(ItemDropRule.Common(ItemID.SnowBlock, 1, 40, 74));
            itemLoot.Add(ItemDropRule.Common(ItemID.IceBlock, 1, 40, 74));
        }
    }
}