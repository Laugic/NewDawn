using Terraria;
using Terraria.ModLoader;
using System;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Thrown
{
    public class Hail : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 120;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
            if (Main.rand.Next(5) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<EtherialDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Projectile.ai[0] += .01f;
            Projectile.velocity.Y += Projectile.ai[0];
            Projectile.velocity.X *= .98f;
        }
    }
}