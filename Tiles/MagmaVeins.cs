using NewDawn.Dusts;
using NewDawn.Items.Loot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public class MagmaVeins : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileLighted[Type] = false;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Magma Shard");
            AddMapEntry(new Color(150, 100, 50));
            MineResist = 1f;
            MinPick = 60;
            RegisterItemDrop(ModContent.ItemType<DarkShard>());
            DustType = ModContent.DustType<Magma>();
            //soundType = 21;
            //soundStyle = 1;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.4f;
            g = 0.1f;
            b = 0f;
        }
        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override void RandomUpdate(int i, int j)
        {
            if (Main.rand.Next(2) == 0)
                NewDawnWorld.ObsidiumHeartGrowth++;
            if (Main.tile[i, j - 1].TileType == 0 && Main.tile[i + 1, j - 1].TileType == 0 && Main.tile[i, j - 2].TileType == 0 && Main.tile[i + 1, j - 2].TileType == 0 && Main.tile[i, j].HasTile)
            {
                if (NewDawnWorld.ObsidiumHeartGrowth > 20 && Main.rand.Next(4) == 0)
                {
                    NewDawnWorld.ObsidiumHeartGrowth = 0;
                    WorldGen.PlaceObject(i, j - 1, ModContent.TileType<ObsidiumHeart>(), true, 0, -1, -1);
                }
            }
        }
    }
}