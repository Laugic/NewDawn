using System;
using Terraria;
using Terraria.ModLoader;


namespace NewDawn.Projectiles.SoulStone
{
    public class CapacityThornsProj : ModProjectile
    {
        float theta = 0;
        bool spawned = false;
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 800;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {
            if (!spawned)
            {
                spawned = true;
                theta = Projectile.ai[0];
            }
            Player player = Main.player[Projectile.owner];
            
            theta += (float)(Math.PI / 60);
            float mag = 48;
            Projectile.position.X = player.position.X + (float)Math.Cos(theta) * mag;
            Projectile.position.Y = player.position.Y + (float)Math.Sin(theta) * mag;
            Projectile.rotation = theta + (float)Math.PI / 2;
        }
    }
}