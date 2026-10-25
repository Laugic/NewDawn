using NewDawn.Abstracts;
using NewDawn.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Players
{
    public class UIPlayer : ModPlayer
    {
        public AbstractStorage LinkedStorage = null;

        public override void PostUpdate()
        {
            if(!Main.playerInventory)
            {
                LinkedStorage = null;
                ItemStorageUISystem.ItemStorage.Reload();
            }
        }
    }
}
