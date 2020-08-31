using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Essences
{
    public abstract class Essence
    {
        public virtual void Effect(Player player)
        {
        }


        public string Description { get; set; }
        public string EffectDescription { get; set; }
        public Color EssenceColor { get; set; }
        public Levels Level { get; set; }


        public enum Levels
        {
            PRIMAL,
            GREATER,
            SUPER,
            GRAND
        };
    }
}
