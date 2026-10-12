using NewDawn.Buffs;
using Terraria;

namespace NewDawn.SoulStones
{
    public sealed class SoulStoneAbilityCooldownBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Stone Ability Cooldown");
            // Description.SetDefault("You used your Soul Stone's ability recently!");

            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
    }
}