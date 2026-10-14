using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class EtherBones : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ether Bones");
            // Description.SetDefault("Your bones are infused with the strength of the Etherial.");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).EtherBonesBoost = true;
        }
    }
}
