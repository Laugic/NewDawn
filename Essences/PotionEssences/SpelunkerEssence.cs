using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class SpelunkerEssence : PotionEssence
    {
        public SpelunkerEssence()
        {
            Name = "Spelunker";
            EffectDescription = "Shows the location of treasures and ores";
            EssenceColor = new Color(1f, 1f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.findTreasure = true;
        }
    }
}
