using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.AnimaCrystals.Primals
{
    public class CrackedCosmicCrystal : PrimalCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cracked Cosmic Crystal");
            Tooltip.SetDefault("Crystalized space rock\nPrimal");
        }

        public override void SetCrystalDefaults()
        {
            base.SetCrystalDefaults();

            item.createTile = ModContent.TileType<Tiles.Alchemistry.Crystals.CrackedCosmicCrystalTile>();
        }
    }
}
