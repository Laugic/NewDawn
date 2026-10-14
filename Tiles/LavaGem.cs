using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Dusts;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace NewDawn.Tiles
{
    public class LavaGem : AmelderaTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = false;
            Main.tileLighted[Type] = true;

            DustType = ModContent.DustType<Magma>();
            RegisterItemDrop(ModContent.ItemType<Items.Placeable.LavaGemItem>());

            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Lava Gem");
            AddMapEntry(new Color(180, 50, 0), name);

            TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                56, //TileID.Obsidian
				ModContent.TileType<Tiles.ObsidiumRock>()
            };
            TileObjectData.addTile(Type);

            if (!Main.dedServ)
            {
                obsidiumTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/" + GetType().Name);
                amelderaTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/ArcanaGemTile");
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if ((i % 10) < 5)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            frameXOffset = i % 5 * 18;
        }

        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 4;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = .2f;
            if (NewDawnWorld.Ameldera)
                r = .05f;
            g = 0.08f;
            b = 0.0f;
            if (NewDawnWorld.Ameldera)
                b = .2f;
        }

        //public override bool Drop(int i, int j)/* tModPorter Note: Removed. Use CanDrop to decide if an item should drop. Use GetItemDrops to decide which item drops. Item drops based on placeStyle are handled automatically now, so this method might be able to be removed altogether. */
        //{
        //    if (NewDawnWorld.Ameldera)
        //    {
        //        Item.NewItem(i * 16, j * 16, 8, 8, ModContent.ItemType<ElderockItem>(), 1);
        //        return false;
        //    }
        //    return base.Drop(i, j);
        //}
    }
}