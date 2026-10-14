using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
    public class MagmaHeartBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magma Heart");
            // Description.SetDefault("You are immune to lava\n'A special kind of love.'");
            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            NewDawnPlayer.Get(player).obsHeart = true;
            player.buffTime[buffIndex] = 18000;
            player.lavaImmune = true;
            player.fireWalk = true;
            player.buffImmune[BuffID.OnFire] = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<MagmaHeartProjectile>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(new EntitySource_Parent(player), new Vector2(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2)), new Vector2(0f, 0f), ModContent.ProjectileType<MagmaHeartProjectile>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
