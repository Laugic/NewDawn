using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class BattleEssence : PotionEssence
    {
        public BattleEssence()
        {
            Name = "Battle";
            EffectDescription = "Increases enemy spawn rate";
            EssenceColor = new Color(.75f, 0f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.enemySpawns = true;
        }
    }
}
