using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Essences.PotionEssences
{
    public class ShineEssence : PotionEssence
    {
        public ShineEssence()
        {
            Name = "Shine";
            EffectDescription = "Emit light";
            EssenceColor = new Color(1f, 1f, 0f);
            Level = Levels.PRIMAL;
        }


        public override void Effect(Player player)
        {
            Lighting.AddLight((int)(player.position.X + (float)(player.width / 2)) / 16, (int)(player.position.Y + (float)(player.height / 2)) / 16, 0.8f, 0.95f, 1f);
        }
    }
}
