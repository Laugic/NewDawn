using System;
using System.Collections.Generic;
using NewDawn.Essences;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDawn.Items.AnimaCrystals.Greaters
{
    public class GreaterCrystal : AnimaCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rough Crystal");
            Tooltip.SetDefault("Greater");
        }


        public override int EssenceCapacity => 2;

        public override Essence.Levels MaxEssenceLevel => Essence.Levels.GREATER;

        public new List<Essence> Essences = new List<Essence>();
    }
}
