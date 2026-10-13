using System;
using NewDawn.Buffs;
using NewDawn.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Illusion
{
    public class HallowsEveIllusion1 : IllusionProjectile
    {
        public float theta = 0;
		public int timer = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hallow's Eve Illusion");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            buffID = ModContent.BuffType<SpookedBuff>();
        }
		/*
		public override Color? GetAlpha(Color lightColor)
		{
            return ((Color.White * 0.0f) * (0.025f * projectile.timeLeft));
		}*/

        public override void AI()
        {
			Vector2 origin = Projectile.Center;
			theta -= 3.14f / 20;

            for (int i = 0; i < 2; i++)
            {
			    double targetX = origin.X + Math.Cos(theta + i * 3.14f / 1) * 10;
			    double targetY = origin.Y + Math.Sin(theta + i * 3.14f / 1) * 10;
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 0, default(Color), 2.25f);
				Main.dust[dust].position.X = (float)targetX;
				Main.dust[dust].position.Y = (float)targetY;
				Main.dust[dust].velocity *= 0f;
				Main.dust[dust].noGravity = true;
			}
			
			timer++;
			if (timer > 8)
			{
				Projectile.NewProjectile(new EntitySource_Parent(Entity), new Vector2(Projectile.position.X, Projectile.position.Y), new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, -1f)), ModContent.ProjectileType<HallowsEveIllusion2>(), (int)(Projectile.damage * 0.35f), 1f, Main.myPlayer);
				timer = 0;
			}
        }
		
		public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 2; k++)
			{
				for(int i = 0; i < 20; i++)
				{
					Vector2 theta2 = Vector2.UnitX * 0f;
					theta2 += -Vector2.UnitY.RotatedBy((double)((float)i * (6.28318548f / 20)), default(Vector2)) * new Vector2(8f, 8f);
					theta2 = theta2.RotatedBy((double)Projectile.velocity.ToRotation(), default(Vector2));
					int dust = Dust.NewDust(Projectile.Center, 0, 0, 6, 0f, 0f, 0, default(Color), 1.25f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].position = Projectile.Center + theta2;
					Main.dust[dust].velocity = Projectile.velocity * 0f + theta2.SafeNormalize(Vector2.UnitY) * 1.25f;
				}
			}
		}
    }
}