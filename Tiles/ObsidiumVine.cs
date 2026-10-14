using Microsoft.Xna.Framework.Graphics;
using NewDawn.Dusts;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class ObsidiumVine : AmelderaTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileNoFail[Type] = true;
            Main.tileLighted[Type] = true;
            LocalizedText name = CreateMapEntryName();
            //AddMapEntry(new Color(150, 0, 0), name);
            HitSound = SoundID.Dig;
            DustType = ModContent.DustType<Magma>();

            if (!Main.dedServ)
            {
                obsidiumTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/" + GetType().Name);
                amelderaTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/AmelderaVineTile");
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }


        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if ((j % 2 == 0 || i % 2 == 0) && !(j % 2 == 0 && i % 2 == 0))
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = -2;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = .1f;
            if (NewDawnWorld.Ameldera)
                r = 0;
            g = 0.05f;
            b = 0.0f;
            if (NewDawnWorld.Ameldera)
                b = .1f;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (j < Main.maxTilesY - 4)
            {
                if (Main.tile[i, j + 1].TileType == ModContent.TileType<ObsidiumVine>())
                    Terraria.WorldGen.KillTile(i, j + 1);
            }
        }
    }
}