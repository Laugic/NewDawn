using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class MagicPowerEssence : PotionEssence
    {
        public MagicPowerEssence()
        {
            Name = "Magic Power";
            EffectDescription = "+10% magic damage";
            EssenceColor = new Color(.6f, 0f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.magicDamage += .1f;
        }
    }
}
