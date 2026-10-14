using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using NewDawn.Items.Loot;

namespace NewDawn.Items.Loot
{
    public class SlybertronTreasureBag : NewDawnItem
    {
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

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SteamBar>(), 1, 20, 34));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SoulOfFraught>(), 1, 25, 39));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pipeworks>()));
            itemLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 10, 14));
        }
    }
}