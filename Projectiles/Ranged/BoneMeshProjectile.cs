using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Ranged
{
    public class BoneMeshProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            NewDawnVars.SnowballProjectiles.Add(Projectile.type);
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.velocity.Y += .5f;
            Projectile.velocity.X *= .975f;
        }

        public override void OnKill(int timeLeft)
        {
            int shards = Main.rand.Next(4);
            if (Main.myPlayer == Projectile.owner)
            {
                for (int i = 0; i < shards; i++)
                {
                    var theta = (float)(Math.PI * Main.rand.NextDouble() * 2);
                    var newVel = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                    newVel *= 10;
                    Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, newVel.X, newVel.Y, ModContent.ProjectileType<BoneMeshFragmentProjectile>(), Projectile.damage / 2, 3, Main.myPlayer);
                }
            }
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}