using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class EvilBossCooldownBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Evil Boss Soul Cooldown");
            // Description.SetDefault("Your World Evil Boss effect is on Cooldown.");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}
