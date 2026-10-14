using Terraria;

namespace NewDawn.Buffs
{
    public class CarapaceDamageBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Broken Shroud");
            // Description.SetDefault("Increased Damage!");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).DamageBoost(.15f);
        }
    }
}
