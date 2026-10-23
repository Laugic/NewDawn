using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Players;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using static NewDawn.Players.MysticPlayer;

namespace NewDawn.UI
{
    public class NewDawnUI
    {
        //Thanks for the help, DayOrk <3
        private const int BAR_WIDTH = 96;

        public static bool visible = true;
        public static SpriteProgressBar LuxBar;
        public static SpriteProgressBar OverflowLuxBar;
        public static Asset<Texture2D> LuxBGTexture;
        public static Asset<Texture2D> LuxBarTexture;
        public static Asset<Texture2D> LuxFGTexture;
        public static SpriteProgressBar MundusBar;
        public static SpriteProgressBar OverflowMundusBar;
        public static Asset<Texture2D> MundusBGTexture;
        public static Asset<Texture2D> MundusBarTexture;
        public static Asset<Texture2D> MundusFGTexture;
        public static SpriteProgressBar VisBar;
        public static SpriteProgressBar OverflowVisBar;
        public static Asset<Texture2D> VisBGTexture;
        public static Asset<Texture2D> VisBarTexture;
        public static Asset<Texture2D> VisFGTexture;
        public static Asset<Texture2D> OverflowBGTexture;
        public static Asset<Texture2D> OverflowBarTexture;
        public static Asset<Texture2D> OverflowFGTexture;
        public static Asset<Texture2D> LuxFGOverflowTexture;
        public static Asset<Texture2D> VisFGOverflowTexture;
        public static Asset<Texture2D> MundusOverflowTexture;
        public static Asset<Texture2D> MundusFGOverflowTexture;
        public static SpriteProgressBar MysticBurstBar;
        public static Asset<Texture2D> MysticBurstBGTexture;
        public static Asset<Texture2D> MysticBurstBarTexture;
        static float posOffset = 1f;
        public static Vector2 TopPosBase = new Vector2(((Main.screenWidth / 2) + (int)(posOffset * BAR_WIDTH) + ((float)Math.Cos(3f / 2f * Math.PI) * 100)) / Main.UIScale, ((Main.screenHeight / 2) + (int)(posOffset * BAR_WIDTH) + ((float)Math.Sin(3f / 2f * Math.PI) * 100) + 25) / Main.UIScale);
        public static Vector2 MidPosBase = new Vector2(((Main.screenWidth / 2) + (int)(posOffset * BAR_WIDTH) + ((float)Math.Cos(5f / 6f * Math.PI) * 100)) / Main.UIScale, ((Main.screenHeight / 2) + (int)(posOffset * BAR_WIDTH) + ((float)Math.Sin(5f / 6f * Math.PI) * 100) + 25) / Main.UIScale);
        public static Vector2 BotPosBase = new Vector2(((Main.screenWidth / 2) + (int)(posOffset * BAR_WIDTH) + ((float)Math.Cos(1f / 6f * Math.PI) * 100)) / Main.UIScale, ((Main.screenHeight / 2) + (int)(posOffset * BAR_WIDTH) + ((float)Math.Sin(1f / 6f * Math.PI) * 100) + 25) / Main.UIScale);
        private static Vector2 TopPos = new Vector2(0, 0);
        private static Vector2 MidPos = new Vector2(0, 0);
        private static Vector2 BotPos = new Vector2(0, 0);
        private static Vector2 MysticBurstPos = new Vector2(0, 0);

        private static MysticMode position = MysticMode.Destruction;
        private static float topTheta = -100;
        private static float midTheta = -100;
        private static float botTheta = -100;
        private static float topMag = 0;
        private static float midMag = 0;
        private static float botMag = 0;
        private static float magMax = 5;
        private static float rotation = (float)Math.PI * 2f;
        private static float rotationGoal = (float)Math.PI * 2;

        public static MysticMode targetPosition = MysticMode.Destruction;
        private static int mysticBurstCooldownMax = 0;

        //CONFIGURABLES
        public static int MysticUIType { get; set; }
        public static bool Animated { get; set; }
        public static bool SoundOn { get; set; }

