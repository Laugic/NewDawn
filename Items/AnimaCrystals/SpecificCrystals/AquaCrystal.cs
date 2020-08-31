using NewDawn.Essences;
using NewDawn.Essences.Primals;

namespace NewDawn.Items.AnimaCrystals.SpecificCrystals
{
    public class AquaCrystal : ImpureCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aqua Crystal");
            Tooltip.SetDefault("Primal\nStores Essence");
        }

        public override void SetCrystalDefaults()
        {
            
        }


        public override int EssenceCapacity => 1;

        public override Essence.Levels MaxEssenceLevel => Essence.Levels.PRIMAL;
    }
}
