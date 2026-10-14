using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.NPCs.Necrodon
{
    public class Quarter : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("A Quarter Note");
        }

        public override void SetDefaults()
        {
            NewDawnVars.eProjectiles.Add(Projectile.type);
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 10 * 60;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.ai[0] = 0;
        }

        public override void AI()
        {
            float light = 0.8f;
            Lighting.AddLight(Projectile.Center + Projectile.velocity, light, light, light);

            Projectile.rotation = 0;//(float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
            if (Projectile.rotation > Math.PI / 2 && Projectile.rotation < Math.PI * 1.5)
            {
                Projectile.rotation += (float)Math.PI;
            }
            else
                Projectile.spriteDirection = 1;
            if (Projectile.timeLeft < 30)
            {
                Projectile.alpha += 4;
                Projectile.scale *= .98f;
            }
        }
    }
}
