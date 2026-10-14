//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using NewDawn.Items.Weapons;
//using NewDawn.Players;
//using System;
//using System.Collections.Generic;
//using Terraria;
//using Terraria.GameContent.UI.Elements;
//using Terraria.Localization;
//using Terraria.ModLoader;
//using Terraria.UI;

//namespace NewDawn.UI.NewAttempts.Mystic
//{
//    public class NewDawnMysticUI : UIState
//    {
//        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
//        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
//        private UIText text;
//        //private UIElement area;

//        private UIImage LuxBarBG, LuxBarBar, LuxBarFG, VisBarBG, VisBarBar, VisBarFG, MundusBarBG, MundusBarBar, MundusBarFG;
//        private UIBar LuxBar, VisBar, MundusBar;
//        private const int BAR_WIDTH = 96;
//        private static Vector2 BAR = new Vector2(96, 32);
//        private Vector2 TopPosBase = new Vector2(Main.screenWidth / 2, Main.screenHeight * .4f) + (BAR / 2);
//        private Vector2 MidPosBase = new Vector2(Main.screenWidth / 2, Main.screenHeight * .4f) + (BAR / 2);
//        private Vector2 BotPosBase = new Vector2(Main.screenWidth / 2, Main.screenHeight * .4f) + (BAR / 2);
//        private Vector2 TopPos = Vector2.Zero, MidPos = Vector2.Zero, BotPos = Vector2.Zero, CenterPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);

//        int position = 1;
//        float topTheta = -100;
//        float midTheta = -100;
//        float botTheta = -100;
//        float topMag = 0;
//        float midMag = 0;
//        float botMag = 0;
//        float magMax = 5;
//        static float rotation = (float)Math.PI * 2f;
//        static float rotationGoal = (float)Math.PI * 2;
//        static int targetPosition = 1;
//        public static bool Animated { get; set; } = true;
//        public override void OnInitialize()
//        {
//            return;
//            LuxBarBG = new UIImage(GetTexture("LuxBG"));
//            LuxBarBar = new UIImage(GetTexture("LuxBar"));
//            LuxBarFG = new UIImage(GetTexture("LuxFG"));
//            LuxBar = new UIBar(LuxBarBG, LuxBarBar, LuxBarFG, TopPosBase);
//            LuxBar.Initialize();

//            VisBarBG = new UIImage(GetTexture("VisBG"));
//            VisBarBar = new UIImage(GetTexture("VisBar"));
//            VisBarFG = new UIImage(GetTexture("VisFG"));
//            VisBar = new UIBar(VisBarBG, VisBarBar, VisBarFG, MidPosBase);
//            VisBar.Initialize();

//            MundusBarBG = new UIImage(GetTexture("MundusBG"));
//            MundusBarBar = new UIImage(GetTexture("MundusBar"));
//            MundusBarFG = new UIImage(GetTexture("MundusFG"));
//            MundusBar = new UIBar(MundusBarBG, MundusBarBar, MundusBarFG, BotPosBase);
//            MundusBar.Initialize();

//            Append(LuxBarBG);
//            Append(LuxBarBar);
//            Append(LuxBarFG);
//            Append(VisBarBG);
//            Append(VisBarBar);
//            Append(VisBarFG);
//            Append(MundusBarBG);
//            Append(MundusBarBar);
//            Append(MundusBarFG);
//        }

//        private Texture2D GetTexture(string textureName)
//        {
//            return ModContent.Request<Texture2D>(GetType().Namespace.Replace('.', '/') + "/" + textureName).Value;
//        }

//        public override void Draw(SpriteBatch spriteBatch)
//        {
//            // This prevents drawing unless we are using a MysticWeapon
//            if (Main.LocalPlayer.HeldItem.ModItem is not MysticItem)
//                return;

//            base.Draw(spriteBatch);
//        }

//        // Here we draw our UI
//        protected override void DrawSelf(SpriteBatch spriteBatch)
//        {
//            base.DrawSelf(spriteBatch);
//            var modPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();
//            // Calculate quotient
//            float quotient = (float)modPlayer.Lux / modPlayer.GetMaxLux(); // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
//            quotient = Utils.Clamp(quotient, 0f, 1f);
//        }

