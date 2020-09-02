using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class WaterWalkingEssence : PotionEssence
    {
        public WaterWalkingEssence()
        {
            Name = "Water Walking";
            EffectDescription = "Allows the ability to walk on water";
            EssenceColor = new Color(0f, .1f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.waterWalk = true;
        }
    }
}
