using Microsoft.Xna.Framework;
using NewDawn.Items.Placeable;
using System.Collections.Generic;
using Terraria;
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
            //RegisterItemDrop(ModContent.ItemType<ObsidiumOre>());
            DustType = DustID.Stone;

            if (!Main.dedServ)
            {
                obsidiumTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/" + GetType().Name);
                amelderaTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/AmelderaOreTile");
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

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            if (NewDawnWorld.Ameldera)
                return new List<Item> { new Item(ModContent.ItemType<AmelderaOreItem>()) };
            return new List<Item> { new Item(ModContent.ItemType<ObsidiumOre>()) };
        }
    }
}