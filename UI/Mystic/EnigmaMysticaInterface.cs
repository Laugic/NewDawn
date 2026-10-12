using Terraria;
using Terraria.UI;

namespace NewDawn.UI
{
    public class EnigmaMysticaInterface : GameInterfaceLayer
    {
        private readonly NewDawnUI mysticaUI;

        public EnigmaMysticaInterface(NewDawnUI mysticaUI) : base("Enigma: Mystica", InterfaceScaleType.UI)
        {
            this.mysticaUI = mysticaUI;
        }

        protected override bool DrawSelf()
        {
            NewDawnPlayer NewDawnPlayer = NewDawnPlayer.Get();

            if (NewDawnPlayer == null)
                return true;

            if (NewDawnPlayer.MysticHold > 0)
                mysticaUI.Draw(Main.spriteBatch);

            return true;
        }
    }
}