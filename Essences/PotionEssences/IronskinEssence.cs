using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class IronskinEssence : PotionEssence
    {
        public IronskinEssence()
        {
            Name = "Ironskin";
            EffectDescription = "+4 Defense";
            EssenceColor = new Color(1f, 1f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.statDefense += 4;
        }
    }
}
