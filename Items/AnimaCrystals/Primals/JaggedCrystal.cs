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
    public class JaggedCrystal : PrimalCrystal
    {
        public JaggedCrystal() : base(new [] 
            {
                (GameCulture.English, "Jagged Crystal", "Primal"),
                (GameCulture.French, "Cristal Déchiqueté", "Primitif")
            },
            WIDTH, HEIGHT)
        {
        }
    }
}
