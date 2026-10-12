using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Hypothema
{
    public class ShotgunYeet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("a yeeted shotgun");
        }

        public override void SetDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            Projectile.width = 70;
            Projectile.height = 70;
            Projectile.alpha = 0;
            Projectile.timeLeft = 240;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

        }

        public override void AI()
        {
            Projectile.velocity.Y += .2f;

            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item51, Projectile.Center);
            if (!Main.expertMode)
                return;
            for (int k = 0; k < 2; k++)
            {
                var theta = Main.rand.NextDouble() * Math.PI;
                float mag = -5 - (Main.rand.NextFloat() * 3);
                var entitySource = new EntitySource_Parent(this.Entity);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2((float)Math.Cos(theta) * mag, (float)Math.Sin(theta) * mag), ModContent.ProjectileType<Snowball>(), Projectile.damage, 7);
            }
        }
    }
}