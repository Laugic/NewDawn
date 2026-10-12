using System;
using NewDawn.NPCs.RockTwins;
using NewDawn.Projectiles.BossSummons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using NewDawn.Buffs;
using NewDawn.Projectiles.Mystic.Conjuration;

namespace NewDawn.Items.Consumables.Buffs
{
    public class Soul : NewDawnItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Restores Potentia and gives the Reaper buff.");
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
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            player.AddBuff(ModContent.BuffType<ReaperBuff>(), (int)(10 * 60 * modPlayer.MysticDuration));
            Item.stack = 0;
            modPlayer.AddLux(50);
            modPlayer.AddVis(50);
            modPlayer.AddMundus(50);

            BurstConjuration(player);

            return base.OnPickup(player);
        }

        private void BurstConjuration(Player player)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<DeathConjuration>()] > 0)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (Main.projectile[i].type == ModContent.ProjectileType<DeathConjuration>())
                    {
                        DeathConjuration dc = (DeathConjuration)(Main.projectile[i].ModProjectile);
                        dc.Burst();
                    }
                }
            }
        }
    }
}
