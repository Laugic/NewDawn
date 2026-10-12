using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace NewDawn.Tiles
{
    public abstract class AmelderaTile : ModTile
    {
        public Texture2D obsidiumTexture, amelderaTexture;

        public override void SetStaticDefaults()
        {
            if(!Main.dedServ)
                obsidiumTexture = this.GetType().GetTexture();
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            if (NewDawnWorld.Ameldera)
                TextureAssets.Tile[Type].Value = amelderaTexture;
            else
                TextureAssets.Tile[Type].Value = obsidiumTexture;
            return base.PreDraw(i, j, spriteBatch);
        }
    }
}
