using Terraria;
using Terraria.ModLoader;
using System;
using NewDawn.Buffs;
using NewDawn.Dusts;

namespace NewDawn.Projectiles.Plague
{
	public class TrueDawnSpark : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 256;
            Projectile.tileCollide = false;
        }

		public override void AI()
		{
            if (Main.rand.Next(4) == 0) Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<DawnDust>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            Projectile.scale *= .95f;
            if (Projectile.scale < .05f)
                Projectile.Kill();
            Projectile.rotation = Projectile.timeLeft *(float)Math.PI / 10f;
        }
        
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 8; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<DawnDust>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.damage > 0)
                target.AddBuff(ModContent.BuffType<TrueDawn>(), 6 * 60 + Main.rand.Next(6 * 60));
        }
    }
}