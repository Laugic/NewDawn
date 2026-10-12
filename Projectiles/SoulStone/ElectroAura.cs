using System;
using Terraria;
using Terraria.ModLoader;

namespace NewDawn.Projectiles.SoulStone
{
    public class ElectroAura : ModProjectile
    {
        float theta = 0;
        float range = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Electroaura");
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.timeLeft = 120;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            theta -= (float)Math.PI / 60;
            if (Projectile.timeLeft <= 30)
                range -= 3;
            else if (range < 90)
                range += 3;
            Projectile.position.X = (float)(Math.Cos(theta) * range) + Main.player[Projectile.owner].position.X;
            Projectile.position.Y = (float)(Math.Sin(theta) * range) + Main.player[Projectile.owner].position.Y;
        }
    }
}