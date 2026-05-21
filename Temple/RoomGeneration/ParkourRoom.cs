using Microsoft.Xna.Framework;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Items.Temple.Consumables;
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
using static NewDawn.Temple.RoomGeneration.ParkourRoom.GenParkour;
using static NewDawn.Temple.RoomGeneration.TempleWorld.GenDecoBlobs;
using static tModPorter.ProgressUpdate;

namespace NewDawn.Temple.RoomGeneration
{
    public class ParkourRoom : TempleWorld
    {
        private static int buffer = 40;
        private static int minHeight = 9;
        private static int maxHeight = 16;
        private static int numKeystones = 0;
        readonly ushort tile = (ushort)ModContent.TileType<TempleBrick>();

        public override int Width => 320;
        public override int Height => 320;

        public override List<GenPass> Tasks => new()
        {
            new Fill(placeholder),
            new GenParkour((ushort)ModContent.TileType<TempleBrick>()),
            //new Blur(placeholder, 3),
            //new Blur(placeholder, 2),
            new GenDecoBlobs(-1, WallID.LivingLeaf, 120, 6, 12, false),
            new GenDecoBlobs(-1, ModContent.WallType<TempleBrickWall>(), 120, 16, 24, false),
            new GenDecoBlobs(TileID.LeafBlock, WallID.LivingLeaf, 120, 1, 2),
            new GenDecoBlobs(TileID.LivingMahoganyLeaves, WallID.LivingLeaf, 120, 6, 12),
            //new GenDecoBlobs(TileID.LivingWood, WallID.LivingWood, 120, 6, 12),


            new Replace((ushort)ModContent.TileType<TempleBrick>(), placeholder),
            new GenColumns(TileID.MarbleColumn, TileID.VineRope),
        };
        public class GenParkour : GenPass
        {
            public enum JumpTypes
            {
                RIGHT,
                UPRIGHT,
                UPLEFT,
                LEFT,
                ROPE
            }

            public ushort tileID, columnID, vineID;
            Random rand = new();
            int Width, Height, numKeystones, keyCounter;
            public GenParkour(ushort tile) : base("Terrain", 1)
            {
                tileID = tile;
                Width = SubworldSystem.Current.Width;
                Height = SubworldSystem.Current.Height;
                numKeystones = 0;
                keyCounter = 0;
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Building Jumps";
                rand = new Random();
                var totalRooms = 30;
                Vector2 curPos = new Vector2((int)(Main.maxTilesX * .4), Main.maxTilesY - 2 * buffer);
                Main.spawnTileX = (int)curPos.X;
                Main.spawnTileY = (int)curPos.Y;
                var roomHeight = rand.Next(minHeight, maxHeight);
                var roomWidth = rand.Next(10, 15);
                var prevJumps = new List<JumpTypes>
                {
                    JumpTypes.RIGHT
                };
                int difficulty = 6;

                GenFlatRoom(ref curPos, roomHeight, roomWidth);
                ClearSpace(ref curPos, prevJumps, difficulty);
                GenJumpPlatform(ref curPos, prevJumps);

                for (int i = 0; i < totalRooms; i++)
                    GenNextJump(ref curPos, ref prevJumps, ref difficulty);

                GenFlatRoom(ref curPos, roomHeight, roomWidth, true);
            }

