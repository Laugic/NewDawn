using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class Annihilation : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Annihilation");
            // Description.SetDefault("You are infused with the rage of an Annihilator.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).AnnihilationBoost = true;
        }
    }
}
