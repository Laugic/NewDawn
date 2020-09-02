using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class CrateEssence : PotionEssence
    {
        public CrateEssence()
        {
            Name = "Crate";
            EffectDescription = "Greater chance of fishing up a crate";
            EssenceColor = new Color(1f, .9f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.cratePotion = true;
        }
    }
}