            private void GenNextJump(ref Vector2 curPos, ref List<JumpTypes> prevJumps, ref int difficulty)
            {
                var prevJump = prevJumps[prevJumps.Count - 1];
                switch (prevJump)
                {
                    case JumpTypes.RIGHT:
                        if (curPos.X >= Main.maxTilesX - 2 * buffer)
                            prevJumps.Add(JumpTypes.UPRIGHT);
                        else if (prevJumps.Count > 1 && prevJumps[prevJumps.Count - 2] != JumpTypes.RIGHT && rand.Next(2) == 0)
                            prevJumps.Add(JumpTypes.RIGHT);
                        else
                            prevJumps.Add(JumpTypes.UPRIGHT);
                        break;
                    case JumpTypes.UPRIGHT:
                        if (curPos.X >= Main.maxTilesX - 2 * buffer)
                            prevJumps.Add(JumpTypes.UPLEFT);
                        else if (prevJumps.Count > 1 && prevJumps[prevJumps.Count - 2] != JumpTypes.UPRIGHT && rand.Next(3) == 0)
                            prevJumps.Add(JumpTypes.UPRIGHT);
                        else if (rand.Next(2) == 0)
                            prevJumps.Add(JumpTypes.RIGHT);
                        else
                            prevJumps.Add(JumpTypes.UPLEFT);
                        break;
                    case JumpTypes.UPLEFT:
                        if (curPos.X <= 2 * buffer)
                            prevJumps.Add(JumpTypes.UPRIGHT);
                        else if (prevJumps.Count > 1 && prevJumps[prevJumps.Count - 2] != JumpTypes.UPLEFT && rand.Next(3) == 0)
                            prevJumps.Add(JumpTypes.UPLEFT);
                        else if (rand.Next(2) == 0)
                            prevJumps.Add(JumpTypes.UPRIGHT);
                        else
                            prevJumps.Add(JumpTypes.LEFT);
                        break;
                    case JumpTypes.LEFT:
                        if (curPos.X <= 2 * buffer)
                            prevJumps.Add(JumpTypes.UPLEFT);
                        else if (prevJumps.Count > 1 && prevJumps[prevJumps.Count - 2] != JumpTypes.LEFT && rand.Next(2) == 0)
                            prevJumps.Add(JumpTypes.LEFT);
                        else
                            prevJumps.Add(JumpTypes.UPLEFT);
                        break;
                }
                difficulty = rand.Next(6);
                keyCounter++;
                ClearSpace(ref curPos, prevJumps, difficulty);
                var thisJump = prevJumps[prevJumps.Count - 1];
                bool spawnKey = false;
                if (keyCounter >= 8 && (thisJump == JumpTypes.UPRIGHT/* && prevJump == JumpTypes.UPLEFT) || (prevJump == JumpTypes.UPRIGHT &&*/|| thisJump == JumpTypes.UPLEFT))
                {
                    keyCounter = 0;
                    spawnKey = true;
                }
                GenJumpPlatform(ref curPos, prevJumps, spawnKey);
            }

            private void ClearSpace(ref Vector2 curPos, List<JumpTypes> prevJumps, int difficulty)
            {
                int width = 12;
                int height = 12;

                var prevJump = prevJumps[prevJumps.Count - 1];
                switch (prevJump)
                {
                    case JumpTypes.RIGHT:
                        curPos.X += 10 - difficulty;
                        width *= 2;
                        break;
                    case JumpTypes.UPRIGHT:
                        height *= 2;
                        width *= 2;
                        curPos.X += 9 - difficulty;
                        curPos.Y -= difficulty > 3 ? 6 : 5;
                        break;
                    case JumpTypes.UPLEFT:
                        height *= 2;
                        curPos.X -= 9 - difficulty;
                        curPos.Y -= difficulty > 3 ? 6 : 5;
                        break;
                    case JumpTypes.LEFT:
                        curPos.X -= 10 - difficulty;
                        width *= 2;
                        break;
                }

                for (int i = (int)curPos.X - width; i < (int)curPos.X + width; i++)
                {
                    for (int j = (int)curPos.Y - height; j < (int)curPos.Y + height; j++)
                    {
                        if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY)
                            continue;

                        if (Main.tile[i, j].TileType == placeholder && Vector2.Distance(curPos, new Vector2(i, j)) < width)
                            WorldGen.KillTile(i, j, false, false, true);
                    }
                }
            }

