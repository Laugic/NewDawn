using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using NewDawn.Essences;
using NewDawn.Essences.Primals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.AnimaCrystals.SpecifiedCrystals
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
            EssenceCapacity = 1;
            MaxEssenceLevel = Essence.Levels.PRIMAL;
            Essences.Add(new AquaEssence());
        }
    }
}
