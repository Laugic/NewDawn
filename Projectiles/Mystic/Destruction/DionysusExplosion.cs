using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Projectiles.Mystic.Destruction
{
	public class DionysusExplosion : DestructionProjectile
    {
        bool justSpawned = false;
        int projMaxTimeLeft = 0;

        public override void SetDefaults()
        {
            projMaxTimeLeft = 0;
            justSpawned = false;
            Projectile.width = 64;
            Projectile.height = 64;
			Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 26;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 9;
        }
		
		public override Color? GetAlpha(Color lightColor)
		{
			return ((Color.White * 0.75f) * (0.15f * Projectile.timeLeft));
		}

        public override void AI()
        {
			if (!justSpawned)
			{
                justSpawned = true;
                projMaxTimeLeft = Projectile.timeLeft;
                for (int i = 0; i < 15; i++)
				{
					Vector2 vector12 = Vector2.UnitX * 0f;
					vector12 += -Vector2.UnitY.RotatedBy((double)((float)i * (6.28318548f / 15)), default(Vector2)) * new Vector2(8f, 8f);
					vector12 = vector12.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
					int num104 = Dust.NewDust(Projectile.Center, 0, 0, 157, 0f, 0f, 150, default(Color), 1.5f);
					Main.dust[num104].noGravity = true;
					Main.dust[num104].position = Projectile.Center + vector12;
					Main.dust[num104].velocity = -1 * (Projectile.velocity * 0f + vector12.SafeNormalize(Vector2.UnitY) * 3.5f);
				}
			}
        }
		
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 10; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 157, Main.rand.Next((int)-3f, (int)3f), Main.rand.Next((int)-3f, (int)-1f), 150, default(Color), 1f);
				Main.dust[dust].noGravity = true;
			}
		}
        
        public override void PostAI()
        {         
            Projectile.frameCounter++;
            if (Projectile.frameCounter > projMaxTimeLeft / Main.projFrames[Projectile.type])
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 9)
            {
                Projectile.frame = 8;
                return;
            }
        }
    }
}