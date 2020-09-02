using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class BuilderEssence : PotionEssence
    {
        public BuilderEssence()
        {
            Name = "Builder";
            EffectDescription = "Increases placement speed and range";
            EssenceColor = new Color(.75f, .6f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            player.tileSpeed += 0.25f;
            player.wallSpeed += 0.25f;
            player.blockRange++;
        }
    }
}
