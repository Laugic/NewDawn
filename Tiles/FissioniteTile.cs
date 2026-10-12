using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class FissioniteTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = true;
            MineResist = 2.5f;
            MinPick = 50;
            HitSound = SoundID.Tink;
            //DustType = ModContent.DustType<EtherialDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0f;
            g = 0.2f;
            b = 0.3f;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}