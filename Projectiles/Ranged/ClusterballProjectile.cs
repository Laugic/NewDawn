using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
    public class ClusterballProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Clusterball");
        }

        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .9f;
            Projectile.velocity.X *= .975f;
        }

        public override void OnKill(int timeLeft)
        {
            int shards = Main.rand.Next(2, 5);
            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < shards; i++)
                {
                    var theta = (float)(Math.PI * Main.rand.NextDouble() * 2);
                    var newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                    newVel *= 10;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, newVel.X, newVel.Y, ModContent.ProjectileType<ClusterballFragmentProjectile>(), Projectile.damage / 2, 3, Main.myPlayer);
                }
            }
            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);
        }
    }
}
