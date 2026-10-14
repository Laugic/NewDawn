using System;
using Terraria;

namespace NewDawn.Buffs
{
    public class TenacityCurseBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tenacity Curse");
            // Description.SetDefault("You've been weakened!\n-12 Defense");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).WeakenedDefense += 12;
        }
    }
}
