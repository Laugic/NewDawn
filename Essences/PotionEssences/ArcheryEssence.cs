using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class ArcheryEssence : PotionEssence
    {
        public ArcheryEssence()
        {
            Name = "Archery";
            EffectDescription = "+20% arrow damage";
            EssenceColor = new Color(1f, .75f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.arrowDamage += .2f;
        }
    }
}
