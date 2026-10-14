using NewDawn.Abstracts;
using Terraria;

namespace NewDawn.Items.Alchemistry.OLD_Essences
{
    public class ChaosEssence : Essence
    {

        public override void GetEffectAtIndex(Player player, AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    player.statManaMax2 += 40;
                    break;
                case 1:
                    player.enemySpawns = true;
                    player.aggro += 5;
                    break;
                default:
                    player.ammoCost80 = true;
                    break;
            }
        }

        public override string GetTipAtIndex(AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    return "+40 max mana";
                case 1:
                    return "Enemies are more likely to spawn and target you";
                default:
                    return "20% chance not to consume ammo";
            }
        }
    }
}
