using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class FishingEssence : PotionEssence
    {
        public FishingEssence()
        {
            Name = "Fishing";
            EffectDescription = "+15 fishing power";
            EssenceColor = new Color(.25f, 1f, .5f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.fishingSkill += 15;
        }
    }
}
