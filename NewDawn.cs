using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NewDawn.UI;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace NewDawn
{
    public class NewDawn : Mod
    {
        public static Texture2D MysticWeaponIcon;

        internal static ModKeybind toggleMystic, toggleSoulStoneV, toggleSoulStoneM, quickMystica, soulStoneAbility, restockNearby;

        public static int zaWarudo = 0;

        public static SoundStyle CreateSoundStyle(string path)
        {
            return new SoundStyle($"{nameof(NewDawn)}/{path}");
        }

        public static Asset<Texture2D> RequestTextureAsset(string path)
        {
            string removeDuplicateName = $"{nameof(NewDawn)}/{path}";
            removeDuplicateName = removeDuplicateName.Replace("NewDawn.", "");
            return ModContent.Request<Texture2D>($"{nameof(NewDawn)}/{removeDuplicateName}");
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
                Vector2 position = start + (direction * (length * (i + 0.5f) / segmentCount)) - Main.screenPosition;
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
                //GameInterfaceLayer Insert = new GameInterfaceLayer("NewDawn: Mysticism", delegate
                //{
                //    MysticaUserInterface.Draw(Main.spriteBatch, new GameTime());
                //    return true;
                //}, InterfaceScaleType.UI);
                //ModSystem.ModifyInterfaceLayers(Insert, MysticaUserInterface);
                //ModSystem.Interf
                NewDawnUI.Load();
            }

            toggleMystic = KeybindLoader.RegisterKeybind(this, "Toggle Mysticism", "Mouse2");
            toggleSoulStoneV = KeybindLoader.RegisterKeybind(this, "Toggle Visual Effects", "V");
            toggleSoulStoneM = KeybindLoader.RegisterKeybind(this, "Toggle Mobility Effects", "C");
            quickMystica = KeybindLoader.RegisterKeybind(this, "Quick Mystica", "G");
            soulStoneAbility = KeybindLoader.RegisterKeybind(this, "Soul Stone Ability", "Mouse3");
            restockNearby = KeybindLoader.RegisterKeybind(this, "Restock from Nearby Chests", "N");
        }

        public override void Unload()
        {
            Instance = null;
            NewDawnUI.Unload();
        }

        //public static UserInterface MysticaUserInterface { get; private set; }
        public static NewDawn Instance { get; private set; }
    }
}