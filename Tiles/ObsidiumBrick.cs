using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class ObsidiumBrick : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = false;
            Main.tileBlockLight[Type] = true;
            AddMapEntry(new Color(40, 40, 40));
            MineResist = 1f;
            MinPick = 20;
            RegisterItemDrop(ModContent.ItemType<Items.Placeable.ObsidiumBrick>());
            HitSound = SoundID.Tink;
            DustType = 1;
            //soundStyle = 1;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        
        /*public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.2f;
            g = 0.1f;
            b = 0.2f;
        }
        
        public override bool CanExplode(int i, int j)
        {
            return false;
        }*/

    }
}