using NewDawn.Players;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Accessories
{
    public class GlobalAccessoryItem : GlobalItem
    {
        public override void OnConsumeItem(Item item, Player player)
        {
            AlchemistryPlayer accessoryPlayer = player.GetModPlayer<AlchemistryPlayer>();

            if (accessoryPlayer.ConvertsHealingPotions && AlchemistryPlayer.IsHealingPotion(item))
                accessoryPlayer.RemoveFatalityCharge();
        }
    }
}
