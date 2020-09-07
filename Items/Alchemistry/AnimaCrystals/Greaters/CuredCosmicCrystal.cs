using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;

namespace NewDawn.Items.Alchemistry.AnimaCrystals.Greaters
{
    public class CuredCosmicCrystal : GreaterCrystal
    {
        public CuredCosmicCrystal() : base(new []
            {
                (GameCulture.English, "Cured Cosmic Crystal", "Crystalized space rock infused with Stellar energy\nGreater"),
                (GameCulture.French, "Cristal Cosmic Traité", "Roche spatiale cristallisée infuse d'énergie stellaire\nSupérieur")
            },
            WIDTH, HEIGHT)
        {

        }
    }
}
