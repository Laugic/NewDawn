using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
    public class EtherialEffectCooldownBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Etherial Rage");
            // Description.SetDefault("+20 Defense & +20% Damage");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
            //canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.2f;

            player.statDefense += 20;
        }
    }
}
