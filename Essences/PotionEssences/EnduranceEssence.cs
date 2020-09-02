using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class EnduranceEssence : PotionEssence
    {
        public EnduranceEssence()
        {
            Name = "Endurance";
            EffectDescription = "10% damage reduction";
            EssenceColor = new Color(.9f, .9f, .9f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.endurance = 1 - (1 - player.endurance) * .9f;
        }
    }
}
