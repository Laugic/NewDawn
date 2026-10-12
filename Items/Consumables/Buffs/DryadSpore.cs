using System;
using NewDawn.NPCs.RockTwins;
using NewDawn.Projectiles.BossSummons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewDawn.Items.Consumables.Buffs
{
    public class DryadSpore : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Gives Dryad's Blessing'");
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 1;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return false;
        }

        public override bool CanPickup(Player player)
        {
            return true;
        }

        public override bool OnPickup(Player player)
        {
            player.AddBuff(BuffID.DryadsWard, 4 * 60);
            Item.stack = 0;
            return base.OnPickup(player);
        }
    }
}
