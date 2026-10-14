using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace NewDawn.Projectiles.Magic
{
    public class BlitzBolt2 : ModProjectile
    {
        public int timer = 0;
        public float reduce = 0f;
        bool justSpawned = false;
        public override void SetDefaults()
        {
            justSpawned = false;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 10;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            for (int i = 0; i < 1; i++)
            {
                Vector2 newPos = Projectile.Center;
                newPos -= Projectile.velocity * ((float)i * 0.25f);
                Projectile.alpha = 50;
                int dust = Dust.NewDust(newPos, 1, 1, 156, 0f, 0f, 150, default(Color), 1.25f - reduce);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].position = newPos;
                Dust dust3 = Main.dust[dust];
                dust3.velocity *= 0.2f;
            }

            if (reduce < 2f)
            {
                reduce += 0.025f;
            }

            timer++;
            if (timer > 2)
            {
                Projectile.velocity.Y += Main.rand.NextFloat(-1.5f, 1.5f);
                Projectile.velocity.X += Main.rand.NextFloat(-1.5f, 1.5f);
                timer = 0;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 156, 0.5f * -Projectile.velocity.X, 0.5f * -Projectile.velocity.Y, 100, default(Color), 0.5f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}