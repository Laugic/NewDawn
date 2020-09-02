using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class InfernoEssence : PotionEssence
    {
        public InfernoEssence()
        {
            Name = "Inferno";
            EffectDescription = "Ignites nearby enemies";
            EssenceColor = new Color(1f, 1f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.AddBuff(116, 2);
        }
    }
}
