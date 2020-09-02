using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class ManaRegenerationEssence : PotionEssence
    {
        public ManaRegenerationEssence()
        {
            Name = "Mana Regeneration";
            EffectDescription = "Increases mana regeneration";
            EssenceColor = new Color(1f, 0f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.manaRegenBonus += 25;
        }
    }
}
