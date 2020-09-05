using NewDawn.Essences;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDawn.Items.AnimaCrystals.Primals
{
    public class PrimalCrystal : AnimaCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Primal Crystal");
            Tooltip.SetDefault("Primal");
        }

        public override int EssenceCapacity => 1;
        public override Essence.Levels MaxEssenceLevel => Essence.Levels.PRIMAL;

        public new List<Essence> Essences = new List<Essence>();
    }
}
