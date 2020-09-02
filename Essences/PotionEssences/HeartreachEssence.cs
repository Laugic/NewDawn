using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class HeartreachEssence : PotionEssence
    {
        public HeartreachEssence()
        {
            Name = "Heartreach";
            EffectDescription = "Increases heart pickup range";
            EssenceColor = new Color(1f, .5f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.lifeMagnet = true;
        }
    }
}
