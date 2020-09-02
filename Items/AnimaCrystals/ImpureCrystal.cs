using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;

namespace NewDawn.Items.AnimaCrystals
{
    public class ImpureCrystal : AnimaCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impure Crystal");
            Tooltip.SetDefault("Primal");
        }


        public override int EssenceCapacity => 1;
        
        public override Essence.Levels MaxEssenceLevel => Essence.Levels.PRIMAL;

        public new List<Essence> Essences = new List<Essence>();
    }
}
