using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace NewDawn.UI.NewAttempts.Mystic
{
    public class SpriteProgressBar : UIElement
    {
        public string BGTexturePath;
        public string BarTexturePath;
        public string FGTexturePath;

        public Texture2D BGTexture;
        public Texture2D BarTexture;
        public Texture2D FGTexture;

        public UIImage BGImage, BarImage, FGImage;

        public Vector2 Position;
        public Vector2 PosOffset;

        public float Alpha = 1f;
        public float FGAlpha = 1f;
        public int MaxVal;
        public int CurVal;
        public float FillPercent = 0.5f;
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

            BGImage = new UIImage((Texture2D)ModContent.Request<Texture2D>(BGTexturePath));
            BGImage.Left.Set(22, 0f);
            BGImage.Top.Set(0, 0f);
            BGImage.Width.Set(138, 0f);
            BGImage.Height.Set(34, 0f);
            BarImage = new UIImage((Texture2D)ModContent.Request<Texture2D>(BarTexturePath));
            BarImage.Left.Set(22, 0f);
            BarImage.Top.Set(0, 0f);
            BarImage.Width.Set(138, 0f);
            BarImage.Height.Set(34, 0f);
            FGImage = new UIImage((Texture2D)ModContent.Request<Texture2D>(FGTexturePath));
            FGImage.Left.Set(22, 0f);
            FGImage.Top.Set(0, 0f);
            FGImage.Width.Set(138, 0f);
            FGImage.Height.Set(34, 0f);

            Position = pos;
            PosOffset = offset;

            if (c1 != new Color())
                Colors[0] = c1;

            if (c2 != new Color())
                Colors[1] = c2;
            FrameCount = frameCount;
        }
        public SpriteProgressBar(Texture2D bgTexture, Texture2D barTexture, Texture2D fgTexture, Vector2 pos, Vector2 offset = new Vector2(), int frameCount = 1, Color c1 = new Color(), Color c2 = new Color())
        {
            BGTexture = bgTexture;
            BarTexture = barTexture;
            FGTexture = fgTexture;
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
            //DrawBar(spriteBatch, CurVal, MaxVal);
            DrawBar(spriteBatch, FillPercent);
        }
        public void DrawBar(SpriteBatch spriteBatch, float full, bool flipped = false)
        {
            #region LoadTextures
            if (BGTexture == null)
                BGTexture = (Texture2D)ModContent.Request<Texture2D>(BGTexturePath);
            if (BarTexture == null)
                BarTexture = (Texture2D)ModContent.Request<Texture2D>(BarTexturePath);
            if (FGTexture == null)
                FGTexture = (Texture2D)ModContent.Request<Texture2D>(FGTexturePath);
            #endregion

            if (!Visible)
                return;
            spriteBatch.Draw
                (
                    BGTexture,
                    Position,
                    new Rectangle(0, 0, BGTexture.Width, BGTexture.Height),
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
                    Position,
                    new Rectangle(0, 0, (int)(full * BarTexture.Width), BarTexture.Height),
                    Colors[0] * Alpha,
                    0f,
                    Vector2.Zero,
                    Scales[0],
                    SpriteEffects.None,
                    1f
                );
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("sprite rect width: " + (full * BarTexture.Width)), Color.White);
            spriteBatch.Draw
                (
                    FGTexture,
                    Position,
                    new Rectangle(0, 0, FGTexture.Width, FGTexture.Height),
                    Colors[0] * Alpha,
                    0f,
                    Vector2.Zero,
                    Scales[0],
                    SpriteEffects.None,
                    1f
                );
        }

        public void DrawBar(SpriteBatch spriteBatch, int currentValue, int maxValue, bool flipped = false)
        {
            #region LoadTextures
            if (BGTexture == null)
                BGTexture = (Texture2D)ModContent.Request<Texture2D>(BGTexturePath);
            if (BarTexture == null)
                BarTexture = (Texture2D)ModContent.Request<Texture2D>(BarTexturePath);
            if (FGTexture == null)
                FGTexture = (Texture2D)ModContent.Request<Texture2D>(FGTexturePath);
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
                Rectangle rect2 = new Rectangle(0, 0, (int)CalcLength(currentValue, maxValue, BarTexture.Width), BarTexture.Height);
                if (Widths[1] != 0)
                    rect2 = new Rectangle(0, 0, (int)CalcLength(currentValue, maxValue, Widths[1]), Heights[1]);
                if (flipped)
                    rect2.X = BarTexture.Width - rect2.Width;
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
                        new Vector2((int)Position.X + (flipped ? -rect2.Width : 0), (int)Position.Y) + new Vector2((int)PosOffset.X, (int)PosOffset.Y),
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

                Vector2 drawPos = Main.MouseWorld - Main.screenPosition;

                drawPos.X += 20;
                drawPos.Y += 8;

                float posX = Main.MouseWorld.X - Main.screenPosition.X;
                float posY = Main.MouseWorld.Y - Main.screenPosition.Y;
                if (posX > Position.X && posX < Position.X + BGTexture.Width && posY > Position.Y && posY < Position.Y + BGTexture.Height && Display != "")
                    spriteBatch.DrawString(FontAssets.MouseText.Value, Display, drawPos, Color.White);
            }
        }
        public float CalcLength(float val, float maxVal, float TextureWidth)
        {
            return val * TextureWidth / maxVal;
        }

        //internal void AppendTo(ref UIElement area)
        //{
        //    area.Append(BGImage);
        //    area.Append(BarImage);
        //    area.Append(FGImage);
        //}
    }
}
