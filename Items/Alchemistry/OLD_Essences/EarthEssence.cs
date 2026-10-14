using NewDawn.Abstracts;
using Terraria;

namespace NewDawn.Items.Alchemistry.OLD_Essences
{
    public class EarthEssence : Essence
    {

        public override void GetEffectAtIndex(Player player, AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    player.statDefense += 6;
                    break;
                case 1:
                    player.noKnockback = true;
                    break;
                default:
                    player.pickSpeed -= 0.25f;
                    player.tileSpeed += .25f;
                    player.wallSpeed += .25f;
                    break;
            }
        }

        public override string GetTipAtIndex(AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    return "6 defense";
                case 1:
                    return "Grants immunity to knockback";
                default:
                    return "+25% mining and building speed";
            }
        }
    }
}
