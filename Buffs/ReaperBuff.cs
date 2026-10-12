using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
    public class ReaperBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reaper");
            // Description.SetDefault("Increased damage and movement speed");
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += .15f;
            player.moveSpeed += .15f;
        }
    }
}
