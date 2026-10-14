using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class MobilityCurseBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mobility Curse");
            // Description.SetDefault("Slow down there, punk.");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= .7f;
            player.maxRunSpeed *= .7f;
        }
    }
}
