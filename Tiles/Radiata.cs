using Microsoft.Xna.Framework;
using NewDawn.Dusts;
using NewDawn.Items.Placeable;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class Radiata : AmelderaTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(160, 15, 0));
            MineResist = .5f;
            MinPick = 10;
            RegisterItemDrop(ModContent.ItemType<Items.Placeable.Radiata>());
            DustType = ModContent.DustType<Magma>();

            if (!Main.dedServ)
            {
                obsidiumTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/" + GetType().Name);
                amelderaTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/ElderootTile");
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void RandomUpdate(int i, int j)
        {
            int randm = Main.rand.Next(18);
            if (randm < 9)
            {
                if (CheckTile(i, j + 1))
                {
                    Terraria.WorldGen.PlaceTile(i, j + 1, ModContent.TileType<ObsidiumVine>(), true);
                }
                else if (Main.tile[i, j + 1].TileType == ModContent.TileType<ObsidiumVine>())
                {
                    for (int k = 1; k < 12; k++)
                    {
                        if (Main.tile[i, j + k].TileType != ModContent.TileType<ObsidiumVine>())
                        {
                            if (Main.tile[i, j + k].TileType == 0)
                                Terraria.WorldGen.PlaceTile(i, j + k, ModContent.TileType<ObsidiumVine>(), true);
                            break;
                        }
                    }
                }
            }
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (j < Main.maxTilesY - 4)
            {
                if (Main.tile[i, j + 1].TileType == ModContent.TileType<ObsidiumVine>())
                    Terraria.WorldGen.KillTile(i, j + 1);
            }
        }
        private bool CheckTile(int i, int j)
        {
            if (Main.tile[i, j].TileType != 0)
                return false;
            return true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            if (NewDawnWorld.Ameldera)
                return new List<Item> { new Item(ModContent.ItemType<ElderootItem>()) };
            return new List<Item> { new Item(ModContent.ItemType<Items.Placeable.Radiata>()) };
        }
    }
}