using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class FlipperEssence : PotionEssence
    {
        public FlipperEssence()
        {
            Name = "Flipper";
            EffectDescription = "Lets you move swiftly in liquids";
            EssenceColor = new Color(0f, .5f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.ignoreWater = true;
            player.accFlipper = true;
        }
    }
}
