using System;
using NewDawn.Buffs;
using NewDawn.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.NPCProj
{
	public class ConductorProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 76;
			Projectile.height = 102;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 8;
			Projectile.timeLeft = 120;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 2;

        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            if (Projectile.velocity.X < 0) Projectile.frame = 1;
            else Projectile.frame = 0;
            if(Main.rand.Next(0,14)==0)Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Steam>(), 0f, 0f);
		}

        public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Steam>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
			}
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<Steamy>(), 120);
		}
	}
}