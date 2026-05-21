using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDawn.Temple.RoomGeneration
{
    public class Empty
    {
        public Empty(Point pos, int width, int height)
        {
            Pos = pos;
            Width = width;
            Height = height;
        }

        public Point Pos { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
