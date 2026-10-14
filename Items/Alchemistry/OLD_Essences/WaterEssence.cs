using NewDawn.Abstracts;
using Terraria;

namespace NewDawn.Items.Alchemistry.OLD_Essences
{
    public class WaterEssence : Essence
    {

        public override void GetEffectAtIndex(Player player, AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    player.fishingSkill += 15;
                    player.sonarPotion = true;
                    break;
                case 1:
                    player.aggro -= 5;
                    player.calmed = true;
                    break;
                default:
                    player.ignoreWater = true;
                    player.accFlipper = true;
                    player.gills = true;
                    break;
            }
        }

        public override string GetTipAtIndex(AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    return "15% increased fishing power and detects hooked fish";
                case 1:
                    return "Enemies spawn less and target you less";
                default:
                    return "Allows you to swim and breathe underwater";
            }
        }
    }
}
