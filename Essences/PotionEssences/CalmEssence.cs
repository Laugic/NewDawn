using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class CalmEssence : PotionEssence
    {
        public CalmEssence()
        {
            Name = "Calm";
            EffectDescription = "Reduces enemy spawn rate";
            EssenceColor = new Color(0f, .25f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.calmed = true;
        }
    }
}
