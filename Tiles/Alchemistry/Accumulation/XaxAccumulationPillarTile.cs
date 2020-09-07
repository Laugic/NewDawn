using Microsoft.Xna.Framework;
using NewDawn.Dusts;
using NewDawn.Items.Alchemistry.Placeables;
using NewDawn.Tiles.Alchemistry.Crystals;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using WebmilioCommons.Tiles;

namespace NewDawn.Tiles.Alchemistry.Accumulation
{
    public class XaxAccumulationPillarTile : StandardTile
    {
        private static int _range = 8;
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.Width = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Xax Accumulation Pillar");
            AddMapEntry(new Color(150, 150, 150), name);
            disableSmartCursor = true;
        }

        public override void RandomUpdate(int i, int j)
        {
            var crystals = FindNearbyCrystals(i, j);
            var crystal = Main.rand.Next(0, crystals.Count);

            var iPos = (i - Main.tile[i, j].frameX / 16) * 16;
            var jPos = (j - 1 - Main.tile[i, j].frameY / 16) * 16 + 4;
            var targetPos = new Vector2(crystals[crystal].X * 16, crystals[crystal].Y * 16);
            if (crystals.Count > 0)
            {
                var dust = Dust.NewDust(new Vector2(iPos, jPos), 16, 8, ModContent.DustType<White>());
                Main.dust[dust].velocity = new Vector2(GetSpeedX(iPos, jPos, targetPos, White.maxLife), GetSpeedY(iPos, jPos, targetPos, White.maxLife));
            }
        }

        private List<Vector2> FindNearbyCrystals(int i, int j)
        {
            var crystal = ModContent.TileType<CrackedCosmicCrystalTile>();
            List<Vector2> crystals = new List<Vector2>();
            for (int x = -_range; x <= _range; x++)
            {
                for (int y = -_range; y <= _range; y++)
                {
                    var Pos = TileMethods.ReturnSafePos(i + x, j + y);
                    if (Main.tile[(int)Pos.X, (int)Pos.Y].type == crystal)
                        crystals.Add(Pos);
                }
            }
            return crystals;
        }

        private float GetSpeedX(int i, int j, Vector2 targetPos, int life)
        {
            var Pos = new Vector2(i, j);
            var toTarget = targetPos - Pos;

            return toTarget.X / life;
        }

        private float GetSpeedY(int i, int j, Vector2 targetPos, int life)
        {
            var Pos = new Vector2(i, j);
            var toTarget = targetPos - Pos;

            return toTarget.Y / life;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 2 * 16, 3 * 16, ModContent.ItemType<XaxPillarItem>());
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
        {
            offsetY = 2;
        }

    }
}
