using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Ameldera
{
    public class ElderShardRock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Elder Shard Rock");
        }

        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.timeLeft = 180;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + (1.57f / 2);
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            float theta, mag;
            for (int i = 0; i < 5; i++)
            {
                theta = Main.rand.NextFloat() * (float)Math.PI;
                mag = (Main.rand.NextFloat() * 3) + 3;
                Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * -mag), ModContent.ProjectileType<ElderCrystalShard>(), Projectile.damage / 2, 3f);
            }
            Projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }
    }
}