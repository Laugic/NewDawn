using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class CapacityCurseBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Capacity Curse");
            // Description.SetDefault("You've been weakened!");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).WeakenedDefense += 10;
        }
    }
}
