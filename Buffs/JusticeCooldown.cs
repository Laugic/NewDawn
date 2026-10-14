using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class JusticeCooldown : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blind Justice");
            // Description.SetDefault("Justice has gone blind for a while.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).JusticeCooldown = true;
        }
    }
}