//        public void CyclePositions(MysticPlayer.MysticMode mode)
//        {
//            rotationGoal += (float)(2f / 3 * Math.PI);
//            switch (mode)
//            {
//                case MysticPlayer.MysticMode.Destruction:
//                    rotationGoal = 6f / 3f * (float)Math.PI;
//                    targetPosition = 1;
//                    break;
//                case MysticPlayer.MysticMode.Conjuration:
//                    rotationGoal = 4f / 3f * (float)Math.PI;
//                    targetPosition = 2;
//                    break;
//                case MysticPlayer.MysticMode.Illusion:
//                    rotationGoal = 8f / 3f * (float)Math.PI;
//                    targetPosition = 3;
//                    break;
//            }
//        }

//        public override void Update(GameTime gameTime)
//        {
//            if (Main.LocalPlayer.HeldItem.ModItem is not MysticItem)
//                return;

//            // Setting the text per tick to update and show our resource values.
//            //text.SetText(NewDawnMysticUISystem.ExampleResourceText.Format(modPlayer.Lux, modPlayer.GetMaxLux()));

//            UpdateIdle();
//            UpdateRotations();
//            UpdatePositions();
//            UpdateImages();
//            base.Update(gameTime);
//        }

//        private void UpdateImages()
//        {
//            LuxBar.UpdateImages(LuxBarBG, LuxBarBar, LuxBarFG);
//            VisBar.UpdateImages(VisBarBG, VisBarBar, VisBarFG);
//            MundusBar.UpdateImages(MundusBarBG, MundusBarBar, MundusBarFG);
//        }

//        private void UpdateIdle()
//        {
//            UpdateTop();
//            UpdateMid();
//            UpdateBot();
//            var mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();
//            float lux = Math.Min(mysticPlayer.Lux, mysticPlayer.GetMaxLux()) / mysticPlayer.GetMaxLux();
//            float vis = Math.Min(mysticPlayer.Vis, mysticPlayer.GetMaxVis()) / mysticPlayer.GetMaxVis();
//            float mundus = Math.Min(mysticPlayer.Mundus, mysticPlayer.GetMaxMundus()) / mysticPlayer.GetMaxMundus();
//            LuxBar.Update(lux * LuxBarBar.MaxWidth.Pixels);
//            VisBar.Update(vis * VisBarBar.MaxWidth.Pixels);
//            MundusBar.Update(mundus * MundusBarBar.MaxWidth.Pixels);
//        }
//        private void UpdateRotations()
//        {
//            if (!Animated)
//            {
//                rotation = rotationGoal;
//                position = targetPosition;
//            }
//            if (rotation < rotationGoal)
//                rotation += (float)(Math.PI / 10f);
//            else
//            {
//                rotation = rotationGoal;
//                position = targetPosition;
//            }
//            if (rotationGoal > (float)Math.PI * 2 && rotation > (float)Math.PI * 2)
//            {
//                rotationGoal -= (float)Math.PI * 2;
//                rotation -= (float)Math.PI * 2;
//            }
//            TopPosBase.X = (CenterPos.X + (BAR.X / 2) + ((float)Math.Cos(rotation + (3f / 2f * Math.PI)) * 100)) / Main.UIScale;
//            TopPosBase.Y = (CenterPos.Y + (BAR.Y / 2) + ((float)Math.Sin(rotation + (3f / 2f * Math.PI)) * 100) + 25) / Main.UIScale;
//            MidPosBase.X = (CenterPos.X + (BAR.X / 2) + ((float)Math.Cos(rotation + (5f / 6f * Math.PI)) * 100)) / Main.UIScale;
//            MidPosBase.Y = (CenterPos.Y + (BAR.Y / 2) + ((float)Math.Sin(rotation + (5f / 6f * Math.PI)) * 100) + 25) / Main.UIScale;
//            BotPosBase.X = (CenterPos.X + (BAR.X / 2) + ((float)Math.Cos(rotation + (1f / 6f * Math.PI)) * 100)) / Main.UIScale;
//            BotPosBase.Y = (CenterPos.Y + (BAR.Y / 2) + ((float)Math.Sin(rotation + (1f / 6f * Math.PI)) * 100) + 25) / Main.UIScale;
//        }

