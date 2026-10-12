using Microsoft.Xna.Framework.Graphics;
using NewDawn.UI.NewAttempts.Mystic;
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