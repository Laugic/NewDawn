using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Players;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace NewDawn.UI.NewAttempts.Mystic
{
    public class MysticUI : UIState
    {
        //Thanks for the help, DayOrk <3
        private const int BAR_WIDTH = 96;

        public static bool visible = true;
        public static SpriteProgressBar LuxBar;
        public static SpriteProgressBar OverflowLuxBar;
        public static Texture2D LuxBGTexture;
        public static Texture2D LuxBarTexture;
        public static Texture2D LuxFGTexture;
        public static SpriteProgressBar MundusBar;
        public static SpriteProgressBar OverflowMundusBar;
        public static Texture2D MundusBGTexture;
        public static Texture2D MundusBarTexture;
        public static Texture2D MundusFGTexture;
        public static SpriteProgressBar VisBar;
        public static SpriteProgressBar OverflowVisBar;
        public static Texture2D VisBGTexture;
        public static Texture2D VisBarTexture;
        public static Texture2D VisFGTexture;
        public static Texture2D OverflowBGTexture;
        public static Texture2D OverflowBarTexture;
        public static Texture2D OverflowFGTexture;
        public static Texture2D LuxFGOverflowTexture;
        public static Texture2D VisFGOverflowTexture;
        public static Texture2D MundusOverflowTexture;
        public static Texture2D MundusFGOverflowTexture;
        public static SpriteProgressBar MysticBurstBar;
        public static Texture2D MysticBurstBGTexture;
        public static Texture2D MysticBurstBarTexture;

        private Vector2 TopPosBase = new Vector2(((Main.screenWidth / 2) - (BAR_WIDTH / 2) + ((float)Math.Cos(3f / 2f * Math.PI) * 100)) / Main.UIScale, ((Main.screenHeight / 2) - (BAR_WIDTH / 2) + ((float)Math.Sin(3f / 2f * Math.PI) * 100) + 25) / Main.UIScale);
        private Vector2 MidPosBase = new Vector2(((Main.screenWidth / 2) - (BAR_WIDTH / 2) + ((float)Math.Cos(5f / 6f * Math.PI) * 100)) / Main.UIScale, ((Main.screenHeight / 2) - (BAR_WIDTH / 2) + ((float)Math.Sin(5f / 6f * Math.PI) * 100) + 25) / Main.UIScale);
        private Vector2 BotPosBase = new Vector2(((Main.screenWidth / 2) - (BAR_WIDTH / 2) + ((float)Math.Cos(1f / 6f * Math.PI) * 100)) / Main.UIScale, ((Main.screenHeight / 2) - (BAR_WIDTH / 2) + ((float)Math.Sin(1f / 6f * Math.PI) * 100) + 25) / Main.UIScale);
        private Vector2 TopPos = new Vector2(0, 0);
        private Vector2 MidPos = new Vector2(0, 0);
        private Vector2 BotPos = new Vector2(0, 0);
        private Vector2 MysticBurstPos = new Vector2(0, 0);

        int position = 1;
        float topTheta = -100;
        float midTheta = -100;
        float botTheta = -100;
        float topMag = 0;
        float midMag = 0;
        float botMag = 0;
        float magMax = 5;
        float rotation = (float)Math.PI * 2f;
        float rotationGoal = (float)Math.PI * 2;

        int targetPosition = 1;
        int mysticBurstCooldownMax = 0;

        //CONFIGURABLES
        public static int MysticUIType { get; set; }
        public static bool Animated { get; set; }
        public static bool SoundOn { get; set; }

        //CONFIG RELATED VALUES
        private Vector2 CenterPos = new Vector2(0, 0);
        Vector2 CENTER = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
        Vector2 TOPCIRCLE = new Vector2(Main.screenWidth / 2, 100 / Main.UIScale);
        Vector2 TOPSTACK = new Vector2(Main.screenWidth / 2, 30 / Main.UIScale);

        static MysticUI()
        {
            visible = true;
        }

        public void Load()
        {
            LuxBGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/LuxBG");
            LuxBarTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/LuxBar");
            LuxFGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/LuxFG");
            MundusBGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/MundusBG");
            MundusBarTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/MundusBar");
            MundusFGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/MundusFG");
            VisBGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/VisBG");
            VisBarTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/VisBar");
            VisFGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/VisFG");
            OverflowBGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/OverflowBG");
            OverflowBarTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/OverflowBar");
            OverflowFGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/OverflowFG");
            LuxFGOverflowTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/LuxFGOverflow");
            VisFGOverflowTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/VisFGOverflow");
            MundusOverflowTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/MundusOverflow");
            MundusFGOverflowTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/MundusFGOverflow");
            MysticBurstBGTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/MysticBurstBG");
            MysticBurstBarTexture = (Texture2D)ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/MysticBurstBar");

            LuxBar = new SpriteProgressBar
            (
                "NewDawn/UI/Temple/SpiritBG",
                "NewDawn/UI/Temple/SpiritBar",
                "NewDawn/UI/Temple/SpiritFG",
                //LuxBGTexture,
                //LuxBarTexture,
                //LuxFGTexture,
                TopPos,
                new Vector2(0, 0)
            );

            OverflowLuxBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                LuxFGOverflowTexture,
                TopPos,
                new Vector2(OverflowBarTexture.Width, 0)
            );

            MundusBar = new SpriteProgressBar
            (
                MundusBGTexture,
                MundusBarTexture,
                MundusFGTexture,
                MidPos,
                new Vector2(0, 0)
            );

            OverflowMundusBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                MundusFGOverflowTexture,
                MidPos,
                new Vector2(OverflowBarTexture.Width, 0)
            );

            VisBar = new SpriteProgressBar
            (
                VisBGTexture,
                VisBarTexture,
                VisFGTexture,
                BotPos,
                new Vector2(0, 0)
            );

            OverflowVisBar = new SpriteProgressBar
            (
                OverflowBGTexture,
                OverflowBarTexture,
                VisFGOverflowTexture,
                BotPos,
                new Vector2(OverflowBarTexture.Width, 0)
            );

            MysticBurstBar = new SpriteProgressBar
            (
                MysticBurstBGTexture,
                MysticBurstBarTexture,
                OverflowFGTexture,
                MysticBurstPos,
                new Vector2(0, 0)
            );
            return;
            Append(LuxBar);
            Append(MundusBar);
            Append(VisBar);
            Append(OverflowLuxBar);
            Append(OverflowMundusBar);
            Append(OverflowVisBar);
        }

        public void Unload()
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();

            visible = false;// mysticPlayer.IsHoldingMystic();
            return;
            ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Trying to draw UI"), Color.White);
            if (LuxBar != null)
            {

                float lux = Math.Min(mysticPlayer.Lux, mysticPlayer.GetMaxLux()) / mysticPlayer.GetMaxLux();
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Lux: " + lux), Color.White);
                LuxBar.DrawBar(spriteBatch, lux);
            }

            if (OverflowLuxBar != null && mysticPlayer.Lux > mysticPlayer.GetMaxLux())
                OverflowLuxBar.DrawBar(spriteBatch, Math.Min((int)(mysticPlayer.Lux - mysticPlayer.GetMaxLux()), (int)mysticPlayer.GetMaxLux()), (int)mysticPlayer.GetMaxLux(), true);

            if (MundusBar != null)
                MundusBar.DrawBar(spriteBatch, (int)Math.Min(mysticPlayer.Mundus, mysticPlayer.GetMaxMundus()), (int)mysticPlayer.GetMaxMundus());

            if (OverflowMundusBar != null && mysticPlayer.Mundus > mysticPlayer.GetMaxMundus())
                OverflowMundusBar.DrawBar(spriteBatch, Math.Min((int)(mysticPlayer.Mundus - mysticPlayer.GetMaxMundus()), (int)mysticPlayer.GetMaxMundus()), (int)mysticPlayer.GetMaxMundus(), true);

            if (VisBar != null)
                VisBar.DrawBar(spriteBatch, (int)Math.Min(mysticPlayer.Vis, mysticPlayer.GetMaxVis()), (int)mysticPlayer.GetMaxVis());

            if (OverflowVisBar != null && mysticPlayer.Vis > mysticPlayer.GetMaxVis())
                OverflowVisBar.DrawBar(spriteBatch, Math.Min((int)(mysticPlayer.Vis - mysticPlayer.GetMaxVis()), (int)mysticPlayer.GetMaxVis()), (int)mysticPlayer.GetMaxVis(), true);
            /*
            if (MysticBurstBar != null && mysticPlayer.MysticSwitchCool > 0 && mysticBurstCooldownMax >= mysticPlayer.MysticSwitchCool)
                MysticBurstBar.DrawBar(spriteBatch, mysticPlayer.MysticSwitchCool, mysticBurstCooldownMax);
            */
            UpdateHover(spriteBatch);
        }

        public void CyclePositions(MysticPlayer.MysticMode mode)
        {
            rotationGoal += (float)(2f / 3 * Math.PI);
            switch (mode)
            {
                case MysticPlayer.MysticMode.Destruction:
                    rotationGoal = 6f / 3f * (float)Math.PI;
                    targetPosition = 1;
                    break;
                case MysticPlayer.MysticMode.Illusion:
                    rotationGoal = 4f / 3f * (float)Math.PI;
                    targetPosition = 2;
                    break;
                case MysticPlayer.MysticMode.Conjuration:
                    rotationGoal = 8f / 3f * (float)Math.PI;
                    targetPosition = 3;
                    break;
            }
        }

        public void Update()
        {
            UpdateIdle();
            UpdateType();
            if (MysticUIType < 2)
                UpdateRotations();
            else
                UpdateHeights();
            UpdatePositions();
        }

        private void UpdateIdle()
        {
            UpdateTop();
            UpdateMid();
            UpdateBot();
        }

        private void UpdateType()
        {
            switch (MysticUIType)
            {
                case 1:
                    CenterPos = TOPCIRCLE;
                    break;
                case 2:
                    CenterPos = TOPSTACK;
                    break;
                default:
                    CenterPos = CENTER;
                    break;
            }
        }

        private void UpdateRotations()
        {
            if (!Animated)
            {
                rotation = rotationGoal;
                position = targetPosition;
            }
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
            TopPosBase.X = (CenterPos.X - (LuxBGTexture.Width / 2) + ((float)Math.Cos(rotation + (3f / 2f * Math.PI)) * 100)) / Main.UIScale;
            TopPosBase.Y = (CenterPos.Y - (LuxBGTexture.Height / 2) + ((float)Math.Sin(rotation + (3f / 2f * Math.PI)) * 100) + 25) / Main.UIScale;
            MidPosBase.X = (CenterPos.X - (LuxBGTexture.Width / 2) + ((float)Math.Cos(rotation + (5f / 6f * Math.PI)) * 100)) / Main.UIScale;
            MidPosBase.Y = (CenterPos.Y - (LuxBGTexture.Height / 2) + ((float)Math.Sin(rotation + (5f / 6f * Math.PI)) * 100) + 25) / Main.UIScale;
            BotPosBase.X = (CenterPos.X - (LuxBGTexture.Width / 2) + ((float)Math.Cos(rotation + (1f / 6f * Math.PI)) * 100)) / Main.UIScale;
            BotPosBase.Y = (CenterPos.Y - (LuxBGTexture.Height / 2) + ((float)Math.Sin(rotation + (1f / 6f * Math.PI)) * 100) + 25) / Main.UIScale;
            MysticBurstPos.X = (CenterPos.X - (MysticBurstBGTexture.Width / 2)) / Main.UIScale;
            MysticBurstPos.Y = (CenterPos.Y + 125) / Main.UIScale;
        }

        private void UpdateHeights()
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
            TopPosBase.X = (CenterPos.X - (LuxBGTexture.Width / 2)) / Main.UIScale;
            TopPosBase.Y = (CenterPos.Y + (LuxBGTexture.Height / 2) + ((float)Math.Sin(rotation - (.333f * Math.PI)) * LuxBGTexture.Height * Main.UIScale) + 25) / Main.UIScale;
            MidPosBase.X = (CenterPos.X - (LuxBGTexture.Width / 2)) / Main.UIScale;
            MidPosBase.Y = (CenterPos.Y + (LuxBGTexture.Height / 2) + ((float)Math.Sin(rotation - (1f * Math.PI)) * LuxBGTexture.Height * Main.UIScale) + 25) / Main.UIScale;
            BotPosBase.X = (CenterPos.X - (LuxBGTexture.Width / 2)) / Main.UIScale;
            BotPosBase.Y = (CenterPos.Y + (LuxBGTexture.Height / 2) + ((float)Math.Sin(rotation - (1.667f * Math.PI)) * LuxBGTexture.Height * Main.UIScale) + 25) / Main.UIScale;
        }

        private void UpdateTop()
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

        private void UpdateMid()
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

        private void UpdateBot()
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

        private void UpdatePositions()
        {
            LuxBar.Position = TopPos;
            OverflowLuxBar.Position = TopPos;
            VisBar.Position = BotPos;
            OverflowVisBar.Position = BotPos;
            MundusBar.Position = MidPos;
            OverflowMundusBar.Position = MidPos;
        }

        private void UpdateHover(SpriteBatch spriteBatch)
        {
            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();


            Vector2 drawPos = Main.MouseWorld - Main.screenPosition;

            drawPos.X += 20;
            drawPos.Y += 8;
            int widthMod = 12;
            int heightMod = 8;

            float posX = Main.MouseWorld.X - Main.screenPosition.X;
            float posY = Main.MouseWorld.Y - Main.screenPosition.Y;
            if (posX > TopPosBase.X - widthMod && posX < TopPosBase.X + LuxBGTexture.Width + widthMod && posY > TopPosBase.Y - heightMod && posY < TopPosBase.Y + LuxBGTexture.Height + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Lux).ToString() + "/" + Math.Round((double)mysticPlayer.GetMaxLux()).ToString() + " (" + Math.Round(mysticPlayer.GetMaxLux() * mysticPlayer.LuxOverflow * mysticPlayer.OverflowCapacity).ToString() + ") Lux", drawPos, Color.White);
            if (posX > BotPosBase.X - widthMod && posX < BotPosBase.X + LuxBGTexture.Width + widthMod && posY > BotPosBase.Y - heightMod && posY < BotPosBase.Y + LuxBGTexture.Height + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Vis).ToString() + "/" + Math.Round((double)mysticPlayer.GetMaxVis()).ToString() + " (" + Math.Round(mysticPlayer.GetMaxVis() * mysticPlayer.VisOverflow * mysticPlayer.OverflowCapacity).ToString() + ") Vis", drawPos, Color.White);
            if (posX > MidPosBase.X - widthMod && posX < MidPosBase.X + LuxBGTexture.Width + widthMod && posY > MidPosBase.Y - heightMod && posY < MidPosBase.Y + LuxBGTexture.Height + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, Math.Round(mysticPlayer.Mundus).ToString() + "/" + Math.Round((double)mysticPlayer.GetMaxMundus()).ToString() + " (" + Math.Round(mysticPlayer.GetMaxMundus() * mysticPlayer.MundusOverflow * mysticPlayer.OverflowCapacity).ToString() + ") Mundus", drawPos, Color.White);
        }
    }


    public class MysticUISystem : ModSystem
    {
        private UserInterface MysticUserInterface;

        public static MysticUI MysticUIBars;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MysticUIBars = new();
                MysticUIBars.Load();
                MysticUserInterface = new();
                MysticUserInterface.SetState(MysticUIBars);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            MysticUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "New Dawn: Spirit Bar",
                    delegate
                    {
                        MysticUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
/*
public class SpriteProgressBar : UIElement
{
    public Texture2D MainTexture = TextureAssets.MagicPixel.Value;
    public Texture2D BarTexture = TextureAssets.MagicPixel.Value;
    public Texture2D DecoTexture = TextureAssets.MagicPixel.Value;

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

    public SpriteProgressBar(Texture2D mainTexture, Texture2D barTexture, Texture2D decoTexture, Vector2 pos, Vector2 offset = new Vector2(), int frameCount = 1, Color c1 = new Color(), Color c2 = new Color())
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

    public void DrawBar(SpriteBatch spriteBatch)
    {
        #region Drawing
        int frameHeight = FrameCount > 1 ? MainTexture.Height / FrameCount : MainTexture.Height;
        Rectangle rect1 = new Rectangle(0, CurrentFrame * frameHeight, MainTexture.Width, frameHeight);
        if (Widths[0] != 0)
            rect1 = new Rectangle(0, 0, Widths[0], Heights[0]);
        Rectangle rect2 = new Rectangle(0, 0, CalcLength(CurVal, MaxVal, BarTexture.Width), BarTexture.Height);
        if (Widths[1] != 0)
            rect2 = new Rectangle(0, 0, CalcLength(CurVal, MaxVal, Widths[1]), Heights[1]);
        spriteBatch.Draw
            (
                MainTexture,
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
            DecoTexture,
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
    public void DrawBar(SpriteBatch spriteBatch, int currentValue, int maxValue, bool flipped = false)
    {
        #region Drawing
        int frameHeight = FrameCount > 1 ? MainTexture.Height / FrameCount : MainTexture.Height;

        Rectangle rect1 = new Rectangle(0, CurrentFrame * frameHeight, MainTexture.Width, frameHeight);
        if (Widths[0] != 0)
            rect1 = new Rectangle(0, 0, Widths[0], Heights[0]);

        Rectangle rect2 = new Rectangle(0, 0, CalcLength(currentValue, maxValue, BarTexture.Width), BarTexture.Height);
        if (Widths[1] != 0)
            rect2 = new Rectangle(0, 0, CalcLength(currentValue, maxValue, Widths[1]), Heights[1]);
        if (flipped)
            rect2.X = BarTexture.Width - rect2.Width;

        spriteBatch.Draw
            (
                MainTexture,
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
                DecoTexture,
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
}*/
