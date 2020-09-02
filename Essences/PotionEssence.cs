using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Essences
{
    public class PotionEssence : Essence
    {
        public PotionEssence()
        {
            Name = "Potion";
            Description = "";
            EffectDescription = "";
            EssenceColor = new Color(1f, 1f, 1f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {

        }
    }
}
