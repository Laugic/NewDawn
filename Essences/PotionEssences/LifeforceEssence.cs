using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class LifeforceEssence : PotionEssence
    {
        public LifeforceEssence()
        {
            Name = "Lifeforce";
            EffectDescription = "+20% Max Life";
            EssenceColor = new Color(1f, 0f, .2f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.statLifeMax2 += (player.statLifeMax + player.statLifeMax2) / 5 / 20 * 20 - (player.statLifeMax / 5 / 20 * 20);
        }
    }
}
