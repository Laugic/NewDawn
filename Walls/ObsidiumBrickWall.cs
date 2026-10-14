using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Walls
{
	public class ObsidiumBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallHouse[Type] = true;
			DustType = 1;
			AddMapEntry(new Color(11, 22, 33));
            RegisterItemDrop(ModContent.ItemType<Items.Placeable.ObsidiumBrickWall>());
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
		
	}
}