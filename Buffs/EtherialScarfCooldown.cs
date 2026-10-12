using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class EtherialScarfCooldown : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fragmented Soul");
            // Description.SetDefault("Your Etherial Scarf has to regenerate before it can save you again.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).EtherialScarfCooldown = true;
        }
    }
}
