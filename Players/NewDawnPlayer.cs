using NewDawn.Items.Alchemistry.AnimaCrystals.Primals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NewDawn.Players
{
    public sealed class NewDawnPlayer : ModPlayer
    {
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
        {
            if(player.ZoneMeteor)
            {
                int rand = Main.rand.Next(5);
                switch (rand)
                {
                    case 0:
                        caughtType = ModContent.ItemType<CrackedCosmicCrystal>();
                        break;
                    case 1:
                        caughtType = ItemID.Meteorite;
                        break;
                }
            }
        }
    }
}
