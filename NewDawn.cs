using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.UI.NewAttempts.Mystic;
using ReLogic.Content;
using System;
using Terraria.Audio;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace NewDawn
{
    public class NewDawn : Mod
    {
        public const string GOLD_BARS_GROUP = "GldBars";
        public const string EVIL_BARS_GROUP = "EnigmaEvilBars";
        public const string DOUBLE_JUMP_GROUP = "EnigmaDoubleJump";
        public const string COLORED_BALLOON_GROUP = "EnigmaColoredBalloon";
        public static Texture2D MysticWeaponIcon;

        internal static ModKeybind toggleMystic, toggleSoulStoneV, toggleSoulStoneM, quickMystica, soulStoneAbility, restockNearby;

        public static int zaWarudo = 0;

        public static SoundStyle CreateSoundStyle(string path)
        {
            return new SoundStyle($"{nameof(NewDawn)}/{path}");
        }

        public static Asset<Texture2D> RequestTextureAsset(string path)
        {
            return ModContent.Request<Texture2D>($"{nameof(NewDawn)}/{path}");
        }

        public static Texture2D RequestTexture(string path)
        {
            return RequestTextureAsset(path).Value;
        }

        public static void DrawChain(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end, float scale = 1f, Color? drawColor = null)
        {
            Vector2 difference = end - start;
            float length = difference.Length();
            float segmentLength = texture.Height * scale;
            if (length <= 0f || segmentLength <= 0f)
                return;

            int segmentCount = (int)Math.Ceiling(length / segmentLength);
            Vector2 direction = difference / length;
            float rotation = difference.ToRotation() + MathHelper.PiOver2;
            Color color = drawColor ?? Color.White;

            for (int i = 0; i < segmentCount; i++)
            {
                Vector2 position = start + direction * (length * (i + 0.5f) / segmentCount) - Main.screenPosition;
                spriteBatch.Draw(texture, position, null, color, rotation, texture.Size() / 2f, scale, SpriteEffects.None, 0f);
            }
        }

        public NewDawn()
        {
            Instance = this;
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {

                EnigmaMysticUI = new NewDawnMysticUI();
                EnigmaMysticUI.Activate();
                //EnigmaMysticUI.Load();
                MysticaUserInterface = new UserInterface();
                MysticaUserInterface.SetState(EnigmaMysticUI);
            }

            toggleMystic = KeybindLoader.RegisterKeybind(this, "Toggle Mysticism", "Mouse2");
        }

        public override void Unload()
        {
            Instance = null;
            //MysticaUserInterface = null;
        }


        //public static UserInterface MysticaUserInterface { get; private set; }
        public static NewDawn Instance { get; private set; }
        public static NewDawnMysticUI EnigmaMysticUI { get; private set; }
        public static UserInterface MysticaUserInterface { get; private set; }
    }
}