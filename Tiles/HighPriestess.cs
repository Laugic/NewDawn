using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using NewDawn.Dusts;

namespace NewDawn.Tiles
{
    public class HighPriestess : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();

            // name.SetDefault("High Priestess");
            AddMapEntry(new Color(0, 150, 150), name);
            TileID.Sets.DisableSmartCursor[Type] = true;
            DustType = ModContent.DustType<EtherialDust>();
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Vector2 pos;
            pos.X = i * 16 - 24;
            pos.Y = j * 16 - 24;
            for (int k = 0; k < 12; k++)
            {
                Dust.NewDust(pos, 64, 64, ModContent.DustType<EtherialDust>(), 0f, 0f);
            }
        }

        public override void HitWire(int i, int j)
        {
            bool boss = false;
            Vector2 pos;
            pos.X = i * 16 - 24;
            pos.Y = j * 16 - 24;

            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].boss && Main.npc[k].active)
                {
                    boss = true;
                    break;
                }
            }

            if(!boss)
            {
                NewDawnWorld.downedEtheria = !NewDawnWorld.downedEtheria;

                for(int k = 0; k < 12; k++)
                    Dust.NewDust(pos, 64, 64, ModContent.DustType<EtherialDust>(), 0f, 0f);

                SoundEngine.PlaySound(NewDawn.CreateSoundStyle("Sounds/EtherialChange"));
            }
        }
    }
}
