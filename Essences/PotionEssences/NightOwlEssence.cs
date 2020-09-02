using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class NightOwlEssence : PotionEssence
    {
        public NightOwlEssence()
        {
            Name = "Night Owl";
            EffectDescription = "Increases night vision";
            EssenceColor = new Color(.5f, 1f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.nightVision = true;
        }
    }
}
