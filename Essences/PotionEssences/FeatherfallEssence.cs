using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class FeatherfallEssence : PotionEssence
    {
        public FeatherfallEssence()
        {
            Name = "Featherfall";
            EffectDescription = "Slows falling speed";
            EssenceColor = new Color(0f, 1f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.slowFall = true;
        }
    }
}
