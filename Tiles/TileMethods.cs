using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace NewDawn.Tiles
{
    public class TileMethods 
    {
        public static Vector2 ReturnSafePos(int i, int j)
        {
            var k = Math.Max(0, i);
            k = Math.Min(k, Main.maxTilesX);
            var l = Math.Max(0, j);
            l = Math.Min(l, Main.maxTilesY);

            return new Vector2(k, l);
        }
    }
}
