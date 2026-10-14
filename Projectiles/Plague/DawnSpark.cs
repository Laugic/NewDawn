using NewDawn.Buffs;
using NewDawn.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Plague
{
	public class DawnSpark : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.penetrate = 4;
			Projectile.timeLeft = 56;
            Projectile.tileCollide = false;
        }

		public override void AI()
		{
			if(Main.rand.Next(4) == 0)Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<DawnDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Projectile.scale *= .95f;
            if (Projectile.scale < .05f)
                Projectile.Kill();
		}
        
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<DawnDust>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.damage > 0)
                target.AddBuff(ModContent.BuffType<DawnBuff>(), 4 * 60 + Main.rand.Next(2 * 60));
		}
	}
}