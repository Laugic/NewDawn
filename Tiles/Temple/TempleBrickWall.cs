using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NewDawn.Tiles.Temple
{
	public class TempleBrickWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			DustType = 1;
			AddMapEntry(new Color(11, 22, 33));
        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
		
	}
}