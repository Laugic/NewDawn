using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class WrathEssence : PotionEssence
    {
        public WrathEssence()
        {
            Name = "Wrath";
            EffectDescription = "+5% Damage";
            EssenceColor = new Color(1f, .1f, .15f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.allDamage += .05f;
        }
    }
}
