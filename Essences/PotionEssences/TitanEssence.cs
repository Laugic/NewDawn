using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class TitanEssence : PotionEssence
    {
        public TitanEssence()
        {
            Name = "Titan";
            EffectDescription = "Increases knockback";
            EssenceColor = new Color(.4f, 1f, .2f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.kbBuff = true;
        }
    }
}
