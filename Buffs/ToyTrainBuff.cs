using Microsoft.Xna.Framework;
using NewDawn.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Buffs
{
    public class ToyTrainBuff : NewDawnBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toy Train");
            // Description.SetDefault("Choo Choo!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            NewDawnPlayer.Get(player).ToyTrain = true;

            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<ToyTrainProjectile>()] <= 0;

            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(new EntitySource_Parent(player), new Vector2(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2)), new Vector2(0f, 0f), ModContent.ProjectileType<ToyTrainProjectile>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
