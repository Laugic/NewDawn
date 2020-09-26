using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using NewDawn.Subworlds.MoldyrTemple;
using Terraria;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace NewDawn.Tiles.Subworlds.MoldyrTemple
{
    public class TestTriggerTile : ModTile
    {
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
            name.SetDefault("Test Temple Entrance Pillar");
            AddMapEntry(new Color(150, 150, 150), name);
            disableSmartCursor = true;
        }

        public override bool NewRightClick(int i, int j)
        {
            MoldyrTempleMain.GenerateSubworld();
            return true;
        }
    }
}
