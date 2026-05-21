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
    public abstract class AbstractStorageItem : ModItem, AbstractStorage
    {
        public abstract Item[] Items { get; set; }

        public override void UpdateInventory(Player player)
        {
            CheckInventoryClosed();
        }

        public override void UpdateEquip(Player player)
        {
            CheckInventoryClosed();
        }

        public override void PostUpdate()
        {
            if (Main.mouseItem.ModItem == this)
            {
                var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
                if (uiplayer != null)
                {
                    if (uiplayer.LinkedStorage == this)
                    {
                        uiplayer.LinkedStorage = null;
                        SoundEngine.PlaySound(SoundID.MenuClose);
                    }
                    ItemStorageUISystem.ItemStorage.Reload();
                }
            }
            CheckInventoryClosed();
        }

        private void CheckInventoryClosed()
        {
            if (!Main.playerInventory)
            {
                var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
                if (uiplayer != null)
                {
                    if (uiplayer.LinkedStorage == this)
                    {
                        uiplayer.LinkedStorage = null;
                        SoundEngine.PlaySound(SoundID.MenuClose);
                    }
                    ItemStorageUISystem.ItemStorage.Reload();
                }
            }
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            var uiplayer = Main.LocalPlayer.GetModPlayer<UIPlayer>();
            if (uiplayer != null)
            {
                if (uiplayer.LinkedStorage == this)
                {
                    uiplayer.LinkedStorage = null;
                    SoundEngine.PlaySound(SoundID.MenuClose);
                }
                ItemStorageUISystem.ItemStorage.Reload();
            }
            CheckInventoryClosed();
        }

        public override void SaveData(TagCompound tag)
        {
            tag["Items"] = Items;
        }

        public override void LoadData(TagCompound tag)
        {
            Item[] testItems = tag.Get<Item[]>("Items");
            if (testItems != null)
                Items = testItems;
        }

        public virtual bool FilterCheck(Item item, int slot)
        {
            return true;
        }
        public virtual bool MaxStackOne()
        {
            return false;
        }
    }
}
