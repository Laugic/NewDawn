using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class FriggConjuration : ConjurationProjectile
    {
        public bool stopped = false;
        public int power = 0;
        public int damage = 0;
        public int delay = 0;

        public override void SetDefaults()
        {
            power = 0;
            stopped = false;
            damage = Projectile.damage;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5 * 60;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
            Projectile.rotation = 0;
            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Projectile.velocity.X *= .95f;
            Projectile.velocity.Y *= .975f;
			
			if (Projectile.velocity.X > 0f)
			{
				Projectile.spriteDirection = -1;
			}
			else
			{
				Projectile.spriteDirection = 1;
			}
			
            if(Math.Abs(Projectile.velocity.X) <= .2 && Math.Abs(Projectile.velocity.Y) <= .2)
            {
                stopped = true;
            }
            if (stopped)
            {
                delay += 1;
                if(delay >= 20)
                {
                    delay = 0;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        if (player.strongBees && Main.rand.Next(3) == 0)
                            Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X - 4 + Main.rand.Next(9), Projectile.Center.Y - 4 + Main.rand.Next(9), 0, 0.1f, ProjectileID.GiantBee, (int)(Projectile.damage * 1.5f), 2f, Projectile.owner);
                        else
                            Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X - 4 + Main.rand.Next(9), Projectile.Center.Y - 4 + Main.rand.Next(9), 0, 0.1f, ProjectileID.Bee, (int)(Projectile.damage), 2f, Projectile.owner);

                        for (int k = 0; k < 5; k++)
						{
							int num234 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 20) - Projectile.velocity, Projectile.width, Projectile.height, 153, Main.rand.NextFloat(-1f, 1f), 3f, 50, default(Color), 1f);
							Dust dust3 = Main.dust[num234];
							dust3 = Main.dust[num234];
							dust3.velocity *= 0.95f;
							Main.dust[num234].noGravity = true;
						}
					}
                }
            }
			
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 10;
			}
        }
		
		public override void PostAI()
        {         
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 4)
            {
                Projectile.frame = 0;
                return;
            }
        }
        
    }
}