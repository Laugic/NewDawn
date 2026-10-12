using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{

    public class WallOfFleshEffectCooldownBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wall of Flesh Effect Cooldown");
            // Description.SetDefault("Your Wall of Flesh effect is on Cooldown.");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}
