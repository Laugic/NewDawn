using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class HunterEssence : PotionEssence
    {
        public HunterEssence()
        {
            Name = "Hunter";
            EffectDescription = "Shows the location of enemies";
            EssenceColor = new Color(1f, .85f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.detectCreature = true;
        }
    }
}
