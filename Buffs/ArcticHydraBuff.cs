using NewDawn.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
    public class ArcticHydraBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Arctic Hydra");
            // Description.SetDefault("The Hydra Hoard will fight for you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ArcticHydraHead>()] > 0)
            {
                modPlayer.ArcticHydraSummon = true;
            }
            if (!modPlayer.ArcticHydraSummon)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}