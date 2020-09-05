using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace NewDawn.Items.AnimaCrystals.Primals
{
    public class FracturedCrystal : PrimalCrystal
    {
        public FracturedCrystal() : base(
            new[]
            {
                (GameCulture.English, "Fractured Crystal", "Primal"),
                (GameCulture.French, "Cristal Fracturé", "Primitif")
            },
            WIDTH, HEIGHT)
        {

        }
    }
}
