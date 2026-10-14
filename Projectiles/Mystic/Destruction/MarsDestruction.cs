using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class MarsDestruction : DestructionProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 30;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.scale = 1.5f;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f / 2;
        }
    }
}