using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class TimeDilation : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Time Dilation");
            // Description.SetDefault("Take a burst of damage when Time is Stopped");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}
