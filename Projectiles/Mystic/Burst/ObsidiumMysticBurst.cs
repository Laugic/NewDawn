using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace NewDawn.Projectiles.Mystic.Burst
{
    public class ObsidiumMysticBurst : MysticProjectile
    {
        public int delay = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Thermal Crystalline");
        }

        public override void SetDefaults()
        {
            delay = 0;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.timeLeft = 180;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
        }

        public override void AI()
        {
            if (Main.myPlayer == Projectile.owner && Projectile.timeLeft < 170)
            {
                Vector2 vec;
                vec.X = Main.MouseWorld.X;
                vec.Y = Main.MouseWorld.Y;

                float num488 = 7.5f;

                Vector2 vector38 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
                float num489 = vec.X - vector38.X;
                float num490 = vec.Y - vector38.Y;
                float num491 = (float)Math.Sqrt((double)(num489 * num489 + num490 * num490));
                num491 = num488 / num491;
                num489 *= num491;
                num490 *= num491;

                Projectile.velocity.X = (Projectile.velocity.X * 20f + num489) / 21f;
                Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num490) / 21f;
            }
        }
    }
}