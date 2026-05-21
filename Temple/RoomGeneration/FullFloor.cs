using Microsoft.Xna.Framework;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Items.Temple.Consumables;
using NewDawn.Temple.Rooms;
using NewDawn.Tiles;
using NewDawn.Tiles.Temple;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static tModPorter.ProgressUpdate;

namespace NewDawn.Temple.RoomGeneration
{
    public class FullFloor : TempleWorld
    {
        public enum Dir
        {
            RIGHT,
            UP,
            LEFT,
            DOWN
        }

        public static int RoomWidth = 64, RoomHeight = 36, GridWidth = 17, GridHeight = 13; //GridWidth and GridHeight must be ODD!
        public override int Width => RoomWidth * GridWidth;
        public override int Height => RoomHeight * GridHeight;

        public static TempleRoom[,] Rooms { get; set; }
        public static Random Rand { get; set; }

        public static void Initialize()
        {
            Rand = new Random();
            Rooms = new TempleRoom[GridWidth, GridHeight];
            FillRooms();
        }

        public static void FillRooms()
        {
            GenRoomGrid();

            GenEmpties();
        }

        public static void GenRoomGrid()
        {
            for (int i = 0; i < Rooms.GetLength(0); i++)
            {
                for (int j = 0; j < Rooms.GetLength(1); j++)
                    Rooms[i, j] = new TempleRoom(new Point(i, j));
            }

            var start = Rooms[(GridWidth + 1) / 2 - 1, (GridHeight + 1) / 2 - 1];
            start.Full = true;


            /*Rooms[start.Pos.X + 1, start.Pos.Y].Full = true;
            Rooms[start.Pos.X - 1, start.Pos.Y].Full = true;
            Rooms[start.Pos.X, start.Pos.Y + 1].Full = true;
            Rooms[start.Pos.X, start.Pos.Y - 1].Full = true;
            GenPath(new Point(start.Pos.X + 1, start.Pos.Y), Dir.RIGHT, 10 + Rand.Next(6));
            GenPath(new Point(start.Pos.X - 1, start.Pos.Y), Dir.LEFT, 10 + Rand.Next(6));
            GenPath(new Point(start.Pos.X, start.Pos.Y + 1), Dir.DOWN, 10 + Rand.Next(6));
            GenPath(new Point(start.Pos.X, start.Pos.Y - 1), Dir.UP, 10 + Rand.Next(6));*/
            GenPath(new Point(start.Pos.X, start.Pos.Y), Dir.RIGHT, 10 + Rand.Next(6));
            GenPath(new Point(start.Pos.X, start.Pos.Y), Dir.LEFT, 10 + Rand.Next(6));
            GenPath(new Point(start.Pos.X, start.Pos.Y), Dir.DOWN, 10 + Rand.Next(6));
            GenPath(new Point(start.Pos.X, start.Pos.Y), Dir.UP, 10 + Rand.Next(6));
        }

        public static void GenPath(Point curPoint, Dir direction, int size, int curLength = 1)
        {
            var newPoint = curPoint;
            switch (direction)
            {
                case Dir.RIGHT:
                    newPoint.X += 1;
                    break;
                case Dir.UP:
                    newPoint.Y -= 1;
                    break;
                case Dir.LEFT:
                    newPoint.X -= 1;
                    break;
                case Dir.DOWN:
                    newPoint.Y += 1;
                    break;
            }

            if (newPoint.X <= 0 || newPoint.X >= GridWidth - 1 || newPoint.Y <= 0 || newPoint.Y >= GridHeight - 1)
                return;

            var nextRoom = Rooms[newPoint.X, newPoint.Y];
            nextRoom.Full = true;

            if(nextRoom.Distance == 0)
                nextRoom.Distance = curLength;
            else if (nextRoom.Distance > curLength)
                curLength = nextRoom.Distance;
            curLength++;

            if(size > 1)
            {
                Dir newDir = Dir.RIGHT;
                if (Rand.Next(size) == 0)
                {
                    size /= 2;
                    switch (direction)
                    {
                        case Dir.RIGHT:
                            if (Rand.Next(2) == 0)
                                newDir = Dir.UP;
                            else
                                newDir = Dir.DOWN;
                            break;
                        case Dir.UP:
                            if (Rand.Next(2) == 0)
                                newDir = Dir.RIGHT;
                            else
                                newDir = Dir.LEFT;
                            break;
                        case Dir.LEFT:
                            if (Rand.Next(2) == 0)
                                newDir = Dir.UP;
                            else
                                newDir = Dir.DOWN;
                            break;
                        case Dir.DOWN:
                            if (Rand.Next(2) == 0)
                                newDir = Dir.RIGHT;
                            else
                                newDir = Dir.LEFT;
                            break;
                    }
                    GenPath(newPoint, newDir, size, curLength);
                }
                switch (direction)
                {
                    case Dir.RIGHT:
                        if (Rand.Next(GridWidth - newPoint.X - 1) == 0)
                        {
                            if (Rand.Next(2) == 0)
                                newDir = Dir.UP;
                            else
                                newDir = Dir.DOWN;
                        }
                        else
                            newDir = Dir.RIGHT;
                        break;
                    case Dir.UP:
                        if (Rand.Next(newPoint.Y - 1) == 0)
                        {
                            if (Rand.Next(2) == 0)
                                newDir = Dir.RIGHT;
                            else
                                newDir = Dir.LEFT;
                        }
                        else
                            newDir = Dir.UP;
                        break;
                    case Dir.LEFT:
                        if (Rand.Next(newPoint.X - 1) == 0)
                        {
                            if (Rand.Next(2) == 0)
                                newDir = Dir.UP;
                            else
                                newDir = Dir.DOWN;
                        }
                        else
                            newDir = Dir.LEFT;
                        break;
                    case Dir.DOWN:
                        if (Rand.Next(GridHeight - newPoint.Y - 1) == 0)
                        {
                            if (Rand.Next(2) == 0)
                                newDir = Dir.RIGHT;
                            else
                                newDir = Dir.LEFT;
                        }
                        else
                            newDir = Dir.DOWN;
                        break;
                }
                GenPath(newPoint, newDir, size - 1, curLength);
            }
        }

