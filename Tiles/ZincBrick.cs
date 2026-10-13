using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class ZincBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(80, 130, 100));
            MineResist = 1f;
            MinPick = 20;
            RegisterItemDrop(ModContent.ItemType<Items.Placeable.ZincBrick>());
            HitSound = SoundID.Tink;
            DustType = 1;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        

    }
}