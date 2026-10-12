using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class Mysticality3 : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Volatile Mysticality");
            // Description.SetDefault("You can't drink Potentia Potions for a while.\n+20% Overflow");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).Mysticality = 2;
            NewDawnPlayer.Get(player).GlobalOverflow += .2f;
        }
    }
}
