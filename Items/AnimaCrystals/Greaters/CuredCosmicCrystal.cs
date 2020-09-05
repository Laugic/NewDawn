using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDawn.Items.AnimaCrystals.Greaters
{
    public class CuredCosmicCrystal : GreaterCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cured Cosmic Crystal");
            Tooltip.SetDefault("Crystalized space rock infused with Stellar energy\nGreater");
        }
    }
}
