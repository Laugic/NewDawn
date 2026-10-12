using NewDawn.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
	public class HallowsEveIllusion2 : IllusionProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = 14;
			Projectile.alpha = 200;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 90;
			AIType = ProjectileID.SpikyBall;
            buffID = ModContent.BuffType<SpookedBuff>();
        }

        public override void AI()
		{
			if (Main.rand.Next(2) == 0)
			{
				int DustID = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 1f, 125, default(Color), 1.25f);
				Main.dust[DustID].noGravity = true;
			}
			
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 10;
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
	}
}