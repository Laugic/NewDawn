using Microsoft.Xna.Framework;
using NewDawn.Tiles.Temple;
using SubworldLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace NewDawn.Temple.RoomGeneration
{
    public class ExploreRoom : TempleWorld
    {
        private static int _buffer = 40;
        private static int _minHeight = 9;
        private static int _maxHeight = 16;
        readonly ushort tile = (ushort)ModContent.TileType<TempleBrick>();

        public override List<GenPass> Tasks => new()
        {
            new Fill((ushort)ModContent.TileType<TempleBrick>()), new GenRooms((ushort)ModContent.TileType<TempleBrick>())
        };
        public class GenRooms : GenPass
        {
            public enum RoomTypes
            {
                FLAT = 2,
                DOME,
                PIT,
                TOTAL
            }

            ushort tileID;
            Random rand = new();
            int Width, Height;
            public GenRooms(ushort tile) : base("Terrain", 1)
            {
                tileID = tile;
                Width = SubworldSystem.Current.Width;
                Height = SubworldSystem.Current.Height;
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Building Rooms";
                rand = new Random();
                var totalRooms = GetTotalRooms();
                Vector2 curPos = new Vector2(Main.maxTilesX / 2, Main.maxTilesY / 2);
                var roomHeight = rand.Next(_minHeight, _maxHeight);
                for (int i = totalRooms; i > 0; i--)
                {
                    GenRoom(ref curPos, ref roomHeight, i, totalRooms);
                }
            }


            private void GenRoom(ref Vector2 curPos, ref int curHeight, int roomsLeft, int totalRooms)
            {
                int roomType = rand.Next((int)RoomTypes.TOTAL);
                if (roomType <= (int)RoomTypes.FLAT || roomsLeft == 1 || roomsLeft == totalRooms)
                    GenFlatRoom(ref curPos, ref curHeight, roomsLeft, totalRooms);
                else if (roomType <= (int)RoomTypes.DOME)
                    GenDomeRoom(ref curPos, ref curHeight, roomsLeft, totalRooms);
                else if (roomType <= (int)RoomTypes.PIT)
                    GenPitRoom(ref curPos, ref curHeight, roomsLeft, totalRooms);
            }

            private void GenFlatRoom(ref Vector2 curPos, ref int curHeight, int roomsLeft, int totalRooms)
            {
                var roomWidth = GetRoomWidth(ref curPos, roomsLeft, totalRooms);
                if (roomsLeft == totalRooms)
                    curPos.X += roomWidth / 2;
                for (int i = (int)curPos.X - roomWidth; i < (int)curPos.X; i++)
                {
                    for (int j = (int)curPos.Y - curHeight; j < (int)curPos.Y; j++)
                    {
                        WorldGen.KillTile(i, j);
                    }
                }
                GetSpecial(ref curPos, ref curHeight, roomWidth, roomsLeft, totalRooms);
                curPos.X -= roomWidth;
                curPos.Y = rand.Next((int)Math.Max(_buffer, curPos.Y - 6), (int)Math.Min(Main.maxTilesY - _buffer, curPos.Y + 6));
                curHeight = rand.Next(_minHeight, _maxHeight);
            }

            private void GetSpecial(ref Vector2 curPos, ref int curHeight, int roomWidth, int roomsLeft, int totalRooms)
            {
                if (roomsLeft == 1)
                {
                    WorldGen.PlaceTile((int)curPos.X - roomWidth / 2, (int)curPos.Y - 1, ModContent.TileType<TestTriggerTile>(), true, true);
                }
            }

            private void GenDomeRoom(ref Vector2 curPos, ref int curHeight, int roomsLeft, int totalRooms)
            {
                var roomWidth = GetRoomWidth(ref curPos, roomsLeft, totalRooms);
                for (int i = (int)curPos.X - roomWidth; i < (int)curPos.X; i++)
                {
                    for (int j = (int)curPos.Y - curHeight; j < (int)curPos.Y; j++)
                    {
                        WorldGen.KillTile(i, j);
                    }
                }
                for (int i = (int)curPos.X - roomWidth; i < (int)curPos.X; i++)
                {
                    for (int j = (int)curPos.Y - curHeight * 2; j < (int)curPos.Y - curHeight; j++)
                    {
                        if (Distance((int)curPos.X - roomWidth * 3 / 4, (int)curPos.Y - curHeight, i, j) < roomWidth / 4 ||
                            Distance((int)curPos.X - roomWidth / 4, (int)curPos.Y - curHeight, i, j) < roomWidth / 4 ||
                            Distance((int)curPos.X - roomWidth / 2, (int)curPos.Y - curHeight, i, j) < curHeight / 2)
                            WorldGen.KillTile(i, j);
                    }
                }
                curPos.X -= roomWidth;
                curPos.Y = rand.Next((int)Math.Max(_buffer, curPos.Y - 6), (int)Math.Min(Main.maxTilesY - _buffer, curPos.Y + 6));
                curHeight = rand.Next(_minHeight, _maxHeight);
            }

            private void GenPitRoom(ref Vector2 curPos, ref int curHeight, int roomsLeft, int totalRooms)
            {
                var roomWidth = GetRoomWidth(ref curPos, roomsLeft, totalRooms) / 2;
                int roomHeight = (int)Math.Min(curHeight * 4, Main.maxTilesY - _buffer - curPos.Y);
                curPos.Y += roomHeight - curHeight;

                for (int i = (int)curPos.X - roomWidth; i < (int)curPos.X; i++)
                {
                    for (int j = (int)curPos.Y - roomHeight; j < (int)curPos.Y; j++)
                    {
                        WorldGen.KillTile(i, j);
                    }
                }
                curPos.X -= roomWidth;
                curPos.Y = rand.Next((int)Math.Max(_buffer, curPos.Y - 6), (int)Math.Min(Main.maxTilesY - _buffer, curPos.Y + 6));
                curHeight = rand.Next(_minHeight, _maxHeight);
            }


            private int GetTotalRooms()
            {
                return rand.Next(Width / 100, Width / 50);
            }

            private int GetRoomWidth(ref Vector2 curPos, int roomsLeft, int totalRooms)
            {
                return (int)Math.Min((curPos.X - _buffer) / roomsLeft, rand.Next(Width / totalRooms / 4, Width / totalRooms / 2));
            }


            private float Distance(float x1, float y1, float x2, float y2)
            {
                return (float)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            }
        }

    }
}
