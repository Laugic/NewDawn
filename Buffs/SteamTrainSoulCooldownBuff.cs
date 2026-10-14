using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class SteamTrainSoulCooldownBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Steam Train Soul Cooldown");
            // Description.SetDefault("Your Steam Train effect is on Cooldown.");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}
