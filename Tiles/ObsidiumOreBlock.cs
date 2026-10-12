using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class ObsidiumOreBlock : AmelderaTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[56][ModContent.TileType<ObsidiumOreBlock>()] = true;
            Main.tileMerge[ModContent.TileType<ObsidiumOreBlock>()][56] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileLighted[Type] = true;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Obsidium Ore");
            AddMapEntry(new Color(150, 50, 50), name);
            MineResist = 1f;
            MinPick = 60;
            //ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ModContent.ItemType<ObsidiumOre>();
            DustType = DustID.Stone;

            if (!Main.dedServ)
            {
                obsidiumTexture = this.GetType().GetTexture();
                amelderaTexture = Mod.GetTexture(this.GetType().GetRootPath() + "/AmelderaOreTile");
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.4f;
            if (NewDawnWorld.Ameldera)
                r = 0.025f;
            g = 0.2f;
            b = 0f;
            if (NewDawnWorld.Ameldera)
            {
                b = .4f;
                MinPick = 200;
            }
            else
                MinPick = 60;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        //public override bool Drop(int i, int j)/* tModPorter Note: Removed. Use CanDrop to decide if an item should drop. Use GetItemDrops to decide which item drops. Item drops based on placeStyle are handled automatically now, so this method might be able to be removed altogether. */
        //{
        //    if (NewDawnWorld.Ameldera)
        //    {
        //        Item.NewItem(i * 16, j * 16, 8, 8, ModContent.ItemType<AmelderaOreItem>(), 1);
        //        return false;
        //    }
        //    return base.Drop(i, j);
        //}
    }
}