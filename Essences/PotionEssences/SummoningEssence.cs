using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class SummoningEssence : PotionEssence
    {
        public SummoningEssence()
        {
            Name = "Summoning";
            EffectDescription = "Increases minion capacity";
            EssenceColor = new Color(.6f, 1f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.maxMinions++;
        }
    }
}
