using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.AnimaCrystals
{
    public class ImpureCrystal : AnimaCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impure Crystal");
            Tooltip.SetDefault("Primal\nStores Essence");
        }

        public override void SetCrystalDefaults()
        {
            EssenceCapacity = 1;
            MaxEssenceLevel = Essence.Levels.PRIMAL;
        }
    }
}
