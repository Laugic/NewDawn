﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using WebmilioCommons.Tiles;

namespace NewDawn.Tiles.Alchemistry.Crystals
{
    public class CrackedCosmicCrystalTile : GlobalCrystalTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;

            //dustType = ModContent.DustType<Magma>();
            drop = ModContent.ItemType<Items.Alchemistry.AnimaCrystals.Primals.CrackedCosmicCrystal>();

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Cracked Cosmic Crystal");
            AddMapEntry(new Color(180, 180, 180), name);

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                TileID.Cloud,
                TileID.RainCloud
            };
            TileObjectData.addTile(Type);
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if ((i % 10) < 5)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            frameXOffset = i % 5 * 18;
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
        {
            offsetY = 4;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = .4f;
            g = .4f;
            b = .4f;
        }

        public override ushort GrowType { get => GrowType; set => GrowType = (ushort)ModContent.TileType<CuredCosmicCrystalTile>(); }
    }
}
