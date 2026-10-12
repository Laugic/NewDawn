using NewDawn.Players;
using Terraria;
using Terraria.UI;

namespace NewDawn.UI.NewAttempts.Mystic
{
    public class EnigmaMysticaInterface : GameInterfaceLayer
    {
        private readonly MysticUI mysticaUI;

        public EnigmaMysticaInterface(MysticUI mysticaUI) : base("Enigma: Mystica", InterfaceScaleType.UI)
        {
            this.mysticaUI = mysticaUI;
        }

        protected override bool DrawSelf()
        {
            MysticPlayer mysticPlayer = Main.LocalPlayer.GetModPlayer<MysticPlayer>();
            //if (mysticPlayer?.IsHoldingMystic() == true)
            //    mysticaUI.Draw(Main.spriteBatch);

            return true;
        }
    }
}