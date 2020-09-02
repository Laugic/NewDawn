using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class RageEssence : PotionEssence
    {
        public RageEssence()
        {
            Name = "Rage";
            EffectDescription = "+5% critical strike chance";
            EssenceColor = new Color(1f, .9f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.magicCrit += 5;
            player.thrownCrit += 5;
        }
    }
}
