using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class MoonLordSoulCooldownBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Moon Lord Soul Cooldown");
            // Description.SetDefault("Your Moon Lord effect is on Cooldown.");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}
