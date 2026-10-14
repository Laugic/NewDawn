using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace NewDawn.Items.Materials
{
    public class BottledPinkGel : NewDawnItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            //item.UseSound = SoundID.Item9;
            Item.consumable = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(new EntitySource_ItemUse(player, Item, "RightClick"), ItemID.PinkGel, Main.rand.Next(2, 5));
            player.QuickSpawnItem(new EntitySource_ItemUse(player, Item, "RightClick"), ItemID.Bottle, 1);
        }

    }
}