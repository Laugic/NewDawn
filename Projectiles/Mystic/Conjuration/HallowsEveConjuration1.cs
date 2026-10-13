using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class HallowsEveConjuration1 : ConjurationProjectile
    {
        public bool stopped = false;
        public int timer = 0;
		public int offset = 0;
        bool justSpawned = false;
        int delay = 0;

        public override void SetDefaults()
        {
            delay = 0;
            stopped = false;
            justSpawned = false;
            Projectile.width = 44;
            Projectile.height = 32;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 7 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Main.projFrames[Projectile.type] = 2;
        }
		
		public override void PostDraw(Color lightColor)
        {
			Rectangle frame = new Rectangle(0, 0, 44, 36);
			frame.Y += 36 * Projectile.frame;	
			if (Projectile.spriteDirection == 1)
			{
				Main.spriteBatch.Draw(NewDawn.RequestTexture("Projectiles/Mystic/Conjuration/HallowsEveConjuration1_Glow"), Projectile.Center - Main.screenPosition, frame, Color.White * 0.5f, Projectile.rotation, new Vector2(22, 18), 1f, SpriteEffects.None, 0f);
			}
			else
			{
				Main.spriteBatch.Draw(NewDawn.RequestTexture("Projectiles/Mystic/Conjuration/HallowsEveConjuration1_Glow"), Projectile.Center - Main.screenPosition, frame, Color.White * 0.5f, Projectile.rotation, new Vector2(22, 18), 1f, SpriteEffects.FlipHorizontally, 0f);
			}
		}
        
        public override bool PreAI()
        {
            Projectile.tileCollide = true;
            return true;
        }

        public override void AI()
        {
			Lighting.AddLight(Projectile.position, 0.5f, 0.35f, 0.15f);
            Projectile.velocity.X *= .95f;
            Projectile.velocity.Y += 1f;
            delay++;
			if (delay > 10)
			{
				timer++;
				if (timer >= 20)
				{
					Projectile.frameCounter = 0;
					float distance = 2000f;
					int index = -1;
					for (int i = 0; i < 200; i++)
					{
						float dist = Vector2.Distance(Projectile.Center, Main.npc[i].Center);
						if (dist < distance && dist < 700f && Main.npc[i].CanBeChasedBy(Projectile, false))
						{
							index = i;
							distance = dist;
						}
					}
					if (index != -1)
					{
						if (Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[index].position, Main.npc[index].width, Main.npc[index].height))
						{
							Vector2 vector = Main.npc[index].Center - Projectile.Center;
							float speed = 12f;
							float mag = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
							if (mag > speed && mag != 0)
							{
								mag = speed / mag;
							}
							vector *= mag;
							
							if (vector.X > 0f)
							{
								Projectile.spriteDirection = -1;
								offset = 16;
							}
							else
							{
								Projectile.spriteDirection = 1;
								offset = -16;
							}
				
							int choice = Main.rand.Next(3);
							if (choice == 0)
							{
								Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + offset, Projectile.Center.Y, vector.X, vector.Y, ModContent.ProjectileType<HallowsEveConjuration2>(), (int)(Projectile.damage * 1f), 3f, Projectile.owner);
							}
							else if (choice == 1)
							{
								Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + offset, Projectile.Center.Y, vector.X, vector.Y - 5f, ModContent.ProjectileType<HallowsEveConjuration3>(), (int)(Projectile.damage * 1.25f), 3f, Projectile.owner);
							}
							else
							{
								Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + offset, Projectile.Center.Y, vector.X, vector.Y, ModContent.ProjectileType<HallowsEveConjuration4>(), (int)(Projectile.damage * 0.75f), 3f, Projectile.owner);
							}
						}
					}
					timer = 0;
				}
			}
			else
			{
				if (Projectile.velocity.X > 0f)
				{
					Projectile.spriteDirection = -1;
				}
				else
				{
					Projectile.spriteDirection = 1;
				}
			}

			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 10;
			}
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false; 
			return true;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			stopped = true;
			return false;
		}
		
		public override void PostAI()
        {         
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 2)
            {
                Projectile.frame = 0;
                return;
            }
        }
    }
}