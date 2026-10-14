using NewDawn.Abstracts;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.OLD_Essences
{
    public class ForceEssence : Essence
    {

        public override void GetEffectAtIndex(Player player, AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    player.GetDamage(DamageClass.Generic) += .08f;
                    break;
                case 1:
                    player.maxMinions++;
                    break;
                default:
                    player.GetKnockback(DamageClass.Generic) += 0.33f;
                    break;
            }
        }

        public override string GetTipAtIndex(AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    return "8% increased damage";
                case 1:
                    return "+1 max minion";
                default:
                    return "Increases knockback by 33%";
            }
        }
    }
}
