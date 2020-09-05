using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;

namespace NewDawn.Items.AnimaCrystals.Primals
{
    public class FracturedCrystal : PrimalCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fractured Crystal");
            Tooltip.SetDefault("Primal");
        }
    }
}
