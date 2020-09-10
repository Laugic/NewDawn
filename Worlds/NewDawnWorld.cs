using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace NewDawn.Worlds
{
    public class NewDawnWorld : ModWorld
    {
        public static int crystalGrowth;
        public static int crystalGrowthRefresh;

        public override void Initialize()
        {
            base.Initialize();
            crystalGrowth = crystalGrowthRefresh = 2;
        }
    }
}
