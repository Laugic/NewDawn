using Terraria;

namespace NewDawn.Buffs
{
    public class LavaRegen : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lava Regen");
            // Description.SetDefault("Greatly increased regeneration");
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 8;
        }
    }
}
