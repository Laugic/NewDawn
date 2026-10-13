using NewDawn.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Structures
{
    public class ObsidiumHouse1
    {
        private static readonly int[,] _structureArray = new int[,]
        {
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,2,2,2,2},
            {2,5,4,4,9,9,9,9,9,9,7,9,9,9,9,9,9,9,9,9,9,9,7,9,2},
            {2,4,4,9,9,9,9,9,9,9,7,9,9,9,9,9,9,9,9,9,9,9,7,9,2},
            {2,5,9,9,9,9,9,9,9,9,5,9,9,9,9,9,9,9,9,9,9,9,7,9,2},
            {2,5,9,9,9,9,9,9,9,9,5,9,9,9,9,9,9,9,9,9,9,9,7,9,2},
            {2,4,9,9,9,9,9,9,5,4,5,5,9,9,9,9,9,9,9,9,9,9,7,9,2},
            {2,6,6,6,6,6,6,5,4,5,4,4,9,9,9,9,9,9,9,1,9,9,7,9,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,2,2,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,2,9,7,9,9,9,9,9,9,9,9,9,9,5,4,5,2},
            {0,0,0,0,0,0,0,0,2,9,7,9,9,9,9,9,9,9,9,9,9,9,5,4,2},
            {0,0,0,0,0,0,0,0,2,9,7,9,9,9,9,9,9,9,9,9,9,9,7,4,2},
            {0,0,0,0,0,0,0,0,2,9,7,9,9,9,9,9,9,9,9,9,9,4,5,5,2},
            {0,0,0,0,0,0,0,0,2,8,7,9,8,9,9,9,9,9,9,9,4,5,4,4,2},
            {0,0,0,0,0,0,0,0,2,2,2,2,2,2,2,2,2,3,3,3,2,2,2,2,2},

        };

        public static void StructureGen(int xPosO, int yPosO, bool mirrored)
        {
            //Obsidium Heart
            /**
             * 0 = Do Nothing
             * 1 = obsidium chest
             * 2 = Obsidium Brick
             * 3 = Platforms
             * 4 = Lycoris
             * 5 = Radiata
             * 6 = Lava
             * 7 = lavafall
             * 8 = wooden crate
             * 9 = Kill tile
             * */
            

            for (int i = 0; i < _structureArray.GetLength(1); i++)
            {
                for (int j = 0; j < _structureArray.GetLength(0); j++)
                {
                    if(mirrored)
                    {
                        if (TileCheckSafe((int)(xPosO + _structureArray.GetLength(1) - i), (int)(yPosO + j)))
                        {
                            if (_structureArray[j, i] == 1)
                            {
                                NewDawnWorld.PlaceObsidiumChest(xPosO + _structureArray.GetLength(1) - i, yPosO + j, (ushort)ModContent.TileType<ObsidiumBrick>());
                            }
                            if (_structureArray[j, i] == 2)
                            {
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j, ModContent.TileType<ObsidiumBrick>(), true, true);
                            }
                            if (_structureArray[j, i] == 3)
                            {
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j, 19, true, true, -1, 13);
                            }
                            if (_structureArray[j, i] == 4)
                            {
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j, ModContent.TileType<Lycoris>(), true, true);
                            }
                            if (_structureArray[j, i] == 5)
                            {
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j, ModContent.TileType<Tiles.Radiata>(), true, true);
                            }
                            if (_structureArray[j, i] == 6)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                int tileX = xPosO + _structureArray.GetLength(1) - i;
                                int tileY = yPosO + j;
                                Tile lavaTile = Main.tile[tileX, tileY];
                                lavaTile.LiquidType = LiquidID.Lava;
                                lavaTile.LiquidAmount = 255;
                            }
                            if (_structureArray[j, i] == 7)
                            {
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.KillWall(xPosO + _structureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + _structureArray.GetLength(1) - i, yPosO + j, 137, true); //Lavafall Wall
                            }
                            if (_structureArray[j, i] == 8)
                            {
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j);
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i + 1, yPosO + j);
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j - 1);
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i + 1, yPosO + j - 1);
                                Terraria.WorldGen.PlaceObject(xPosO + _structureArray.GetLength(1) - i, yPosO + j, 376, true, 0, -1, -1);
                            }
                            if (_structureArray[j, i] == 9)
                            {
                                Main.tile[xPosO + _structureArray.GetLength(1) - i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillTile(xPosO + _structureArray.GetLength(1) - i, yPosO + j);
                            }
                        }
                    }
                    else
                    {
                        if (TileCheckSafe((int)(xPosO + i), (int)(yPosO + j)))
                        {
                            if (_structureArray[j, i] == 1)
                            {
                                NewDawnWorld.PlaceObsidiumChest(xPosO + i, yPosO + j, (ushort)ModContent.TileType<ObsidiumBrick>());
                            }
                            if (_structureArray[j, i] == 2)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, ModContent.TileType<ObsidiumBrick>(), true, true);
                            }
                            if (_structureArray[j, i] == 3)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, 19, true, true, -1, 13);
                            }
                            if (_structureArray[j, i] == 4)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, ModContent.TileType<Lycoris>(), true, true);
                            }
                            if (_structureArray[j, i] == 5)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.PlaceTile(xPosO + i, yPosO + j, ModContent.TileType<Tiles.Radiata>(), true, true);
                            }
                            if (_structureArray[j, i] == 6)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                int tileX = xPosO + i;
                                int tileY = yPosO + j;
                                Tile lavaTile = Main.tile[tileX, tileY];
                                lavaTile.LiquidType = LiquidID.Lava;
                                lavaTile.LiquidAmount = 255;
                            }
                            if (_structureArray[j, i] == 7)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.KillWall(xPosO + i, yPosO + j);
                                WorldGen.PlaceWall(xPosO + i, yPosO + j, 137, true); //Lavafall Wall
                            }
                            if (_structureArray[j, i] == 8)
                            {
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                                WorldGen.KillTile(xPosO + i + 1, yPosO + j);
                                WorldGen.KillTile(xPosO + i, yPosO + j - 1);
                                WorldGen.KillTile(xPosO + i + 1, yPosO + j - 1);
                                Terraria.WorldGen.PlaceObject(xPosO + i, yPosO + j, 376, true, 0, -1, -1);
                            }
                            if (_structureArray[j, i] == 9)
                            {
                                Main.tile[xPosO + i, yPosO + j].LiquidAmount = 0;
                                WorldGen.KillTile(xPosO + i, yPosO + j);
                            }
                        }
                    }
                }
            }
        }
        
        //Making sure tiles arent out of bounds
        private static bool TileCheckSafe(int i, int j)
        {
            if (i > 1 && i < Main.maxTilesX - 1 && j > 1 && j < Main.maxTilesY - 1)
            {
                if (NewDawnVars.obsidiumTiles.Contains(Main.tile[i, j].TileType))
                    return true;
            }
            return false;
        }
    }
}