        public static void GenEmpties()
        {
            Random Rand = new Random();
            for (int i = 1; i < Rooms.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < Rooms.GetLength(1) - 1; j++)
                {
                    var room = Rooms[i, j];
                    if(room == null)
                        continue;
                    if(!room.Full)
                        continue;

                    room.Empties.Add(new Empty(new Point(i * RoomWidth + RoomWidth / 2, j * RoomHeight + RoomHeight / 2), (int)(RoomWidth * (.35 + Rand.NextDouble() / 10.0)), (int)(RoomHeight * (.35 + Rand.NextDouble() / 10.0))));


                    if (Rooms[i, j - 1].Full)
                        room.Empties.Add(new Empty(new Point(i * RoomWidth + (int)(RoomWidth * (.2 + Rand.NextDouble() * .15 + (Rand.Next(2) == 0 ? .5 : 0))), j * RoomHeight), 3 + Rand.Next(3), RoomHeight / 2));

                    if (Rooms[i - 1, j].Full)
                        room.Empties.Add(new Empty(new Point(i * RoomWidth, j * RoomHeight + (int)(RoomHeight * (.3 + Rand.NextDouble() * .4))), RoomWidth / 2, 3 + Rand.Next(4)));
                }
            }
        }

        public override List<GenPass> Tasks => new()
        {
            new Fill((ushort)ModContent.TileType<TempleBrick>()),
            new GenRooms(),
            //new Blur((ushort)ModContent.TileType<TempleBrick>(), 3),
            new GenAccess()
        };

        public class GenRooms : GenPass
        {
            public ushort tileID, columnID, vineID;
            public GenRooms() : base("Terrain", 1)
            {

            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Building Jumps";
                Rand = new Random();

                var start = Rooms[(GridWidth + 1) / 2 - 1, (GridHeight + 1) / 2 - 1];
                Main.spawnTileX = start.Pos.X * RoomWidth + RoomWidth / 2;
                if (start.Empties.Count > 0)
                    Main.spawnTileY = start.Pos.Y * RoomHeight + RoomHeight / 2 + start.Empties[0].Height - 1;
                else
                    Main.spawnTileY = start.Pos.Y * RoomHeight + RoomHeight / 2;

                for (int i = 0; i < Rooms.GetLength(0); i++)
                {
                    for (int j = 0; j < Rooms.GetLength(1); j++)
                    {
                        if (Rooms[i, j].Empties.Count == 0)
                            continue;

                        foreach(Empty empty in Rooms[i, j].Empties)
                            ClearTiles(empty);
                    }
                }
            }

            private void ClearTiles(Empty empty)
            {
                for (int x = empty.Pos.X - empty.Width; x < empty.Pos.X + empty.Width; x++)
                {
                    for (int y = empty.Pos.Y - empty.Height; y < empty.Pos.Y + empty.Height; y++)
                    {
                        if(x < 0 || y < 0 || x >= Main.maxTilesX || y >= Main.maxTilesY)
                            continue;

                        WorldGen.KillTile(x, y, false, false, true);
                    }
                }
            }
        }

