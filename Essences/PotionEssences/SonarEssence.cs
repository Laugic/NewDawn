using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class SonarEssence : PotionEssence
    {
        public SonarEssence()
        {
            Name = "Sonar";
            EffectDescription = "Allows detection of hooked fish";
            EssenceColor = new Color(.1f, 1f, .4f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.sonarPotion = true;
        }
    }
}