//        private void UpdateTop()
//        {
//            if (topTheta == -100)
//                topTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
//            topTheta += ((float)Math.PI / 90) + (float)(Math.PI / 120 * Main.rand.NextDouble());
//            if (topTheta > (float)Math.PI * 2)
//                topTheta -= (float)Math.PI * 2;
//            if (Main.rand.Next(20) == 0)
//            {
//                if (Main.rand.Next(2) == 0)
//                    topMag *= .9f;
//                else
//                    topMag *= 1.1f;
//            }
//            if (topMag < 2)
//                topMag = 2;
//            if (topMag > 5)
//                topMag = 5;
//            TopPos.X = TopPosBase.X + (Animated ? topMag * (float)Math.Cos(topTheta) : 0);
//            TopPos.Y = TopPosBase.Y + (Animated ? topMag * (float)Math.Sin(topTheta * 2) : 0);
//        }
//        private void UpdateMid()
//        {
//            if (midTheta == -100)
//                midTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
//            midTheta += ((float)Math.PI / 90) + (float)(Math.PI / 120 * Main.rand.NextDouble());
//            if (midTheta > (float)Math.PI * 2)
//                midTheta -= (float)Math.PI * 2;
//            if (Main.rand.Next(20) == 0)
//            {
//                if (Main.rand.Next(2) == 0)
//                    midMag *= .9f;
//                else
//                    midMag *= 1.1f;
//            }
//            if (midMag < 2)
//                midMag = 2;
//            if (midMag > 5)
//                midMag = 5;
//            MidPos.X = MidPosBase.X + (Animated ? midMag * (float)Math.Cos(midTheta) : 0);
//            MidPos.Y = MidPosBase.Y + (Animated ? midMag * (float)Math.Sin(midTheta * 2) : 0);
//        }

//        private void UpdateBot()
//        {
//            if (botTheta == -100)
//                botTheta = (float)(Main.rand.NextDouble() * Math.PI * 2);
//            botTheta += ((float)Math.PI / 90) + (float)(Math.PI / 120 * Main.rand.NextDouble());
//            if (botTheta > (float)Math.PI * 2)
//                botTheta -= (float)Math.PI * 2;
//            if (Main.rand.Next(20) == 0)
//            {
//                if (Main.rand.Next(2) == 0)
//                    botMag *= .9f;
//                else
//                    botMag *= 1.1f;
//            }
//            if (botMag < 2)
//                botMag = 2;
//            if (botMag > 5)
//                botMag = 5;
//            BotPos.X = BotPosBase.X + (Animated ? botMag * (float)Math.Cos(botTheta) : 0);
//            BotPos.Y = BotPosBase.Y + (Animated ? botMag * (float)Math.Sin(botTheta * 2) : 0);
//        }

//        private void UpdatePositions()
//        {
//            LuxBar.Position = TopPos;
//            //OverflowLuxBar.Position = TopPos;
//            VisBar.Position = MidPos;
//            //OverflowVisBar.Position = BotPos;
//            MundusBar.Position = BotPos;
//            //OverflowMundusBar.Position = MidPos;
//        }
//    }

//    // This class will only be autoloaded/registered if we're not loading on a server
//    [Autoload(Side = ModSide.Client)]
//    internal class NewDawnMysticUISystem : ModSystem
//    {
//        private UserInterface MysticUserInterface;

//        internal NewDawnMysticUI NewDawnMysticBar;

//        public static LocalizedText ExampleResourceText { get; private set; }

//        public override void Load()
//        {
//            NewDawnMysticBar = new();
//            MysticUserInterface = new();
//            MysticUserInterface.SetState(NewDawnMysticBar);

//            string category = "UI";
//            ExampleResourceText ??= Mod.GetLocalization($"{category}.ExampleResource");
//        }

//        public override void UpdateUI(GameTime gameTime)
//        {
//            MysticUserInterface?.Update(gameTime);
//        }

//        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
//        {
//            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
//            if (resourceBarIndex != -1)
//            {
//                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
//                    "ExampleMod: Example Resource Bar",
//                    delegate
//                    {
//                        MysticUserInterface.Draw(Main.spriteBatch, new GameTime());
//                        return true;
//                    },
//                    InterfaceScaleType.UI)
//                );
//            }
//        }
//    }
//}
