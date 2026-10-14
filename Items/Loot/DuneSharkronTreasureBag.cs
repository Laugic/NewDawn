using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Items.Materials;
using NewDawn.Items.Loot;

namespace NewDawn.Items.Loot
{
    public class DuneSharkronTreasureBag : NewDawnItem
    {
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


        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientShard>(), 1, 2, 3));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Crystilla>(), 1, 8, 14));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pyramind>()));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(
                1,
                ItemID.SandstorminaBottle,
                ItemID.FlyingCarpet,
                ItemID.BandofRegeneration,
                ItemID.MagicMirror,
                ItemID.CloudinaBottle,
                ItemID.HermesBoots,
                ItemID.EnchantedBoomerang));
            itemLoot.Add(ItemDropRule.Common(ItemID.HealingPotion, 1, 10, 14));
        }
    }
}