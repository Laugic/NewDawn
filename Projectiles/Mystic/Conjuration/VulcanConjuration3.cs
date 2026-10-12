using Microsoft.Xna.Framework;
using Terraria;
using System;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class VulcanConjuration3 : ConjurationProjectile
    {
        private bool spawned = false;

        public override void SetDefaults()
        {
            spawned = false;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 3;
        }

        public override void AI()
        {
            if (!spawned)
            {
                spawned = true;
                Projectile.frame = Main.rand.Next(3);
                Vector2 targetPos;
                targetPos.X = Main.MouseWorld.X;
                targetPos.Y = Main.MouseWorld.Y;
                Projectile.velocity = Projectile.DirectionTo(targetPos) * 12f;
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
        }
    }
}