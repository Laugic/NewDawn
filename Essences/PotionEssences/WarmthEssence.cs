using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class WarmthEssence : PotionEssence
    {
        public WarmthEssence()
        {
            Name = "Warmth";
            EffectDescription = "Reduces damage from cold sources";
            EssenceColor = new Color(1f, .9f, .2f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.resistCold = true;
        }
    }
}