        //CONFIG RELATED VALUES
        private static Vector2 CenterPos = new Vector2(0, 0);
        private static Vector2 CENTER = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
        private static Vector2 TOPCIRCLE = new Vector2(Main.screenWidth / 2, 100 / Main.UIScale);
        private static Vector2 TOPSTACK = new Vector2(Main.screenWidth / 2, 30 / Main.UIScale);

        static NewDawnUI()
        {
            visible = true;
        }

        public static void Load()
        {
            LuxBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/LuxBG");
            LuxBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/LuxBar");
            LuxFGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/LuxFG");
            MundusBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusBG");
            MundusBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusBar");
            MundusFGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusFG");
            VisBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/VisBG");
            VisBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/VisBar");
            VisFGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/VisFG");
            OverflowBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/OverflowBG");
            OverflowBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/OverflowBar");
            OverflowFGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/OverflowFG");
            LuxFGOverflowTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/LuxFGOverflow");
            VisFGOverflowTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/VisFGOverflow");
            MundusOverflowTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusOverflow");
            MundusFGOverflowTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MundusFGOverflow");
            MysticBurstBGTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MysticBurstBG");
            MysticBurstBarTexture = ModContent.Request<Texture2D>("NewDawn/UI/Mystic/MysticBurstBar");

            LuxBar = new SpriteProgressBar
            (
                LuxBGTexture,
                LuxBarTexture,
                LuxFGTexture,
                TopPos,
                new Vector2(0, 0)
            );

            OverflowLuxBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                LuxFGOverflowTexture,
                TopPos,
                new Vector2(OverflowBarTexture.Width(), 0)
            );

            MundusBar = new SpriteProgressBar
            (
                MundusBGTexture,
                MundusBarTexture,
                MundusFGTexture,
                BotPos,
                new Vector2(0, 0)
            );

            OverflowMundusBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                MundusFGOverflowTexture,
                BotPos,
                new Vector2(OverflowBarTexture.Width(), 0)
            );

            VisBar = new SpriteProgressBar
            (
                VisBGTexture,
                VisBarTexture,
                VisFGTexture,
                MidPos,
                new Vector2(0, 0)
            );

            OverflowVisBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                VisFGOverflowTexture,
                MidPos,
                new Vector2(OverflowBarTexture.Width(), 0)
            );

            MysticBurstBar = new SpriteProgressBar
            (
                MysticBurstBGTexture,
                MysticBurstBarTexture,
                OverflowFGTexture,
                MysticBurstPos,
                new Vector2(0, 0)
            );
        }

        public static void Unload()
        {
            LuxBGTexture = null;
            LuxBarTexture = null;
            LuxFGTexture = null;
            MundusBGTexture = null;
            MundusBarTexture = null;
            MundusFGTexture = null;
            VisBGTexture = null;
            VisBarTexture = null;
            VisFGTexture = null;
            OverflowBGTexture = null;
            OverflowBarTexture = null;
            OverflowFGTexture = null;
            VisFGOverflowTexture = null;
            MundusOverflowTexture = null;
            MundusFGOverflowTexture = null;
            LuxFGOverflowTexture = null;
            MysticBurstBGTexture = null;
            MysticBurstBarTexture = null;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();

            visible = mysticPlayer.IsHoldingMystic();
            float luxTemp = mysticPlayer.Lux;

            if (luxTemp > mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost)
                luxTemp = mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost;

            if (LuxBar != null)
                LuxBar.DrawSelf(spriteBatch, (int)luxTemp, (int)(mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost));

            if (OverflowLuxBar != null && mysticPlayer.Lux > mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost)
                OverflowLuxBar.DrawSelf(spriteBatch, Math.Min((int)(mysticPlayer.Lux - mysticPlayer.LuxMax - mysticPlayer.LuxMaxPermaBoost), (int)(mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost)), (int)(mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost), true);

            float mundusTemp = mysticPlayer.Mundus;

            if (mundusTemp > mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost)
                mundusTemp = mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost;

            if (MundusBar != null)
                MundusBar.DrawSelf(spriteBatch, (int)mundusTemp, (int)(mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost));

            if (OverflowMundusBar != null && mysticPlayer.Mundus > mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost)
                OverflowMundusBar.DrawSelf(spriteBatch, Math.Min((int)(mysticPlayer.Mundus - mysticPlayer.MundusMax - mysticPlayer.MundusMaxPermaBoost), (int)(mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost)), (int)(mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost), true);

            float visTemp = mysticPlayer.Vis;

            if (visTemp > mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost)
                visTemp = mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost;

            if (VisBar != null)
                VisBar.DrawSelf(spriteBatch, (int)visTemp, (int)(mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost));

            if (OverflowVisBar != null && mysticPlayer.Vis > mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost)
                OverflowVisBar.DrawSelf(spriteBatch, Math.Min((int)(mysticPlayer.Vis - mysticPlayer.VisMax - mysticPlayer.VisMaxPermaBoost), (int)(mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost)), (int)(mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost), true);
            /*
            if (MysticBurstBar != null && mysticPlayer.MysticSwitchCool > 0 && mysticBurstCooldownMax >= mysticPlayer.MysticSwitchCool)
                MysticBurstBar.DrawSelf(spriteBatch, mysticPlayer.MysticSwitchCool, mysticBurstCooldownMax);
            */
            UpdateHover(spriteBatch);
        }

        public static void CyclePositions(MysticMode setPosition)
        {
            rotationGoal += (float)(2f / 3 * Math.PI);
            targetPosition = setPosition;
            //switch (targetPosition)
            //{
            //    case MysticMode.Destruction:
            //        rotationGoal = 6f / 3f * (float)Math.PI;
            //        break;
            //    case MysticMode.Illusion:
            //        rotationGoal = 4f / 3f * (float)Math.PI;
            //        break;
            //    default:
            //        rotationGoal = 8f / 3f * (float)Math.PI;
            //        break;
            //}
        }

        public static void Update()
        {
            UpdateType();
            UpdateIdle();
            if (MysticUIType < 2)
                UpdateRotations();
            else
                UpdateHeights();
            UpdatePositions();
            UpdateMysticBurst();

        }

        private static void UpdateIdle()
        {
            UpdateTop();
            UpdateMid();
            UpdateBot();
        }

        private static void UpdateType()
        {
            switch (MysticUIType)
            {
                case 1: //TOP CIRCLE
                    CenterPos = new Vector2(Main.screenWidth / 2, 100 / Main.UIScale);
                    break;
                case 2: //TOP STACK 
                    CenterPos = new Vector2(Main.screenWidth / 2, 30 / Main.UIScale);
                    break;
                default: //CENTER
                    CenterPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                    break;
            }
        }

        private static void UpdateRotations()
        {
            float fullTurn = (float)(Math.PI * 2.0);
            float rotationStep = (float)(Math.PI / 10.0);

            if (!Animated)
            {
                rotation = rotationGoal;
                position = targetPosition;
            }
            if (rotation < rotationGoal)
                rotation = Math.Min(rotation + rotationStep, rotationGoal);
            else
            {
                rotation = rotationGoal;
                position = targetPosition;
            }
            if (rotationGoal >= fullTurn && rotation > fullTurn)
            {
                rotationGoal -= fullTurn;
                rotation -= fullTurn;
            }
            float offsetX = -.5f;
            float offsetY = -.3f;
            TopPosBase.X = ((CenterPos.X + ((float)Math.Cos(rotation + (3f / 2f * Math.PI)) * 100)) / Main.UIScale) + (BAR_WIDTH * offsetX);
            TopPosBase.Y = ((CenterPos.Y + ((float)Math.Sin(rotation + (3f / 2f * Math.PI)) * 100) + 25) / Main.UIScale) + (BAR_WIDTH * offsetY);
            MidPosBase.X = ((CenterPos.X + ((float)Math.Cos(rotation + (5f / 6f * Math.PI)) * 100)) / Main.UIScale) + (BAR_WIDTH * offsetX);
            MidPosBase.Y = ((CenterPos.Y + ((float)Math.Sin(rotation + (5f / 6f * Math.PI)) * 100) + 25) / Main.UIScale) + (BAR_WIDTH * offsetY);
            BotPosBase.X = ((CenterPos.X + ((float)Math.Cos(rotation + (1f / 6f * Math.PI)) * 100)) / Main.UIScale) + (BAR_WIDTH * offsetX);
            BotPosBase.Y = ((CenterPos.Y + ((float)Math.Sin(rotation + (1f / 6f * Math.PI)) * 100) + 25) / Main.UIScale) + (BAR_WIDTH * offsetY);
            MysticBurstPos.X = (CenterPos.X - (MysticBurstBGTexture.Width() / 2)) / Main.UIScale;
            MysticBurstPos.Y = (CenterPos.Y + 125) / Main.UIScale;
        }

        private static void UpdateHeights()
        {
            if (rotation < rotationGoal)
                rotation += (float)(Math.PI / 10f);
            else
            {
                rotation = rotationGoal;
                position = targetPosition;
            }
            if (rotationGoal > (float)Math.PI * 2 && rotation > (float)Math.PI * 2)
            {
                rotationGoal -= (float)Math.PI * 2;
                rotation -= (float)Math.PI * 2;
            }
            TopPosBase.X = (CenterPos.X - (LuxBGTexture.Width() / 2)) / Main.UIScale;
            TopPosBase.Y = (CenterPos.Y + (LuxBGTexture.Height() / 2) + ((float)Math.Sin(rotation - (.333f * Math.PI)) * LuxBGTexture.Height() * Main.UIScale) + 25) / Main.UIScale;
            MidPosBase.X = (CenterPos.X - (LuxBGTexture.Width() / 2)) / Main.UIScale;
            MidPosBase.Y = (CenterPos.Y + (LuxBGTexture.Height() / 2) + ((float)Math.Sin(rotation - (1f * Math.PI)) * LuxBGTexture.Height() * Main.UIScale) + 25) / Main.UIScale;
            BotPosBase.X = (CenterPos.X - (LuxBGTexture.Width() / 2)) / Main.UIScale;
            BotPosBase.Y = (CenterPos.Y + (LuxBGTexture.Height() / 2) + ((float)Math.Sin(rotation - (1.667f * Math.PI)) * LuxBGTexture.Height() * Main.UIScale) + 25) / Main.UIScale;
        }

        private static void UpdateTop()
        {
            if (topTheta == -100)
                topTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
            topTheta += ((float)Math.PI / 90) + (float)(Math.PI / 120 * Main.rand.NextDouble());
            if (topTheta > (float)Math.PI * 2)
                topTheta -= (float)Math.PI * 2;
            if (Main.rand.Next(20) == 0)
            {
                if (Main.rand.Next(2) == 0)
                    topMag *= .9f;
                else
                    topMag *= 1.1f;
            }
            if (topMag < 2)
                topMag = 2;
            if (topMag > 5)
                topMag = 5;
            TopPos.X = TopPosBase.X + (Animated ? topMag * (float)Math.Cos(topTheta) : 0);
            TopPos.Y = TopPosBase.Y + (Animated ? topMag * (float)Math.Sin(topTheta * 2) : 0);
        }

        private static void UpdateMid()
        {
            if (midTheta == -100)
                midTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
            midTheta += ((float)Math.PI / 90) + (float)(Math.PI / 120 * Main.rand.NextDouble());
            if (midTheta > (float)Math.PI * 2)
                midTheta -= (float)Math.PI * 2;
            if (Main.rand.Next(20) == 0)
            {
                if (Main.rand.Next(2) == 0)
                    midMag *= .9f;
                else
                    midMag *= 1.1f;
            }
            if (midMag < 2)
                midMag = 2;
            if (midMag > 5)
                midMag = 5;
            MidPos.X = MidPosBase.X + (Animated ? midMag * (float)Math.Cos(midTheta) : 0);
            MidPos.Y = MidPosBase.Y + (Animated ? midMag * (float)Math.Sin(midTheta * 2) : 0);
        }

        private static void UpdateBot()
        {
            if (botTheta == -100)
                botTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
            botTheta += ((float)Math.PI / 90) + (float)(Math.PI / 120 * Main.rand.NextDouble());
            if (botTheta > (float)Math.PI * 2)
                botTheta -= (float)Math.PI * 2;
            if (Main.rand.Next(20) == 0)
            {
                if (Main.rand.Next(2) == 0)
                    botMag *= .9f;
                else
                    botMag *= 1.1f;
            }
            if (botMag < 2)
                botMag = 2;
            if (botMag > 5)
                botMag = 5;
            BotPos.X = BotPosBase.X + (Animated ? botMag * (float)Math.Cos(botTheta) : 0);
            BotPos.Y = BotPosBase.Y + (Animated ? botMag * (float)Math.Sin(botTheta * 2) : 0);
        }

        private static void UpdatePositions()
        {
            LuxBar.Position = TopPos;
            OverflowLuxBar.Position = TopPos;
            VisBar.Position = MidPos;
            OverflowVisBar.Position = MidPos;
            MundusBar.Position = BotPos;
            OverflowMundusBar.Position = BotPos;
        }

        private static void UpdateMysticBurst()
        {
            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();
            if (mysticBurstCooldownMax < mysticPlayer.MysticSwitchCool)
                mysticBurstCooldownMax = mysticPlayer.MysticSwitchCool;
            if (mysticPlayer.MysticSwitchCool == 0 && mysticBurstCooldownMax != 0)
            {
                mysticBurstCooldownMax = 0;
                SoundEngine.PlaySound(SoundID.MaxMana, Main.LocalPlayer.position);
            }
            MysticBurstBar.Position = MysticBurstPos;
        }

        private static void UpdateHover(SpriteBatch spriteBatch)
        {
            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();


            Vector2 drawPos = Main.MouseWorld - Main.screenPosition;

            drawPos.X += 20;
            drawPos.Y += 8;
            int widthMod = 12;
            int heightMod = 8;

            float posX = Main.MouseWorld.X - Main.screenPosition.X;
            float posY = Main.MouseWorld.Y - Main.screenPosition.Y;
            if (posX > TopPosBase.X - widthMod && posX < TopPosBase.X + LuxBGTexture.Width() + widthMod && posY > TopPosBase.Y - heightMod && posY < TopPosBase.Y + LuxBGTexture.Height() + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Lux).ToString() + "/" + Math.Round(mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost).ToString() + " (" + Math.Round((mysticPlayer.LuxMax + mysticPlayer.LuxMaxPermaBoost) * mysticPlayer.LuxOverflow * mysticPlayer.GlobalOverflow).ToString() + ") Lux", drawPos, Color.White);
            if (posX > BotPosBase.X - widthMod && posX < BotPosBase.X + LuxBGTexture.Width() + widthMod && posY > BotPosBase.Y - heightMod && posY < BotPosBase.Y + LuxBGTexture.Height() + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Mundus).ToString() + "/" + Math.Round(mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost).ToString() + " (" + Math.Round((mysticPlayer.MundusMax + mysticPlayer.MundusMaxPermaBoost) * mysticPlayer.MundusOverflow * mysticPlayer.GlobalOverflow).ToString() + ") Mundus", drawPos, Color.White);
            if (posX > MidPosBase.X - widthMod && posX < MidPosBase.X + LuxBGTexture.Width() + widthMod && posY > MidPosBase.Y - heightMod && posY < MidPosBase.Y + LuxBGTexture.Height() + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Vis).ToString() + "/" + Math.Round(mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost).ToString() + " (" + Math.Round((mysticPlayer.VisMax + mysticPlayer.VisMaxPermaBoost) * mysticPlayer.VisOverflow * mysticPlayer.GlobalOverflow).ToString() + ") Vis", drawPos, Color.White);
        }
    }
}

