using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.Mystic.Conjuration
{
	public class MarsGeyeser : ConjurationProjectile
    {
        public double rot = 0.0;
        public int delay = 0;
        public int delayMax = 4;
        public int dir = 0;

        public override void SetDefaults()
        {
            delayMax = 4;
            rot = 0.0;
            delay = 0;
            dir = 0;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 90;
            Projectile.ignoreWater = true;
            Projectile.alpha = 250;
        }

        public override void AI()
        {
            if (dir == 0)dir = -1 + 2*Main.rand.Next(0, 2);
            rot += dir*0.02;
            if(Main.rand.Next(3) == 0)
                rot += dir * 0.04;
            if (Main.rand.Next(3) == 0)
                rot += dir * 0.04;
            Projectile.rotation = (float)rot;
            delay++;
            if (delay >= delayMax)
            {
                delay = 0;
                if (Main.myPlayer == Projectile.owner)
                {
                    float mag = 12f;
                    for(int i = 0; i < 5; i++)
                    {
                        Projectile.NewProjectile(new EntitySource_Parent(Entity), Projectile.Center.X, Projectile.Center.Y, (float)(Math.Cos(rot + (float)(Math.PI * 2 * i / 5))) * mag, (float)(Math.Sin(rot + (float)(Math.PI * 2 * i / 5))) * mag, ModContent.ProjectileType<MarsGeyeserBurst>(), Projectile.damage / 2, 3f, Main.myPlayer);
                    }
                }
            }
        }
    }
}