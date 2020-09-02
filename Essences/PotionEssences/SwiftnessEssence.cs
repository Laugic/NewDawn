using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class SwiftnessEssence : PotionEssence
    {
        public SwiftnessEssence()
        {
            Name = "Swiftness";
            EffectDescription = "+10% movement and run speed";
            EssenceColor = new Color(.4f, 1f, .4f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.moveSpeed += .1f;
            player.maxRunSpeed += .1f;
            player.accRunSpeed += .1f;
        }
    }
}
