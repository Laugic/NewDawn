using System;
using Terraria;
using Microsoft.Xna.Framework;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class HallowsEveConjuration4 : ConjurationProjectile
    {
		public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.penetrate = 1;
			Projectile.friendly = true;
            Projectile.timeLeft = 180;
        }
		
		public override void AI()
		{
			Projectile.rotation = (float)System.Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			
			for (int i = 0; i < 3; i++)
            {
                float xSpeed = Projectile.velocity.X / 3f * (float)i;
                float ySpeed = Projectile.velocity.Y / 3f * (float)i;
                int newDust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 111, 0f, 0f, 125, default(Color), 1.25f);
                Main.dust[newDust].position.X = Projectile.Center.X - xSpeed;
                Main.dust[newDust].position.Y = Projectile.Center.Y - ySpeed;
                Main.dust[newDust].velocity *= 0.15f;
				Main.dust[newDust].noGravity = true;
            }
			
			Player player = Main.player[Projectile.owner];
			float xPos = Projectile.Center.X;
			float yPos = Projectile.Center.Y;
			float maxDist = 500f;
			bool target = false;
			
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].CanBeChasedBy(Projectile, false) && Projectile.Distance(Main.npc[i].Center) < maxDist && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
				{
					float xDiff = Main.npc[i].position.X + (float)(Main.npc[i].width / 2);
					float yDiff = Main.npc[i].position.Y + (float)(Main.npc[i].height / 2);
					float dist = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - xDiff) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - yDiff);
					if (dist < maxDist)
					{
						maxDist = dist;
						xPos = xDiff;
						yPos = yDiff;
						target = true;
					}
				}
			}
			
			if (target)
			{
				float vel = 6f;
				
				Vector2 position = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float xTarget = xPos - position.X;
				float yTarget = yPos - position.Y;
				float dist = (float)Math.Sqrt((double)(xTarget * xTarget + yTarget * yTarget));
				dist = vel / dist;
				xTarget *= dist;
				yTarget *= dist;
				
				Projectile.velocity.X = (Projectile.velocity.X * 20f + xTarget) / 21f;
				Projectile.velocity.Y = (Projectile.velocity.Y * 20f + yTarget) / 21f;
			}
		}
	}
}