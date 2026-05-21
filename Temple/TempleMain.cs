using Microsoft.Xna.Framework;
using NewDawn.Temple.RoomGeneration;
using SubworldLibrary;
using Terraria;

namespace NewDawn.Temple
{
    public class TempleMain
    {
        public static int floor, kills;
        public static void EnterFloor()
        {
            floor = 0;
            kills = 0;
            //ParkourRoom.Initialize();
            /*foreach (Player player in Main.player)
            {
                player.Teleport(new Vector2(-1000, -1000));
            }*/
            //SubworldSystem.Enter<Sanctuary>();
            SubworldSystem.Enter<ParkourRoom>();
        }

        public static void NextFloor()
        {
            floor++;
            kills = 0;
            if (floor % 7 == 0)
                SubworldSystem.Enter<Sanctuary>();
            else
                SubworldSystem.Enter<ParkourRoom>();
        }
    }
}
