using NewDawn.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
    public class Fatality : ModBuff
    {
        public override string Texture =>
    $"Terraria/Images/Buff_{BuffID.PotionSickness}";

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<AlchemistryPlayer>().FatalityCharges > 0)
                player.buffTime[buffIndex] = (60 * player.GetModPlayer<AlchemistryPlayer>().FatalityCharges) + 2;
        }
    }
}
