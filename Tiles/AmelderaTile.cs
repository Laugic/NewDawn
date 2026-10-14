using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace NewDawn.Tiles
{
    public abstract class AmelderaTile : ModTile
    {
        public Asset<Texture2D> obsidiumTexture, amelderaTexture;

        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
                obsidiumTexture = ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(GetType().Namespace.Replace('.', '/') + "/" + GetType().Name);
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (NewDawnWorld.Ameldera)
                TextureAssets.Tile[Type] = amelderaTexture;
            else
                TextureAssets.Tile[Type] = obsidiumTexture;
            return base.PreDraw(i, j, spriteBatch);
        }
    }
}
