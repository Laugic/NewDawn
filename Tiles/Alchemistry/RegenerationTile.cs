using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Items.Alchemistry.Crystals;
using NewDawn.Items.Alchemistry.Essences;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace NewDawn.Tiles.Alchemistry
{
    public class RegenerationTile : CrystalTile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            RegisterItemDrop(ModContent.ItemType<RegenerationCrystal>());
        }
        public override void AddBuff()
        {
            Main.LocalPlayer.AddBuff(BuffID.Regeneration, 8, true);
        }
        
        public override void GetDrop(int i, int j)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<RegenerationCrystal>());
        }
    }
}