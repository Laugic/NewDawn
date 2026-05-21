using Microsoft.Xna.Framework;
using NewDawn.Abstracts;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.Essences
{
    public class AirEssence : Essence
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("The essence of Speed and Potential");
        }

        public override void GetEffectAtIndex(Player player, AbstractStorageItem item, int index)
        {
            switch(index)
            {
                case 0:
                    player.accRunSpeed += 3.5f;
                    player.maxRunSpeed += 3.5f;
                    player.moveSpeed += .2f;
                    break;
                case 1:
                    player.dashType = 1;
                    break;
                default:
                    player.slowFall = true;
                    player.jumpSpeedBoost = 3f;
                    break;
            }
        }

        public override string GetTipAtIndex(AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    return "20% increased movement speed and running speed";
                case 1:
                    return "Double tap to dash";
                default:
                    return "Jump higher and fall slower";
            }
        }
    }
}
