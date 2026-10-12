using NewDawn.Dusts;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using WebCom.Extensions;

namespace NewDawn.Tiles
{
    public class ObsidiumPlantGrass2 : AmelderaTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Lycoris Radiata");
            //AddMapEntry(new Color(200, 30, 0), name);
            disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
            Main.tileCut[Type] = true;
            DustType = ModContent.DustType<Magma>();
            HitSound = 6;
            //adjTiles = new int[] { TileID.WorkBenches };

            if (!Main.dedServ)
            {
                obsidiumTexture = this.GetType().GetTexture();
                amelderaTexture = Mod.GetTexture(this.GetType().GetRootPath() + "/EldergrassPlant2");
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 2;
        }
    }
}