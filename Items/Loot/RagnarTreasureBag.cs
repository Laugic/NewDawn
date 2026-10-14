using NewDawn.Items.Equipables;
using NewDawn.Items.Weapons.Snowball;
using Terraria;
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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkShard>(), 1, 2, 3));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoltenCore>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ObsidiumChunk>(), 1, 3, 4));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(
                1,
                ItemID.LavaCharm,
                ModContent.ItemType<ObsidiumLily>(),
                ModContent.ItemType<FireDust>(),
                ModContent.ItemType<Eruption>(),
                ModContent.ItemType<CrystalizedMagma>(),
                ModContent.ItemType<Ragnashia>(),
                ModContent.ItemType<MagmaHeart>()));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlackIce>(), 4));
            itemLoot.Add(ItemDropRule.Common(ItemID.HealingPotion, 1, 10, 14));
        }

    }
}