using Terraria;
using Terraria.ID;

namespace NewDawn.Items.Loot
{
    public class SteamTank : NewDawnItem
    {

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 48;
            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            global::NewDawn.Players.MysticPlayer mysticPlayer = player.GetModPlayer<global::NewDawn.Players.MysticPlayer>();
            player.jumpSpeedBoost += 2f;
            player.moveSpeed += 0.5f;
            mysticPlayer.GlobalOverflow += .15f;
            mysticPlayer.OverflowDamage += .15f;
        }
    }
}