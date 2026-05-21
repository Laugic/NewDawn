using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace NewDawn.UI
{
    public class SpriteProgressBar : UIElement
    {
        public string BGTexturePath;
        public string BarTexturePath;
        public string FGTexturePath;

        public Vector2 Position;
        public Vector2 PosOffset;

        public float Alpha = 1f;
        public float FGAlpha = 1f;
        public int MaxVal;
        public int CurVal;
        public string Display = "";

        private int FrameCount = 0;
        private int FrameTimer = 0;
        private int CurrentFrame;
        private int FrameSpeed = 4;

        private Color[] Colors = new Color[] { Color.White, Color.White };

        private float[] Scales = new float[] { 1f, 1f };
        private int[] Widths = new int[] { 0, 0 };
        private int[] Heights = new int[] { 0, 0 };

        public bool Visible { get; set; } = false;

        public SpriteProgressBar(string bgTexturePath, string barTexturePath, string fgTexturePath, Vector2 pos, Vector2 offset = new Vector2(), int frameCount = 1, Color c1 = new Color(), Color c2 = new Color())
        {
            BGTexturePath = bgTexturePath;
            BarTexturePath = barTexturePath;
            FGTexturePath = fgTexturePath;
            Position = pos;
            PosOffset = offset;

            if (c1 != new Color())
                Colors[0] = c1;

            if (c2 != new Color())
                Colors[1] = c2;
            FrameCount = frameCount;
        }

        public SpriteProgressBar(int width1, int height1, int width2, int height2, Vector2 pos, Vector2 offset)
        {
            Widths[0] = width1;
            Widths[1] = width2;
            Heights[0] = height1;
            Heights[1] = height2;
            Position = pos;
            PosOffset = offset;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawBar(spriteBatch, CurVal, MaxVal);
        }

        public void DrawBar(SpriteBatch spriteBatch, int currentValue, int maxValue)
        {
            #region LoadTextures
            Texture2D BGTexture = (Texture2D)ModContent.Request<Texture2D>(BGTexturePath);
            Texture2D BarTexture = (Texture2D)ModContent.Request<Texture2D>(BarTexturePath);
            Texture2D FGTexture = (Texture2D)ModContent.Request<Texture2D>(FGTexturePath);
            #endregion

            if (Visible)
            {
                #region Drawing
                //Testing
                /*
                Texture2D texture = (Texture2D)ModContent.Request<Texture2D>("NewDawn/UI/Temple/SpiritBG");
                Texture2D texture2 = (Texture2D)ModContent.Request<Texture2D>(BGTexturePath);

                spriteBatch.Draw(texture2, Position, texture.Frame(), Color.White, 0, Vector2.Zero, 1, 0, 0);

                */
                int frameHeight = FrameCount > 1 ? BGTexture.Height / FrameCount : BGTexture.Height;
                Rectangle rect1 = new Rectangle(0, CurrentFrame * frameHeight, BGTexture.Width, frameHeight);
                if (Widths[0] != 0)
                    rect1 = new Rectangle(0, 0, Widths[0], Heights[0]);
                Rectangle rect2 = new Rectangle(0, 0, CalcLength(currentValue, maxValue, BarTexture.Width), BarTexture.Height);
                if (Widths[1] != 0)
                    rect2 = new Rectangle(0, 0, CalcLength(currentValue, maxValue, Widths[1]), Heights[1]);
                spriteBatch.Draw
                    (
                        BGTexture,
                        Position,
                        rect1,
                        Colors[0] * Alpha,
                        0f,
                        Vector2.Zero,
                        Scales[0],
                        SpriteEffects.None,
                        1f
                    );
                spriteBatch.Draw
                    (
                        BarTexture,
                        new Vector2((int)Position.X, (int)Position.Y) + new Vector2((int)PosOffset.X, (int)PosOffset.Y),
                        rect2,
                        Colors[1] * Alpha,
                        0f,
                        Vector2.Zero,
                        Scales[1],
                        SpriteEffects.None,
                        1f
                    );
                spriteBatch.Draw
                (
                    FGTexture,
                    new Vector2((int)Position.X, (int)Position.Y),
                    rect1,
                    Colors[0] * Alpha * FGAlpha,
                    0f,
                    Vector2.Zero,
                    Scales[0],
                    SpriteEffects.None,
                    1f
                );
                #endregion

                if (FrameCount > 1)
                {
                    if (++FrameTimer > FrameSpeed)
                    {
                        if (CurrentFrame < FrameCount - 1)
                            CurrentFrame++;
                        else
                            CurrentFrame = 0;
                        FrameTimer = 0;
                    }
                }

                Vector2 drawPos = (Main.MouseWorld - Main.screenPosition);

                drawPos.X += 20;
                drawPos.Y += 8;

                float posX = Main.MouseWorld.X - Main.screenPosition.X;
                float posY = Main.MouseWorld.Y - Main.screenPosition.Y;
                if (posX > Position.X && posX < Position.X + BGTexture.Width && posY > Position.Y && posY < Position.Y + BGTexture.Height && Display != "")
                    spriteBatch.DrawString(FontAssets.MouseText.Value, Display, drawPos, Color.White);
            }
        }
        public int CalcLength(int val, int maxVal, int TextureWidth)
        {
            return (val * TextureWidth) / maxVal;
        }
    }
}
