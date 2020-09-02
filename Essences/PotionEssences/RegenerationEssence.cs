using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class RegenerationEssence : PotionEssence
    {
        public RegenerationEssence()
        {
            Name = "Regeneration";
            EffectDescription = "Increases life regeneration";
            EssenceColor = new Color(1f, .1f, .25f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.lifeRegen += 2;
        }
    }
}