            private void GenJumpPlatform(ref Vector2 curPos, List<JumpTypes> prevJumps, bool spawnKey = false)
            {
                int width = 1 + rand.Next(6);
                var prevJump = prevJumps[prevJumps.Count - 1];
                bool prevCol = false, prevVine = false;
                if (prevJump == JumpTypes.UPLEFT || prevJump == JumpTypes.LEFT)
                {
                    curPos.X -= width;
                    for (int i = (int)curPos.X; i <= (int)curPos.X + width; i++)
                    {
                        int j = (int)curPos.Y;
                        if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY)
                            continue;

                        WorldGen.PlaceTile(i, j, tileID, true, true);
                        if (!prevCol && !prevVine && Main.rand.NextBool(8))
                        {
                            WorldGen.PlaceTile(i, j + 1, colPlaceholder, true, true);
                            prevCol = true;
                        }
                        else if (!prevCol && !prevVine && Main.rand.NextBool(8))
                        {
                            WorldGen.PlaceTile(i, j + 1, vinePlaceholder, true, true);
                            prevVine = true;
                        }
                        if (spawnKey && i == (int)curPos.X)
                            WorldGen.PlaceTile(i, j - 2, ModContent.TileType<KeystoneTile>(), true, true);
                    }
                }
                else
                {
                    for (int i = (int)curPos.X; i <= (int)curPos.X + width; i++)
                    {
                        int j = (int)curPos.Y;
                        if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY - 2)
                            continue;

                        WorldGen.PlaceTile(i, j, tileID, true, true);
                        if (!prevCol && !prevVine && Main.rand.NextBool(8))
                        {
                            WorldGen.PlaceTile(i, j + 1, colPlaceholder, true, true);
                            prevCol = true;
                        }
                        else if (!prevCol && !prevVine && Main.rand.NextBool(8))
                        {
                            WorldGen.PlaceTile(i, j + 1, vinePlaceholder, true, true);
                            prevVine = true;
                        }
                        if (spawnKey && i == (int)curPos.X)
                            WorldGen.PlaceTile(i, j - 2, ModContent.TileType<KeystoneTile>(), true, true);
                    }
                    curPos.X += width;
                }
            }

            private void GenFlatRoom(ref Vector2 curPos, int height, int width, bool makeExit = false)
            {
                for (int i = (int)curPos.X - width; i < (int)curPos.X + width; i++)
                {
                    for (int j = (int)curPos.Y - height; j <= (int)curPos.Y; j++)
                    {
                        if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY)
                            continue;

                        if (j == curPos.Y - 4 && !makeExit && i == curPos.X - 4)
                            WorldGen.PlaceTile(i, j, fullVine, true, true);
                        else if (j == curPos.Y && (makeExit || i > curPos.X - 3))
                        {
                            WorldGen.PlaceTile(i, j, ModContent.TileType<TemplePlatform>(), true, true);
                            //WorldGen.PlaceTile(i, j, TileID.Platforms, true, true, -1, 2);
                        }
                        else if (Vector2.Distance(curPos, new Vector2(i, j)) < width)
                            WorldGen.KillTile(i, j, false, false, true);
                    }
                }
                if (makeExit)
                {
                    WorldGen.PlaceTile((int)curPos.X, (int)curPos.Y - 1, ModContent.TileType<TestTriggerTile>(), true, true);
                    int numKeys = 0;
                    for (int i = 0; i < Main.maxTilesX; i++)
                    {
                        for (int j = 0; j < Main.maxTilesY; j++)
                        {
                            if (Main.tile[i, j].TileType == ModContent.TileType<KeystoneTile>())
                                numKeys++;
                        }
                    }
                    if (numKeys < 3)
                    {
                        for (int i = numKeys; i < 3; i++)
                            WorldGen.PlaceTile((int)curPos.X, (int)curPos.Y - 4 - i, ModContent.TileType<KeystoneTile>(), true, true);
                    }
                }
                curPos.X += width;
            }


        }

        public class GenRoots : GenPass
        {
            bool onlyReplacePlaceholder;
            int tileID, wallID, num, steps;
            float size;
            public GenRoots(int tile, int wall, int num, int steps, float size, bool onlyReplacePlaceholder = true) : base("Terrain", 1)
            {
                tileID = tile;
                this.num = num;
                wallID = wall;
                this.steps = steps;
                this.size = size;
                this.onlyReplacePlaceholder = onlyReplacePlaceholder;
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                for (int n = 0; n < num; n++)
                {
                    var x = Main.rand.Next(20, Main.maxTilesX - 20);
                    var y = Main.rand.Next(20, Main.maxTilesY - 20);
                    double dir = Main.rand.NextDouble() * 2 * Math.PI;
                    double curve = -.08 + .16 * Main.rand.NextDouble();
                    double jerk = -.02 + .04 * Main.rand.NextDouble();
                    for (int s = 0; s < steps; s++)
                    {
                        for (int i = x - (int)size; i <= x + size; i++)
                        {
                            for (int j = y - (int)size; j <= y + size; j++)
                            {
                                var dist = Vector2.Distance(new Vector2(x, y), new Vector2(i, j));
                                if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY &&
                                    (Main.tile[i, j].TileType == placeholder || !onlyReplacePlaceholder) &&
                                    (dist < size || dist < size + 3 && Main.rand.NextBool((int)dist + 1 - (int)size)))
                                {
                                    if (tileID != -1)
                                        WorldGen.PlaceTile(i, j, tileID, true, true);
                                    if (wallID != -1)
                                        WorldGen.PlaceWall(i, j, wallID, true);
                                }
                            }
                        }
                        x += (int)(Math.Cos(dir) * size);
                        y += (int)(Math.Sin(dir) * size);
                        dir += curve;
                        curve += jerk;
                    }
                }
            }
        }
    }
}
