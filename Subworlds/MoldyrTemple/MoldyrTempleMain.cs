using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewDawn.Subworlds.MoldyrTemple.Rooms;
using SubworldLibrary;

namespace NewDawn.Subworlds.MoldyrTemple
{
    public class MoldyrTempleMain
    {
        public static void GenerateSubworld()
        {
            Subworld.Enter<MTSubworld>();
        }
    }
}
