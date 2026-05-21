using Microsoft.Xna.Framework;
using NewDawn.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace NewDawn.UI.MoldyrianTemple
{
    public class NewDawnSpiritInterface : GameInterfaceLayer
    {
        private readonly SpiritBarUI spiritUI;
        public NewDawnSpiritInterface(SpiritBarUI spiritUI) : base("NewDawn: Spirit", InterfaceScaleType.UI)
        {
            this.spiritUI = spiritUI;
        }

        protected override bool DrawSelf()
        {
            SpiritPlayer player = Main.LocalPlayer.GetModPlayer<SpiritPlayer>();

            if (player == null)
                return true;

            if (player.ShowSpirit)
                spiritUI.Draw(Main.spriteBatch);

            return true;
        }

        public class SpiritInterface : ModSystem
        {
            private UserInterface SpiritUserInterface;

            public static ItemStorageUI ItemStorage;

            public override void Load()
            {
                if (!Main.dedServ)
                {
                    ItemStorage = new();
                    SpiritUserInterface = new();
                    SpiritUserInterface.SetState(ItemStorage);
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
                        "New Dawn: Item Storage",
                        delegate {
                            SpiritUserInterface.Draw(Main.spriteBatch, new GameTime());
                            return true;
                        },
                        InterfaceScaleType.UI)
                    );
                }
            }
        }
    }
}
