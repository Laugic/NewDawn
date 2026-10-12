using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class FriggDestruction : DestructionProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 200;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (Projectile.velocity.X > 0f)
			{
				Projectile.rotation += 0.1f;
			}
			else
			{
				Projectile.rotation -= 0.1f;
			}
        }
        
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 44, 0.1f * -Projectile.velocity.X, 0.1f * -Projectile.velocity.Y, 150, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
    }
}