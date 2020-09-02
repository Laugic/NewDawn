using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class MiningEssence : PotionEssence
    {
        public MiningEssence()
        {
            Name = "Mining";
            EffectDescription = "+25% mining speed";
            EssenceColor = new Color(.5f, 1f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.pickSpeed += .25f;
        }
    }
}
