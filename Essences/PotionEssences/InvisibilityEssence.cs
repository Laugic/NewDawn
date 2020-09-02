using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class InvisibilityEssence : PotionEssence
    {
        public InvisibilityEssence()
        {
            Name = "Invisibility";
            EffectDescription = "'You can't see me.'";
            EssenceColor = new Color(0f, .85f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.invis = true;
        }
    }
}
