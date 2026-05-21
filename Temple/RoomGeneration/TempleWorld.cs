using Microsoft.Xna.Framework;
using NewDawn.Items.Temple.Consumables;
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
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace NewDawn.Temple.RoomGeneration
{
    public class TempleWorld : Subworld
    {
        public override int Width => 400;
        public override int Height => 400;
        public Random rand = new Random();
        public static readonly ushort placeholder = TileID.Stone, colPlaceholder = TileID.BubblegumBlock, vinePlaceholder = TileID.GreenCandyCaneBlock, fullVine = TileID.CandyCaneBlock;

        public override List<GenPass> Tasks => new()
        {
            new Fill(TileID.Dirt)
        };

        public override bool GetLight(Tile tile, int x, int y, ref FastRandom rand, ref Vector3 color)
        {
            if (!base.GetLight(tile, x, y, ref rand, ref color))
                color = new Vector3(.004f, .004f, .004f);
            return base.GetLight(tile, x, y, ref rand, ref color);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            foreach (Player player in Main.player)
            {
                foreach (Item item in player.inventory)
                {
                    if (item.type == ModContent.ItemType<Keystone>())
                        item.stack = 0;
                }
            }
            SubworldSystem.hideUnderworld = true;
        }

        public override void Load()
        {
            Main.dayTime = true;
            Main.time = 27000;
        }

        public class Fill : GenPass
        {
            ushort tileID;
            public Fill(ushort tile) : base("Terrain", 1)
            {
                tileID = tile;
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Filling terrain"; // Sets the text displayed for this pass
                Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
                Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds
                //progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY)); // Controls the progress bar, should only be set between 0f and 1f
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        WorldGen.PlaceTile(i, j, tileID, true, true);
                    }
                }
            }
        }

        public class Replace : GenPass
        {
            ushort tileID, tileToReplace;
            public Replace(ushort tile, ushort tileToReplace) : base("Terrain", 1)
            {
                tileID = tile;
                this.tileToReplace = tileToReplace;
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Replacing terrain";
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY));
                        if (Main.tile[i, j].TileType == tileToReplace)
                            WorldGen.PlaceTile(i, j, tileID, true, true);
                    }
                }
            }
        }



        public class Blur : GenPass
        {
            ushort tileID;
            int amount;
            public Blur(ushort tile, int amount) : base("Terrain", 1)
            {
                tileID = tile;
                this.amount = amount;
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                progress.Message = "Blurring terrain";

                float[,] newTiles = new float[Main.maxTilesX, Main.maxTilesY];
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        float curVal = 0, maxVal = 0;
                        for (int a = -amount; a <= amount; a++)
                        {
                            for (int b = -amount; b <= amount; b++)
                            {
                                //progress.Set((b + 1) * (j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY * amount));
                                if (i + a > 0 && i + a < Main.maxTilesX && j + b > 0 && j + b < Main.maxTilesY)
                                {
                                    if (/*Main.tile[i + b, j + b].HasTile &&*/ Main.tile[i + a, j + b].TileType == tileID)
                                        curVal++;
                                }
                                maxVal++;
                            }
                        }
                        newTiles[i, j] = curVal / Math.Max(1, maxVal);
                    }
                }

                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        if (newTiles[i, j] >= .5f)
                        {
                            if (!Main.tile[i, j].HasTile)
                                WorldGen.PlaceTile(i, j, tileID, true, true);
                        }
                        else if (/*Main.tile[i, j].HasTile &&*/ Main.tile[i, j].TileType == tileID)
                            WorldGen.KillTile(i, j, false, false, true);
                        //WorldGen.PlaceTile(i, j, TileID.CrimtaneBrick, true, true);
                        /*if (newTiles[i, j] > 0f)
                        {
                            if (!Main.tile[i, j].HasTile)
                                WorldGen.PlaceTile(i, j, TileID.MarbleBlock, true, true);
                        }*/
                    }
                }

            }
        }

        public class GenColumns : GenPass
        {
            ushort columnID, vineID;
            public GenColumns(ushort column, ushort vine) : base("Terrain", 1)
            {
                columnID = column;
                vineID = vine;
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                Random rand = new Random();
                for (int i = 0; i < Main.maxTilesX; i++)
                {
                    for (int j = 0; j < Main.maxTilesY; j++)
                    {
                        if (Main.tile[i, j].TileType == colPlaceholder)
                        {
                            WorldGen.KillTile(i, j, false, false, true);
                            GenColumn(columnID, i, j, -1);
                        }
                        if (Main.tile[i, j].TileType == vinePlaceholder)
                        {
                            WorldGen.KillTile(i, j, false, false, true);
                            GenColumn(vineID, i, j, j + 6 + rand.Next(10));
                        }
                        if (Main.tile[i, j].TileType == fullVine)
                        {
                            WorldGen.KillTile(i, j, false, false, true);
                            GenColumn(vineID, i, j, -2);
                        }
                    }
                }
            }
        }

        private static void GenColumn(int id, int i, int j) { GenColumn(id, i, j, -1); }

        private static void GenColumn(int tileID, int i, int j, int max)
        {
            bool stop = false;
            Random rand = new Random();
            while (j < Main.maxTilesY - 1 && !stop && (j < max || max < 0))
            {
                if (!Main.tile[i, j].HasTile || Main.tile[i, j].TileType == placeholder)
                {
                    if (rand.Next(10) != 0 || max == -2)
                        WorldGen.PlaceTile(i, j, tileID, true, true);
                    j++;
                }
                else
                    stop = true;
            }
        }

        public class GenDecoBlobs : GenPass
        {
            bool onlyReplacePlaceholder;
            int tileID, wallID, num, min, max;
            public GenDecoBlobs(int tile, int wall, int number, int min, int max, bool onlyReplacePlaceholder = true) : base("Terrain", 1)
            {
                tileID = tile;
                num = number;
                wallID = wall;
                this.min = min;
                this.max = max;
                this.onlyReplacePlaceholder = onlyReplacePlaceholder;
            }

            protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
            {
                Random rand = new Random();
                for (int n = 0; n < num; n++)
                {
                    var x = rand.Next(20, Main.maxTilesX - 20);
                    var y = rand.Next(20, Main.maxTilesY - 20);
                    float size = rand.Next(min, max);
                    double dir = rand.NextDouble() * 2 * Math.PI;
                    double curve = -.8 + 1.6 * rand.NextDouble();
                    double jerk = -.2 + .4 * rand.NextDouble();
                    int step = 0;
                    while (size >= 1)
                    {
                        for (int i = x - (int)size / 2; i <= x + size / 2; i++)
                        {
                            for (int j = y - (int)size / 2; j <= y + size / 2; j++)
                            {
                                var dist = Vector2.Distance(new Vector2(x, y), new Vector2(i, j));
                                if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY &&
                                    (Main.tile[i, j].TileType == placeholder || !onlyReplacePlaceholder) &&
                                    (dist < size / 2 || dist < size / 2 + 3 && rand.Next((int)dist + 1 - (int)size / 2) == 0))
                                {
                                    if (tileID != -1)
                                        WorldGen.PlaceTile(i, j, tileID, true, true);
                                    if (wallID != -1)
                                        WorldGen.PlaceWall(i, j, wallID, true);
                                }
                            }
                        }
                        x += (int)(Math.Cos(dir) * size / 2);
                        y += (int)(Math.Sin(dir) * size / 2);
                        step++;
                        if (step % 2 == 0)
                            size *= .67f;
                        dir += curve;
                        curve += jerk;
                    }
                }
            }

            public class GenDeco : GenPass
            {
                bool onlyReplacePlaceholder;
                int tileID, wallID, num, steps, min, max;
                public GenDeco(int tile, int wall, int num, int steps, int min, int max, bool onlyReplacePlaceholder = true) : base("Terrain", 1)
                {
                    tileID = tile;
                    this.num = num;
                    wallID = wall;
                    this.steps = steps;
                    this.min = min;
                    this.max = max;
                    this.onlyReplacePlaceholder = onlyReplacePlaceholder;
                }

                protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
                {
                    Random rand = new Random();
                    for (int n = 0; n < num; n++)
                    {
                        var x = rand.Next(20, Main.maxTilesX - 20);
                        var y = rand.Next(20, Main.maxTilesY - 20);
                        float size = rand.Next(min, max);
                        double dir = rand.NextDouble() * 2 * Math.PI;
                        double curve = -.08 + .16 * rand.NextDouble();
                        double jerk = -.02 + .04 * rand.NextDouble();
                        for (int s = 0; s < steps; s++)
                        {
                            for (int i = x - (int)size; i <= x + size; i++)
                            {
                                for (int j = y - (int)size; j <= y + size; j++)
                                {
                                    var dist = Vector2.Distance(new Vector2(x, y), new Vector2(i, j));
                                    if (i > 0 && i < Main.maxTilesX && j > 0 && j < Main.maxTilesY &&
                                        (Main.tile[i, j].TileType == placeholder || !onlyReplacePlaceholder) &&
                                        (dist < size || dist < size + 3 && rand.Next((int)dist + 1 - (int)size) == 0))
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
}
