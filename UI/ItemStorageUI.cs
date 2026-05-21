using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;
using NewDawn.Items;
using System.Linq;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Terraria.ID;
using static Terraria.ModLoader.PlayerDrawLayer;
using NewDawn.Players;
using NewDawn.Abstracts;
using Terraria.Audio;
using NewDawn.Items.Alchemistry;
using System;

namespace NewDawn.UI
{
	public class ItemStorageUI : UIState
	{
        public void Reload()
        {
            RemoveAllChildren();

            Vector2 Position = new Vector2(Main.screenWidth / 2 - 72, Main.screenHeight / 2 - 96);

            int slots = 0;
            if (Main.LocalPlayer != null)
            {
                var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
                if (uiplayer != null && uiplayer.LinkedStorage != null)
                    slots = uiplayer.LinkedStorage.Items.Length;
            }

            if (slots > 0)
            {
                for (int i = 0; i < slots; i++)
                {
                    ItemStorageSlot slot = new(i);
                    slot.Left.Set(Position.X + 52 * i, 0f);
                    slot.Top.Set(Position.Y, 0f);
                    slot.Width.Set(48, 0);
                    slot.Height.Set(48, 0);
                    Append(slot);
                }
            }
        }
    }

    public class ItemStorageUISystem : ModSystem
	{
		private UserInterface ItemStorageUserInterface;

		public static ItemStorageUI ItemStorage;

		public override void Load() {
			if (!Main.dedServ) {
                ItemStorage = new();
				ItemStorageUserInterface = new();
				ItemStorageUserInterface.SetState(ItemStorage);
			}
		}

		public override void UpdateUI(GameTime gameTime) {
			ItemStorageUserInterface?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1) {
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"New Dawn: Item Storage",
					delegate {
						ItemStorageUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}
