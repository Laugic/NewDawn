using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class GemShard : ConjurationProjectile
    {
		public Color colorType;
		
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
			Main.projFrames[Projectile.type] = 6;
        }
        
		public override void AI()
        {
			Projectile.frame = (int)(Projectile.ai[1]);
			
			Projectile.velocity *= 0.95f;
			
			if (Projectile.velocity.X > 0f)
			{
				Projectile.rotation += 0.25f;
			}
			else
			{
				Projectile.rotation -= 0.25f;
			}

			if (Projectile.ai[1] == 0){colorType = new Color(255,0,0);}
			if (Projectile.ai[1] == 1){colorType = new Color(255,226,0);}
			if (Projectile.ai[1] == 2){colorType = new Color(8,255,0);}
			if (Projectile.ai[1] == 3){colorType = new Color(0,217,255);}
			if (Projectile.ai[1] == 4){colorType = new Color(209,0,255);}
			if (Projectile.ai[1] == 5){colorType = new Color(255,255,255);}
			
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 8;
			}
		}
		
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 16, Main.rand.Next((int)-2f, (int)2f), Main.rand.Next((int)-2f, (int)2f), 0, colorType, 0.75f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].noLight = true;
			}
		}
    }
}