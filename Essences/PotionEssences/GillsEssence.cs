using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class GillsEssence : PotionEssence
    {
        public GillsEssence()
        {
            Name = "Gills";
            EffectDescription = "Allows breathing underwater";
            EssenceColor = new Color(.4f, .4f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.gills = true;
        }
    }
}
