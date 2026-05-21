using NewDawn.Players;
using NewDawn.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace NewDawn.Abstracts
{
    public interface AbstractStorage
    {
        public Item[] Items { get; set; }

        public static void GenerateClearStorage(AbstractStorage storage)
        {
            for (int i = 0; i < storage.Items.Length; i++)
                storage.Items[i] = new();
        }

        public static void TriggerUI(AbstractStorage storage)
        {
            if (!Main.playerInventory)
                Main.playerInventory = true;

            var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
            if (uiplayer != null)
            {
                if (uiplayer.LinkedStorage != storage)
                {
                    uiplayer.LinkedStorage = storage;
                    SoundEngine.PlaySound(SoundID.MenuOpen);
                }
                else
                {
                    uiplayer.LinkedStorage = null;
                    SoundEngine.PlaySound(SoundID.MenuClose);
                }
            }
            ItemStorageUISystem.ItemStorage.Reload();
        }

        public bool FilterCheck(Item item, int slot);

        public virtual bool MaxStackOne()
        {
            return false;
        }
    }
}
