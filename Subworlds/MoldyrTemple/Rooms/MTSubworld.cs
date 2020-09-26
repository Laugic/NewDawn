using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.World.Generation;

namespace NewDawn.Subworlds.MoldyrTemple.Rooms
{
    public class MTSubworld : Subworld
    {
        public override int width => 400;

        public override int height => 300;

        public override List<GenPass> tasks => new List<GenPass>()
        {
            new SubworldGenPass(progress =>
            {
                progress.Message = "Loading";
			    Main.worldSurface = 0;
			    Main.rockLayer = 1;
			    for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY));
					    Main.tile[i, j].active(true);
                        Main.tile[i, j].type = TileID.Stone;
                    }
                }
            })
        };

        public override void Load()
        {
            Main.dayTime = true;
            Main.time = 27000;
        }
    }
}
