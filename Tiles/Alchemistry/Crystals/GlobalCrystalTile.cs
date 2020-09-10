using NewDawn.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using WebmilioCommons.Tiles;

namespace NewDawn.Tiles.Alchemistry.Crystals
{
    public class GlobalCrystalTile : StandardTile
    {
        public virtual ushort GrowType { get; set; }

        public static void Grow(int i, int j, ushort tileType)
        {
            NewDawnWorld.crystalGrowth--;
            if (NewDawnWorld.crystalGrowth <= 0)
            {
                NewDawnWorld.crystalGrowth = NewDawnWorld.crystalGrowthRefresh;
                Main.tile[i, j].type = tileType;
            }
        }
    }
}
