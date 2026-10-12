using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class DionysusConjuration : ConjurationProjectile
    {
        public bool stopped = false;
        public int delay = 0;

        public override void SetDefaults()
        {
            stopped = false;
            Projectile.width = 54;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 8 * 60;
            Main.projFrames[Projectile.type] = 6;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation = 0;
            Player player = Main.player[Projectile.owner];
            NewDawnPlayer modPlayer = NewDawnPlayer.Get(player);
            Projectile.velocity *= .9f;
            if(Math.Abs(Projectile.velocity.X) <= .2 && Math.Abs(Projectile.velocity.Y) <= .2)
            {
                stopped = true;
            }
            if (stopped)
            {
                delay += 1;
                if(delay >= 10)
                {
                    delay = 0;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X + Main.rand.Next(-16, 16), Projectile.Center.Y - 6 + Main.rand.Next(16), 0, 10, ModContent.ProjectileType<DionysusConjuration2>(), (int)(Projectile.damage), 3f, Main.myPlayer);
                    }
                }
            }
        }
		
		public override void PostAI()
        {         
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 5)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 6)
            {
                Projectile.frame = 0;
                return;
            }
        }
    }
}