using Microsoft.Xna.Framework;
using NewDawn.Temple.RoomGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDawn.Temple.Rooms
{
    public class TempleRoom
    {
        public TempleRoom(Point point)
        {
            Pos = point;
        }

        public Point Pos { get; set; }
        public List<Empty> Empties = new List<Empty>();
        public bool Full { get; set; } = false;

        public int Distance { get; set; } = 0;
        /*public virtual int[,] Room { get; set; }
        public bool Right { get; set; } = true;
        public bool Left { get; set; } = true;
        public bool Up { get; set; } = false;
        public bool Down { get; set; } = false;*/
    }
}
