using Terraria;
using System;
using NewDawn.Dusts;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class GreatGladiusConjuration2 : ConjurationProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if(Main.rand.Next(4) == 0)Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<White>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Projectile.velocity.Y += .15f;
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
        }
    }
}