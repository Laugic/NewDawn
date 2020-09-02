using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class DangersenseEssence : PotionEssence
    {
        public DangersenseEssence()
        {
            Name = "Dangersense";
            EffectDescription = "Highlights traps";
            EssenceColor = new Color(1f, .65f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.dangerSense = true;
        }
    }
}
