using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace NewDawn.Items.Alchemistry.AnimaCrystals.Primals
{
    public class ImpureCrystal : PrimalCrystal
    {
        public ImpureCrystal() : base(
            new[]
            {
                (GameCulture.English, "Impure Crystal", "Primal"),
                (GameCulture.French, "Cristal Impur", "Primitif")
            },
            WIDTH, HEIGHT)
        {

        }


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impure Crystal");
            Tooltip.SetDefault("Primal");
        }
    }
}
