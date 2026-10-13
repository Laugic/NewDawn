using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Melee
{
	public class FlaranceProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 240;
		}

		public override void AI()
		{
            if(Projectile.velocity.Y < 12)
                Projectile.velocity.Y += .15f;
			if(Main.rand.Next(6) == 0)Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);

            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 1 && Projectile.ai[0] >= 5 && Projectile.ai[1] == 1)
            {
                Projectile.velocity.X = 0f;
                Projectile.velocity.Y = 0f;
                Projectile.tileCollide = false;
                Projectile.alpha = 255;
                Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
                Projectile.width = 96;
                Projectile.height = 96;
                Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
            }
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
            if (Projectile.ai[0] == 0)
                Projectile.Kill();
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}
            Projectile.velocity.Y *= .9f;
            return false;
		}

		public override void OnKill(int timeLeft)
        {
            if(Projectile.ai[0] >= 5 && Projectile.ai[1] == 1)
                SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int k = 0; k < 18; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Magma>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.ai[0] >= 5)
                modifiers.SourceDamage *= 1.5f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
            if(Projectile.ai[0] >= 3)
			    target.AddBuff(BuffID.OnFire, 2 * 60 + Main.rand.Next(60));
            Projectile.timeLeft = 2;
            Projectile.ai[1] = 1;
        }
	}
}