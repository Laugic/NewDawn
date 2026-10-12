using NewDawn.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Plague
{
	public class Bubble : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
		}

		public override void AI()
		{
            if(Projectile.velocity.Y > -6)
                Projectile.velocity.Y -= .05f;
		}
        
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.damage > 0)
                target.AddBuff(ModContent.BuffType<Bubbly>(), 4 * 60);
		}
	}
}