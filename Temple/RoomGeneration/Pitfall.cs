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
using static NewDawn.Temple.RoomGeneration.Sanctuary;
using static tModPorter.ProgressUpdate;

namespace NewDawn.Temple.RoomGeneration
{
    public class Pitfall : TempleWorld
    {
        public override int Width => 800;
        public override int Height => 2400;
        public Random rand = new Random();


        public override List<GenPass> Tasks => new()
        {
            new Fill(placeholder),
            new GenPits((ushort)ModContent.TileType<TempleBrick>()),
            //new GenDecoBlobs(-1, WallID.LivingLeaf, 120, 6, 12, false),
            //new GenDecoBlobs(-1, ModContent.WallType<TempleBrickWall>(), 120, 16, 24, false),
            //new GenDecoBlobs(TileID.LeafBlock, WallID.LivingLeaf, 120, 1, 2),
            //new GenDecoBlobs(TileID.LivingMahoganyLeaves, WallID.LivingLeaf, 120, 6, 12),


            new Replace((ushort)ModContent.TileType<TempleBrick>(), placeholder),
            new GenColumns(TileID.MarbleColumn, TileID.VineRope),
        };
        public class GenPits : GenPass
        {
            public ushort tileID, columnID, vineID;
            Random rand = new();
            int Width, Height, numKeystones, keyCounter;

            private static int segment = 80;
            public float DIFFICULTY = 32f;
            public GenPits(ushort tile) : base("Terrain", 1)
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
                Main.spawnTileX = Main.maxTilesX / 2 - segment / 2 + 7;
                Main.spawnTileY = segment * 4 + 12;

                GenPit(Main.maxTilesX / 2 - segment / 2, segment * 4, rand.Next(12, 16));
            }
            private void GenPit(float startX, int startY, float startWidth)
            {
                #region var inits
                float width = startWidth;
                float x = startX;
                int y = startY;
                int slantDir = 0;
                if (x < segment * 2)
                    slantDir = 1;
                else if (x > Main.maxTilesX - segment * 2)
                    slantDir = -1;
                else
                    slantDir = -1 + rand.Next(3) * 2;
                int height = rand.Next(segment, segment * 2);
                float slope = (float)(rand.NextDouble() / 2 + rand.NextDouble() / 2) * DIFFICULTY;
                float grow = .98f + (float)rand.NextDouble() * .04f;
                #endregion

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < (int)width; j++)
                    {
                        if (TileCheckSafe((int)x + j, y + i))
                            WorldGen.KillTile((int)x + j, y + i);
                    }

                    x += slantDir * slope / height;
                    x += width / 2;
                    width *= grow;
                    width = Math.Max(Math.Min(width, segment / 4), segment / 8); //restricting size of tunnel
                    x -= width / 2;
                }
                if (y + height < Main.maxTilesY - segment * 3)
                    GenPit(x, y + height, width);
                else
                {
                    //make water to not die and then make exit to the right
                }
            }
        }

        ///Making sure tiles arent out of bounds
        private static bool TileCheckSafe(int i, int j)
        {
            if (i > 1 && i < Main.maxTilesX - 1 && j > 1 && j < Main.maxTilesY - 1)
                return true;
            return false;
        }
    }
}
