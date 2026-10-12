using Microsoft.Xna.Framework;
using Terraria;
using System;
using NewDawn.Dusts;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class YuleDestruction : DestructionProjectile
    {
        public bool spawned = false;
        private float vMag = 16f;
        private float vAccel = .4f;
        private float vMax = 22f;
        bool homing = true;

		public override void SetDefaults()
		{
            homing = true;
            vMag = 16f;
            spawned = false;
			Projectile.width = 24;
			Projectile.height = 24;
            Projectile.timeLeft = 120;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f * 3;
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Frost>(), 0, 0);
            Vector2 targetPos = Main.MouseWorld;
            float dist = Vector2.Distance(targetPos, Projectile.Center);
            float tVel = dist / 15;
            if (vMag < vMax && vMag < tVel && homing)
            {
                vMag += vAccel;
            }
            if (dist != 0 && homing)
            {
                Projectile.velocity = Projectile.DirectionTo(targetPos) * vMag;
            }
            if (dist < vMag + 2f && homing)
                homing = false;
        }
    }
}