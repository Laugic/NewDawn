using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class TimeExhausted : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Out of Time");
            // Description.SetDefault("You cannot use Time Stop for a while");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).zCool = true;
        }
    }
}