public class SpriteProgressBar
{
    public Asset<Texture2D> MainTexture = TextureAssets.MagicPixel;
    public Asset<Texture2D> BarTexture = TextureAssets.MagicPixel;
    public Asset<Texture2D> DecoTexture = TextureAssets.MagicPixel;

    public Vector2 Position;
    public Vector2 PosOffset;

    public float Alpha = 1f;
    public int FrameCount = 0;
    public int FrameTimer = 0;
    public int CurrentFrame;
    public int FrameSpeed = 4;

    public Color[] Colors = new Color[] { Color.White, Color.White };

    public float[] Scales = new float[] { 1f, 1f };
    public int[] Widths = new int[] { 0, 0 };
    public int[] Heights = new int[] { 0, 0 };
    public int MaxVal;
    public int CurVal;

    public SpriteProgressBar(Asset<Texture2D> mainTexture, Asset<Texture2D> barTexture, Asset<Texture2D> decoTexture, Vector2 pos, Vector2 offset = new Vector2(), int frameCount = 1, Color c1 = new Color(), Color c2 = new Color())
    {
        MainTexture = mainTexture;
        BarTexture = barTexture;
        DecoTexture = decoTexture;
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

    public void DrawSelf(SpriteBatch spriteBatch)
    {
        #region Drawing
        int frameHeight = FrameCount > 1 ? MainTexture.Height() / FrameCount : MainTexture.Height();
        Rectangle rect1 = new Rectangle(0, CurrentFrame * frameHeight, MainTexture.Width(), frameHeight);
        if (Widths[0] != 0)
            rect1 = new Rectangle(0, 0, Widths[0], Heights[0]);
        Rectangle rect2 = new Rectangle(0, 0, CalcLength(CurVal, MaxVal, BarTexture.Width()), BarTexture.Height());
        if (Widths[1] != 0)
            rect2 = new Rectangle(0, 0, CalcLength(CurVal, MaxVal, Widths[1]), Heights[1]);
        spriteBatch.Draw
            (
                MainTexture.Value,
                new Vector2((int)Position.X, (int)Position.Y),
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
                BarTexture.Value,
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
            DecoTexture.Value,
            new Vector2((int)Position.X, (int)Position.Y),
            rect1,
            Colors[0] * Alpha,
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
    }
    public void DrawSelf(SpriteBatch spriteBatch, int currentValue, int maxValue)
    {
        DrawSelf(spriteBatch, currentValue, maxValue, false);
    }
    public void DrawSelf(SpriteBatch spriteBatch, int currentValue, int maxValue, bool flipped)
    {

        //ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Trying to draw self"), Color.White);
        #region Drawing
        int frameHeight = FrameCount > 1 ? MainTexture.Height() / FrameCount : MainTexture.Height();

        Rectangle rect1 = new Rectangle(0, CurrentFrame * frameHeight, MainTexture.Width(), frameHeight);
        if (Widths[0] != 0)
            rect1 = new Rectangle(0, 0, Widths[0], Heights[0]);

        Rectangle rect2 = new Rectangle(0, 0, CalcLength(currentValue, maxValue, BarTexture.Width()), BarTexture.Height());
        if (Widths[1] != 0)
            rect2 = new Rectangle(0, 0, CalcLength(currentValue, maxValue, Widths[1]), Heights[1]);
        if (flipped)
            rect2.X = BarTexture.Width() - rect2.Width;

        spriteBatch.Draw
            (
                MainTexture.Value,
                new Vector2((int)Position.X, (int)Position.Y),
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
                BarTexture.Value,
                new Vector2((int)Position.X + (flipped ? BarTexture.Width() - rect2.Width : 0), (int)Position.Y) + new Vector2((int)PosOffset.X, (int)PosOffset.Y),
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
                DecoTexture.Value,
                new Vector2((int)Position.X, (int)Position.Y),
                rect1,
                Colors[0] * Alpha,
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
    }
    public int CalcLength(int val, int maxVal, int TextureWidth)
    {
        return val * TextureWidth / maxVal;
    }
}
