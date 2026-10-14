using NewDawn.Abstracts;
using NewDawn.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Items.Alchemistry.OLD_Essences
{
    public class FireEssence : Essence
    {

        public override void GetEffectAtIndex(Player player, AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    player.GetCritChance(DamageClass.Generic) += 8f;
                    break;
                case 1:
                    player.fireWalk = true;
                    player.lavaImmune = true;
                    player.buffImmune[BuffID.OnFire] = true;
                    break;
                default:
                    player.GetModPlayer<AccessoryPlayer>().SetOnFire = true;
                    break;
            }
        }
        public override string GetTipAtIndex(AbstractStorageItem item, int index)
        {
            switch (index)
            {
                case 0:
                    return "8% increased critical strike chance";
                case 1:
                    return "Grants immunity to fire and lava";
                default:
                    return "Attacks set enemies on fire";
            }
        }
    }
}
