using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.Players;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace NewDawn.UI.MoldyrianTemple
{
    /*
    public class SpiritBarUIOLD : UIState
    {
        private const int BAR_WIDTH = 192;

        public static bool visible = true;
        public static global::NewDawn.UI.NewAttempts.Mystic.SpriteProgressBar SpiritBar;
        public static Texture2D SpiritBGTexture;
        public static Texture2D SpiritBarTexture;
        public static Texture2D SpiritFGTexture;

        public static Mod mod = ModContent.GetInstance<NewDawn>();

        //private Vector2 SpiritBarPos = new Vector2((Main.screenWidth / 2 - BAR_WIDTH / 2 + (float)Math.Cos(3f / 2f * Math.PI) * 100) / Main.UIScale, (Main.screenHeight / 2 - BAR_WIDTH / 2 + (float)Math.Sin(3f / 2f * Math.PI) * 100 + 25) / Main.UIScale);
        private Vector2 SpiritBarPos = new Vector2((Main.screenWidth / 2f - BAR_WIDTH / 2f) / Main.UIScale, (16) / Main.UIScale);


        static SpiritBarUIOLD()
        {
            visible = true;
        }

        public void Load()
        {
            SpiritBGTexture = (Texture2D)ModContent.Request<Texture2D>("NewDawn/UI/Temple/SpiritBG");
            SpiritBarTexture = (Texture2D)ModContent.Request<Texture2D>("NewDawn/UI/Temple/SpiritBar");
            SpiritFGTexture = (Texture2D)ModContent.Request<Texture2D>("NewDawn/UI/Temple/SpiritFG");


            SpiritBar = new SpriteProgressBar
            (
                SpiritBGTexture,
                SpiritBarTexture,
                SpiritFGTexture,
                SpiritBarPos,
                new Vector2(0, 0)
            );
        }


        public void Unload()
        {
            SpiritBGTexture = null;
            SpiritBarTexture = null;
            SpiritFGTexture = null;
        }

        public void Update()
        {
            SpiritBar.Position = Main.screenPosition;
            SpiritBar.Position.X += Main.screenWidth / 2;
            SpiritBar.Position.Y += 16 / Main.UIScale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SpiritPlayer player = Main.LocalPlayer.GetModPlayer<SpiritPlayer>();

            visible = true;// player.ShowSpirit;

            SpiritBar.Position.X = (Main.screenWidth / 2f - BAR_WIDTH / 2f) / Main.UIScale;
            SpiritBar.Position.Y = (16) / Main.UIScale;

            SpiritBar?.DrawBar(spriteBatch, (int)player.Spirit, (int)(player.MaxSpirit));
            if (SpiritBar != null)
                Main.NewText("I'm trying to draw");

            UpdateHover(spriteBatch);
        }

        private void UpdateHover(SpriteBatch spriteBatch)
        {

            float posX = Main.MouseWorld.X - Main.screenPosition.X;
            float posY = Main.MouseWorld.Y - Main.screenPosition.Y;

            Vector2 drawPos = (Main.MouseWorld - Main.screenPosition);

            drawPos.X += 20;
            drawPos.Y += 8;
            int widthMod = 12;
            int heightMod = 8;

            string info = GetInfo();

            if (posX > SpiritBar.Position.X - widthMod && posX < SpiritBar.Position.X + SpiritBGTexture.Width + widthMod && posY > SpiritBar.Position.Y - heightMod && posY < SpiritBar.Position.Y + SpiritBGTexture.Height + heightMod)
                spriteBatch.DrawString(FontAssets.MouseText.Value, info, drawPos, Color.White);
        }

        private string GetInfo()
        {
            SpiritPlayer player = Main.LocalPlayer.GetModPlayer<SpiritPlayer>();
            string info = Math.Round(player.Spirit).ToString() + " Spirit\n";

            if (player.Spirit <= player.GuardianThreshold * player.MaxSpirit)
                info += "The Guardian has noticed you.\n";

            if (player.Spirit <= player.EncounterBuffThreshold * player.MaxSpirit)
                info += "Encounters are more difficult.\n";

            if (player.Spirit <= player.PainThreshold * player.MaxSpirit)
                info += "Your life runs thin, and so do your defenses.\n";

            if (player.Spirit <= player.BossThreshold * player.MaxSpirit)
                info += "Your foes grow stronger.\n";

            if (player.Spirit <= player.HurryThreshold * player.MaxSpirit)
                info += "Your bones grow fragile.\n";

            if (player.Spirit <= player.EmptyThreshold * player.MaxSpirit)
                info += "Damage is permanent. Time to die.\n";

            return info;
        }
    }





    */


    public class SpiritBarUI : UIState
    {
        public Vector2 Position = new Vector2((Main.screenWidth / 2) - 72, 32);

        public static bool visible = true;

        public static global::NewDawn.UI.NewAttempts.Mystic.SpriteProgressBar SpiritBar;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Main.LocalPlayer != null)
            {
                var player = Main.LocalPlayer.GetModPlayer<SpiritPlayer>();
                if (player != null)
                {
                    SpiritBar.Position = new Vector2((Main.screenWidth / 2) - (72 / Main.UIScale), 32 / Main.UIScale);
                    SpiritBar.MaxVal = (int)player.MaxSpirit;
                    SpiritBar.CurVal = (int)player.Spirit;
                    SpiritBar.Visible = player.ShowSpirit;
                    SpiritBar.FGAlpha = player.Spirit / player.MaxSpirit;
                    SpiritBar.Display = "Spirit: " + Math.Round(player.Spirit) + "/" + Math.Round(player.MaxSpirit);
                }
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (Main.LocalPlayer != null)
            {
                var player = Main.LocalPlayer.GetModPlayer<SpiritPlayer>();
                if (player != null && player.ShowSpirit)
                {
                    SpiritBar.DrawBar(Main.spriteBatch, (int)player.Spirit, (int)player.MaxSpirit);
                    //Main.spriteBatch.Draw(SpiritBGTexture, Position, Color.White);
                    //Main.NewText("Position: " + Position);
                }
            }
        }

        public void Load()
        {
            SpiritBar = new global::NewDawn.UI.NewAttempts.Mystic.SpriteProgressBar
            (
                ModContent.Request<Texture2D>("NewDawn/UI/Temple/SpiritBG").Value,
                ModContent.Request<Texture2D>("NewDawn/UI/Temple/SpiritBar").Value,
                ModContent.Request<Texture2D>("NewDawn/UI/Temple/SpiritFG").Value,
                Position,
                new Vector2(0, 0)
            );
            Append(SpiritBar);
        }


        public void Unload()
        {
            SpiritBar = null;
        }
    }

    public class SpiritUISystem : ModSystem
    {
        private UserInterface SpiritUserInterface;

        public static SpiritBarUI SpiritBar;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                SpiritBar = new();
                SpiritBar.Load();
                SpiritUserInterface = new();
                SpiritUserInterface.SetState(SpiritBar);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            SpiritUserInterface?.Update(gameTime);
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
                        SpiritUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
