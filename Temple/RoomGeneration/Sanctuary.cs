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
using static tModPorter.ProgressUpdate;

namespace NewDawn.Temple.RoomGeneration
{
    public class Sanctuary : TempleWorld
    {
        private static int minHeight = 16;
        private static int maxHeight = 24;
        readonly ushort tile = (ushort)ModContent.TileType<TempleBrick>();

        public override int Width => 240;
        public override int Height => 200;

        public override List<GenPass> Tasks => new()
        {
            new Fill(placeholder),
            new GenSanctuary((ushort)ModContent.TileType<TempleBrick>()),
            new GenDecoBlobs(-1, WallID.LivingLeaf, 120, 6, 12, false),
            new GenDecoBlobs(-1, ModContent.WallType<TempleBrickWall>(), 120, 16, 24, false),
            new GenDecoBlobs(TileID.LeafBlock, WallID.LivingLeaf, 120, 1, 2),
            new GenDecoBlobs(TileID.LivingMahoganyLeaves, WallID.LivingLeaf, 120, 6, 12),


            new Replace((ushort)ModContent.TileType<TempleBrick>(), placeholder),
            new GenColumns(TileID.MarbleColumn, TileID.VineRope),
        };

        public class GenSanctuary : GenPass
        {
            public ushort tileID, columnID, vineID;
            Random rand = new();
            int Width, Height, numKeystones, keyCounter;
            public GenSanctuary(ushort tile) : base("Terrain", 1)
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
                Vector2 curPos = new Vector2(Main.maxTilesX / 2, Main.maxTilesY / 2);
                Main.spawnTileX = (int)curPos.X;
                Main.spawnTileY = (int)curPos.Y;
                var roomHeight = rand.Next(minHeight, maxHeight);
                var roomWidth = 30;

                GenFlatRoom(ref curPos, roomHeight, roomWidth);
            }
            private void GenFlatRoom(ref Vector2 curPos, int height, int width)
            {
                for (int i = (int)curPos.X - width; i < (int)curPos.X + width; i++)
                {
                    for (int j = (int)curPos.Y - height; j <= (int)curPos.Y; j++)
                    {
                        if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY)
                            continue;

                        if (Vector2.Distance(curPos, new Vector2(i, j)) < width)
                            WorldGen.KillTile(i, j, false, false, true);
                    }
                }

                if (TempleMain.floor % 7 == 0)
                    WorldGen.PlaceTile((int)curPos.X + 10, (int)curPos.Y, ModContent.TileType<TestTriggerTile>(), true, true);
                else
                {
                    //Generate statues
                }
                for (int i = (int)curPos.X - 16; i < (int)curPos.X - 6; i++)
                {
                    for (int j = (int)curPos.Y - 8; j <= (int)curPos.Y + 8; j++)
                    {
                        if (i < 0 || j < 0 || i >= Main.maxTilesX || j >= Main.maxTilesY)
                            continue;

                        if (Vector2.Distance(new Vector2(curPos.X - 10, curPos.Y - 2), new Vector2(i, j)) < 5)
                            WorldGen.PlaceWall(i, j, ModContent.WallType<TempleBrickWall>());
                    }
                }
                WorldGen.PlaceTile((int)curPos.X - 10, (int)curPos.Y, ModContent.TileType<ExitStairs>(), true, true);
            }
        }
    }
}
