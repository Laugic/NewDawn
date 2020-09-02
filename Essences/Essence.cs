using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebmilioCommons.Managers;

namespace NewDawn.Essences
{
    public abstract class Essence : IHasUnlocalizedName, IComparable<Essence>
    {
        public virtual void Effect(Player player)
        {
        }

        public int CompareTo(Essence other)
        {
            return Name.CompareTo(other.Name);
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string EffectDescription { get; set; }
        public Color EssenceColor { get; set; }
        public Levels Level { get; set; }

        public string UnlocalizedName => GetType().Name;

        public enum Levels
        {
            PRIMAL,
            GREATER,
            SUPER,
            GRAND
        };
    }
}
