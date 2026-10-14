using NewDawn.Buffs;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
	public class FriggIllusion : IllusionProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
			Projectile.aiStyle = -1;
			Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            buffID = ModContent.BuffType<PollinatedBuff>();
            baseDuration = 8 * 60;
        }

        public override void AI()
        {
			if (Projectile.timeLeft > 20 && Projectile.alpha > 0)
			{
				Projectile.alpha -= 15;
			}
			else
			{
				Projectile.alpha += 15;
			}
			
			if (Projectile.velocity.X > 0f)
			{
				Projectile.rotation += 0.08f;
			}
			else
			{
				Projectile.rotation -= 0.08f;
			}
			
            if (Math.Abs(Projectile.velocity.X) <= 10f && Math.Abs(Projectile.velocity.Y) <= 10f)
            {
                Projectile.velocity.X *= 1.01f;
                Projectile.velocity.Y *= 1.01f;
            }
        }
    }
}