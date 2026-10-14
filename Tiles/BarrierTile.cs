using NewDawn.Items.Placeable;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class BarrierTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            DustType = 0;
            RegisterItemDrop(ModContent.ItemType<Barrier>());
            MinPick = 225;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 0;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            return Main.LocalPlayer.GetModPlayer<NewDawnPlayer>().HoldingBarrier;
        }
    }
}