using Terraria;
using Terraria.ModLoader;
using System;
using NewDawn.Buffs;

namespace NewDawn.Projectiles.Accessory
{
    public class CosmicTrailProj : ModProjectile
    {
        int delay = 0;
        int direction = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic");
        }

        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 160;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = .5f;
            delay = 20;
        }

        public override void AI()
        {
            if (delay <= 0)
            {
                Projectile.scale *= .98f;
                Projectile.velocity *= .98f;
                if (Projectile.scale <= .05f)
                    Projectile.Kill();
            }
            else
            {
                delay--;
            }
            if (direction == 0)
            {
                if (Main.rand.Next(2) == 0)
                    direction = -1;
                else
                    direction = 1;
            }
            Projectile.rotation += (float)(Math.PI / 30) * direction;
        }
    }
}