        public class GenAccess : GenPass
        {
            public GenAccess() : base("Terrain", 1)
            {

            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                Rand = new Random();

                var Rooms = FullFloor.Rooms;

                foreach(var room in Rooms)
                {
                    if (room.Empties.Count < 2)
                        continue;

                    for (int e = 1; e < room.Empties.Count; e++)
                    {
                        var empty = room.Empties[e];
                        if (empty != null)
                        {
                            if(empty.Height > empty.Width)
                                GenUpwardsAccess(room, empty);
                            else
                                GenHorizontalAccess(room, empty);
                        }
                    }
                }
            }

            private void GenHorizontalAccess(TempleRoom room, Empty empty)
            {
                int roomBottom = room.Empties[0].Pos.Y + room.Empties[0].Height;
                int hallBottom = empty.Pos.Y + empty.Height;
                //if(roomBottom - hallBottom > 6)
                {
                    //Right
                    int x = room.Empties[0].Pos.X - room.Empties[0].Width;
                    for (int i = 0; i < roomBottom - hallBottom; i++)
                    {
                        if (x + i > 0 && x + i < Main.maxTilesX && hallBottom > 0 && roomBottom < Main.maxTilesY)
                        {
                            if (Main.tile[x + i, hallBottom + i].TileType != ModContent.TileType<TempleBrick>())
                            {
                                WorldGen.KillTile(x + i, hallBottom + i);
                                WorldGen.PlaceTile(x + i, hallBottom + i, TileID.Platforms, true, true);
                                Tile tile = Main.tile[x + i, hallBottom + i];
                                tile.BlockType = BlockType.SlopeDownLeft;
                                WorldGen.ReplaceTile(x + i, hallBottom + i, TileID.Platforms, 0);
                            }
                        }
                    }
                    //Left
                    if (room.Pos.X <= 1)
                        return;
                    TempleRoom left = Rooms[room.Pos.X - 1, room.Pos.Y];
                    if(left == null || !left.Full || left.Empties.Count == 0)
                        return;

                    int leftBottom = left.Empties[0].Pos.Y + room.Empties[0].Height;
                    x = left.Empties[0].Pos.X + room.Empties[0].Width;
                    for (int i = 0; i < leftBottom - hallBottom; i++)
                    {
                        if (x - i > 0 && x - i < Main.maxTilesX && hallBottom > 0 && leftBottom < Main.maxTilesY)
                        {
                            if (Main.tile[x - i, hallBottom + i].TileType != ModContent.TileType<TempleBrick>())
                            {
                                WorldGen.KillTile(x - i, hallBottom + i);
                                WorldGen.PlaceTile(x - i, hallBottom + i, TileID.Platforms, true, true);
                                Tile tile = Main.tile[x - i, hallBottom + i];
                                tile.BlockType = BlockType.SlopeDownRight;
                                WorldGen.ReplaceTile(x - i, hallBottom + i, TileID.Platforms, 0);
                            }
                        }
                    }
                }
            }

            private void GenUpwardsAccess(TempleRoom room, Empty empty)
            {
                bool left = Rand.Next(2) == 0;
                Empty baseEmpty = room.Empties[0];
                if (baseEmpty == null)
                    return;
                if (room.Pos.Y <= 1)
                    return;
                TempleRoom upRoom = Rooms[room.Pos.X, room.Pos.Y - 1];
                Empty upEmpty = upRoom.Empties[0];
                if (upEmpty == null)
                    return;

                for (int y = upEmpty.Pos.Y + upEmpty.Height - 4; y < baseEmpty.Pos.Y + baseEmpty.Height; y++)
                {
                    if (y > 0 && y < Main.maxTilesY && empty.Pos.X - empty.Width > 0 && empty.Pos.X + empty.Width < Main.maxTilesX)
                        WorldGen.PlaceTile(left ? empty.Pos.X - empty.Width + 1 : empty.Pos.X + empty.Width - 1, y, TileID.Rope);
                }

                int platY = upEmpty.Pos.Y + upEmpty.Height;
                for(int x = empty.Pos.X - empty.Width * 2; x < empty.Pos.X + empty.Width * 2; x++)
                {
                    if (x > 0 && x < Main.maxTilesX && platY > 0 && platY < Main.maxTilesY)
                    {
                        if (Main.tile[x, platY].TileType != ModContent.TileType<TempleBrick>())
                            WorldGen.PlaceTile(x, platY, TileID.Platforms, true, true);
                    }
                }
            }
        }
    }
}