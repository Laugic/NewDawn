using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using NewDawn.Subworlds.MoldyrTemple;

namespace NewDawn.Tiles.Subworlds.MoldyrTemple
{
    public class TestTriggerTile : ModTile
    {
        public override bool NewRightClick(int i, int j)
        {
            MoldyrTempleMain.GenerateSubworld();
            return true;
        }
    }
}
