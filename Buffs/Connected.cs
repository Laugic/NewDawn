using Terraria;
using Terraria.ID;

namespace NewDawn.Buffs
{
    public class Connected : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Connected");
            // Description.SetDefault("You are connected to the grid!");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            if (NewDawnPlayer.Get(player).Connected <= 1)
                NewDawnPlayer.Get(player).Connected = 1;
        }
    }
}
