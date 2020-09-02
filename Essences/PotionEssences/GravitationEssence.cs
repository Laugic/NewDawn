using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class GravitationEssence : PotionEssence
    {
        public GravitationEssence()
        {
            Name = "Gravitation";
            EffectDescription = "Allows control of gravity";
            EssenceColor = new Color(.75f, 0f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.gravControl = true;
        }
    }
}
