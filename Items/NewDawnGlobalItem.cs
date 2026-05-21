using NewDawn.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items
{
    public class NewDawnGlobalItem : GlobalItem
    {

        public override bool OnPickup(Item item, Player player)
        {
            if(item.type == ItemID.Heart)
            {
                if (player.GetModPlayer<AccessoryPlayer>().BonusHealFromHearts > 0)
                    player.Heal(player.GetModPlayer<AccessoryPlayer>().BonusHealFromHearts);
            }
            return base.OnPickup(item, player);
        }
    }
}
