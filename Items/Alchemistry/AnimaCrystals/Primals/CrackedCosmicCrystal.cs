using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Essences;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.AnimaCrystals.Primals
{
    public class CrackedCosmicCrystal : PrimalCrystal
    {
        public CrackedCosmicCrystal() : base(
            new[]
            {
                (GameCulture.English, "Cracked Cosmic Crystal", "Crystalized space rock\nPrimal"),
                (GameCulture.French, "Cristal Cosmique Fissuré", "Roche spatiale cristallisée\nPrimitif")
            },
            WIDTH, HEIGHT)
        {

        }


        public override void SetCrystalDefaults()
        {
            base.SetCrystalDefaults();

            item.createTile = ModContent.TileType<Tiles.Alchemistry.Crystals.CrackedCosmicCrystalTile>();
        }
    }
}
