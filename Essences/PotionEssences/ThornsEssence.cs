using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class ThornsEssence : PotionEssence
    {
        public ThornsEssence()
        {
            Name = "Thorns";
            EffectDescription = "Reflect 33% of damage taken";
            EssenceColor = new Color(.7f, 1f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.thorns += .33f;
        }
    }
}
